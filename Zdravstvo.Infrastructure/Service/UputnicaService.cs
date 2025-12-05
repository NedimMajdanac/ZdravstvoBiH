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
    public class UputnicaService : IUputnicaService
    {
        private readonly IMapper _mapper;
        private readonly ZdravstvoContext _db;
        private readonly ValidationService _validationService;

        public UputnicaService(IMapper mapper,ZdravstvoContext db, ValidationService validationService)
        {
            _db = db;
            _mapper = mapper;
            _validationService = validationService;
        }

        // Generisi Sifru Uputnice
        public string GenerisiSifruUputnice()
        {
            var datePart = DateTime.Now.ToString("ddMMyy");
            var random = new Random();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var digits = "0123456789";
            var letterPart = new char[2];
            var digitPart = new char[2];
            for (int i = 0; i < 2; i++)
            {
                letterPart[i] = letters[random.Next(letters.Length)];
                digitPart[i] = digits[random.Next(digits.Length)];
            }
            return $"UP-{datePart}-{new string(letterPart)}{new string(digitPart)}";
        }

        // Get All Uputnice
        public async Task<List<UputnicaDTO.ReadUputnicaDTO>> GetAllUputnice()
        {
            var uputnice = await _db.Uputnice.ToListAsync();
            return _mapper.Map<List<UputnicaDTO.ReadUputnicaDTO>>(uputnice);
        }

        // Get Uputnica by Id
        public async Task<UputnicaDTO.ReadUputnicaDTO> GetUputnicaById(int id)
        {
            var uputnica = await _db.Uputnice.FindAsync(id);
            if(uputnica == null) return null;
            return _mapper.Map<UputnicaDTO.ReadUputnicaDTO>(uputnica);
        }
        // Create Uputnica
        public async Task<UputnicaDTO.ReadUputnicaDTO> CreateUputnicaForTermin(int terminId, UputnicaDTO.CreateUputnicaDTO createUputnicaDTO, int requestingDoktor)
        {

            var termin = await _db.Termini.FindAsync(terminId);

            if (termin == null)
                throw new ArgumentException("Termin ne postoji");
            if (termin.DoktorId != requestingDoktor)
                throw new UnauthorizedAccessException("Niste ovlasteni za kreiranje uputnice");

            var uputnica = _mapper.Map<Uputnica>(createUputnicaDTO);
            
            uputnica.SifraUputnice = GenerisiSifruUputnice();
            uputnica.IsKoristena = false;
            uputnica.DatumKoristenja = null;
            uputnica.PacijentId = termin.PacijentId;
            uputnica.DoktorId = termin.DoktorId;

            _db.Uputnice.Add(uputnica);
            await _db.SaveChangesAsync();
            return _mapper.Map<UputnicaDTO.ReadUputnicaDTO>(uputnica);
        }
    }
}
