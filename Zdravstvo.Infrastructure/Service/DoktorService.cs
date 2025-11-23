using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Entities;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Data;
using Zdravstvo.Infrastructure.Helpers;

namespace Zdravstvo.Infrastructure.Service
{
    public class DoktorService : IDoktorService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;

        public DoktorService(ZdravstvoContext db,IMapper mapper,ValidationService validationService)
        {
            _db = db;
            _mapper = mapper;
            _validationService = validationService;
        }

        // Generate unique email
        public async Task<string> GenerateUniqueEmail(string ime, string prezime)
        {
            var baseEmail = DoktorHelper.GenerateEmail(ime, prezime);
            var uniqueEmail = baseEmail;
            int counter = 1;

            while (await _db.Doktori.AnyAsync(x => x.Email == uniqueEmail))
            {
                uniqueEmail = $"{ime.ToLower()}.{prezime.ToLower()}{counter++}@zdravstvo.com.ba";
            }
            return uniqueEmail;
        }

        // Get All Doktori
        public async Task<List<DoktorDTO.ReadDoktorDTO>> GetAllDoktors()
        {
            var doktori = await _db.Doktori.ToListAsync();
            return _mapper.Map<List<DoktorDTO.ReadDoktorDTO>>(doktori);
        }

        // Get Doktor by ID
        public async Task<DoktorDTO.ReadDoktorDTO> GetDoktorById(int id)
        {
            var doktor = await _db.Doktori.FindAsync(id);
            if (doktor == null) return null;

            return _mapper.Map<DoktorDTO.ReadDoktorDTO>(doktor);
        }
        
        // Update Doktor
        public async Task<DoktorDTO.ReadDoktorDTO> CreateDoktor(DoktorDTO.CreateDoktorDTO createDoktorDTO)
        {

            var doktor = _mapper.Map<Doktor>(createDoktorDTO);

            string email = doktor.Email = GenerateUniqueEmail(doktor.Ime, doktor.Prezime).Result;

            string password = "DefaultPassword123!"; // Default password, should be changed on first login
            
            var korisnik = new Korisnik
            {
                Email = email,
                Role = "Doktor"
            };
            korisnik.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _db.Korisnici.Add(korisnik);
            await _db.SaveChangesAsync();

            doktor.BrojLicence = DoktorHelper.GenerateBrojLicence();

            doktor.KorisnikId = korisnik.Id;    

            _db.Doktori.Add(doktor);

            await _db.SaveChangesAsync();

            return _mapper.Map<DoktorDTO.ReadDoktorDTO>(doktor);
        }
            
        // Create Doktor
        public async Task<DoktorDTO.ReadDoktorDTO> UpdateDoktor(int id, DoktorDTO.UpdateDoktorDTO updateDoktorDTO)
        {
            var doktor = await _db.Doktori.FindAsync(id);
            if (doktor == null) return null;

            
            
            _mapper.Map(updateDoktorDTO, doktor);

            await _db.SaveChangesAsync();
            return _mapper.Map<DoktorDTO.ReadDoktorDTO>(doktor);
        }

    }
}
