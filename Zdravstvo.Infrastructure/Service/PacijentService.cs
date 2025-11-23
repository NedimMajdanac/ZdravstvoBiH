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
    public class PacijentService : IPacijentService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;

        public PacijentService(ZdravstvoContext db, IMapper mapper, ValidationService validationService)
        {
            _db = db;
            _mapper = mapper;
            _validationService = validationService;
        }

        // Get all pacijenti
        public async Task<List<PacijentDTO.ReadPacijentDTO>> GetAllPacijenti()
        {
            var pacijenti = await _db.Pacijenti.ToListAsync();
            return _mapper.Map<List<PacijentDTO.ReadPacijentDTO>>(pacijenti);
        }

        // Get pacijent by ID
        public async Task<PacijentDTO.ReadPacijentDTO> GetPacijentById(int id)
        {
            var pacijent = await _db.Pacijenti.FindAsync(id);
            if (pacijent == null) return null;
            return _mapper.Map<PacijentDTO.ReadPacijentDTO>(pacijent);
        }

        // Create new pacijent
        public async Task<PacijentDTO.ReadPacijentDTO> CreatePacijent(PacijentDTO.CreatePacijentDTO createPacijentDTO)
        {
            var korisnik = await _db.Korisnici.FirstOrDefaultAsync(x => x.Email == createPacijentDTO.Email && x.Role=="Pacijent");

            if (korisnik == null)
                throw new Exception("Korisnik sa tim ID ne postoji");

            if (korisnik.Role != "Pacijent")
                throw new Exception("Korisnik nije registrovan kao pacijent");

            await _validationService.ValidatePacijentEmail(createPacijentDTO.Email);
            await _validationService.ValidatePacijentTelefon(createPacijentDTO.BrojTelefona);    
            await _validationService.ValidatePacijentJMBG(createPacijentDTO.JMBG);


            var pacijent = _mapper.Map<Pacijent>(createPacijentDTO);


            pacijent.KorisnikId = korisnik.Id;

            _db.Pacijenti.Add(pacijent);
            await _db.SaveChangesAsync();   

            return _mapper.Map<PacijentDTO.ReadPacijentDTO>(pacijent);
        }

        // Update pacijent
        public async Task<PacijentDTO.ReadPacijentDTO> UpdatePacijent(int id, PacijentDTO.UpdatePacijentDTO updatePacijentDTO)
        {

            await _validationService.ValidatePacijentTelefon(updatePacijentDTO.BrojTelefona);
            await _validationService.ValidatePacijentEmail(updatePacijentDTO.Email);

            var pacijent = await _db.Pacijenti.FindAsync(id);
            if (pacijent == null) throw new Exception("Pacijent ne postoji");

            _mapper.Map(updatePacijentDTO, pacijent);
            await _db.SaveChangesAsync();

            return _mapper.Map<PacijentDTO.ReadPacijentDTO>(pacijent);
        }

        // Search pacijenti by ime, prezime or jmbg
        // Registacija novih pacijenata
        // Login funkcionalnost
        // Brisanje pacijenata

    }
}
