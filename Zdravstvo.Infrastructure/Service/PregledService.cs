using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Entities;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Data;

namespace Zdravstvo.Infrastructure.Service
{
    public class PregledService : IPregledService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        public PregledService(ZdravstvoContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Get all pregledi
        public async Task<List<PregledDTO.ReadPregledDTO>> GetAllPregledi()
        {
            var pregledi = await _db.Pregledi.ToListAsync();
            return _mapper.Map<List<PregledDTO.ReadPregledDTO>>(pregledi);
        }

        // Get pregled by id
        public async Task<PregledDTO.ReadPregledDTO> GetPregledById(int id)
        {
            var pregld = await _db.Pregledi.FindAsync(id);
            if(pregld == null)
            {
                return null;
            }
            return _mapper.Map<PregledDTO.ReadPregledDTO>(pregld);
        }

        // Create new pregled
        public async Task<PregledDTO.ReadPregledDTO> CreatePregledForTermin(int terminId,PregledDTO.CreatePregledDTO createPregledDTO, int requestingDoktorId)
        {
            var termin = await _db.Termini.FindAsync(terminId);

            if (termin == null) 
                throw new ArgumentException("Termin ne postoji");
            var now = DateTime.Now;

            //if (!(termin.DatumVreme <= now && now < termin.DatumVreme.AddHours(1)))
            //    throw new ArgumentException("Nije moguce izvan satnice termina upisati pregled");
            if (termin.DoktorId != requestingDoktorId) 
                throw new UnauthorizedAccessException("Nemate ovlasti za upis podata za pregled");
            if (await _db.Pregledi.AnyAsync(p => p.TerminId == terminId))
                throw new InvalidOperationException("Pregled za ovaj termin vec postoji!");

            var pregled = _mapper.Map<Pregled>(createPregledDTO);
            pregled.TerminId = termin.Id;
            pregled.DoktorId = termin.DoktorId;
            pregled.PacijentId = termin.PacijentId;
            pregled.DatumPregleda = termin.DatumVreme;

            termin.Status = "Zavrsen";

            _db.Pregledi.Add(pregled);
            await _db.SaveChangesAsync();
            return _mapper.Map<PregledDTO.ReadPregledDTO>(pregled);
            
        }

        // Update existing pregled
        public async Task<PregledDTO.ReadPregledDTO> UpdatePregled(int id, PregledDTO.UpdatePregledDTO updatePregledDTO)
        {
            var pregled = await _db.Pregledi.FindAsync(id);
            if (pregled == null)
            {
                return null;
            }
            _mapper.Map(updatePregledDTO, pregled);
            await _db.SaveChangesAsync();
            return _mapper.Map<PregledDTO.ReadPregledDTO>(pregled);
        }

        // GET pregledi by vlasinistvo (pacijent i doktor) :: trenutno logged in korisnik
        public async Task<List<PregledDTO.ReadPregledDTO>> GetPreglediForLoggedPacient(int pacijentId)
        {
            var pregled = await _db.Pregledi
                .Where(x => x.PacijentId == pacijentId)
                .ToListAsync();
            if (pregled == null) throw new Exception("Pregled ne postoji");
            return _mapper.Map<List<PregledDTO.ReadPregledDTO>>(pregled);
        }

        public async Task<List<PregledDTO.ReadPregledDTO>> GetPreglediForLoggedDoktor(int doktorId)
        {
            var pregled = await _db.Pregledi
                .Where(x => x.DoktorId == doktorId)
                .ToListAsync(); ;
            if (pregled == null) throw new Exception("Pregled ne postoji");
            return _mapper.Map<List<PregledDTO.ReadPregledDTO>>(pregled);
        }
    }
}
