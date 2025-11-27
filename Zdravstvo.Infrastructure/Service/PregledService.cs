using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<PregledDTO.ReadPregledDTO> CreatePregled(PregledDTO.CreatePregledDTO createPregledDTO)
        {

        
            var pregled = _mapper.Map<Pregled>(createPregledDTO);

            var termin = await _db.Termini
                .Include(t => t.Doktor)
                .Include(t => t.Pacijent)
                .FirstOrDefaultAsync(t => t.DatumVreme.Date.Hour == createPregledDTO.DatumPregleda.Date.Hour);
           
            if (termin == null)
            {
                throw new Exception("Termin not found for the given DatumPregleda.");
            }

            pregled.TerminId = termin.Id;
            pregled.DoktorId = termin.DoktorId;
            pregled.PacijentId = termin.PacijentId;
            pregled.DatumPregleda = termin.DatumVreme;

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
    }
}
