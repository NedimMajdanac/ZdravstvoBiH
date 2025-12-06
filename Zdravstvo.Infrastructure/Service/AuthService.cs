using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Entities;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Data;

namespace Zdravstvo.Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly ZdravstvoContext _db;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
      
        public AuthService(ZdravstvoContext db, IConfiguration configuration, IMapper mapper)
        {
            _db = db;
            _configuration = configuration;
            _mapper = mapper;
        }

        // Registracija
        public async Task<string> RegisterUser(KorisnikDTO.RegisterKorisnikDTO registerKorisnikDTO)
        {
            if (await _db.Korisnici.AnyAsync(x => x.Email == registerKorisnikDTO.Email))
                throw new Exception("Korisnik sa ovim emailom vec postoji.");

            // hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerKorisnikDTO.PasswordHash);

            var user = new Korisnik
            {
                Email = registerKorisnikDTO.Email,
                PasswordHash = hashedPassword,
                Role = registerKorisnikDTO.Role,
            };

            _db.Korisnici.Add(user);
            await _db.SaveChangesAsync();

            // JWT token
            return GenerateJwtToken(user);
            
        }

        // Register for user pacijent profile
        public async Task<string> RegisterUserForProfile(KorisnikDTO.RegisterKorisnikForProfile registerKorisnikForProfileDTO)
        {
            bool exists = await _db.Korisnici.AnyAsync(x => x.Email == registerKorisnikForProfileDTO.Email);
            if (exists)
                throw new Exception("Korisnik sa email postoji");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerKorisnikForProfileDTO.Password);
            
            using var tx = await _db.Database.BeginTransactionAsync();

            try
            {
                var korisnik = new Korisnik
                {
                    Email = registerKorisnikForProfileDTO.Email,
                    PasswordHash = hashedPassword,
                    Role = string.IsNullOrWhiteSpace(registerKorisnikForProfileDTO.Role) ? "Pacijent" : registerKorisnikForProfileDTO.Role
                };
                var pacijent = _mapper.Map<Pacijent>(registerKorisnikForProfileDTO);

                _db.Korisnici.Add(korisnik);
                var karton = new MedicinskiKarton
                {
                    Alergije = string.Empty,
                    Vakcinacija = string.Empty,
                    KrvnaGrupa = string.Empty,
                    HronicneBolesti = string.Empty,
                    Operacije = string.Empty,
                    PorodicnaAnamneza = string.Empty,
                    Terapije = string.Empty,
                    Napomena = string.Empty
                };
                _db.MedicinskiKartoni.Add(karton);

                pacijent.Korisnik = korisnik;
                pacijent.MedicinskiKarton = karton;

                _db.Pacijenti.Add(pacijent);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                return GenerateJwtToken(korisnik);
                
            }
            catch(DbUpdateException dbEX)
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        // Login
        public async Task<string> LoginUser(KorisnikDTO.LoginKorisnikDTO loginKorisnikDTO)
        {
            var user = await _db.Korisnici.FirstOrDefaultAsync(x => x.Email == loginKorisnikDTO.Email);

            if (user == null)
                throw new Exception("Pogresan email ili lozinka");

            bool validPass = BCrypt.Net.BCrypt.Verify(loginKorisnikDTO.PasswordHash, user.PasswordHash);

            if (!validPass)
                throw new Exception("Pogresan email ili lozinka");

            // JWT token
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(Korisnik user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var doktor = _db.Doktori.FirstOrDefault(d => d.KorisnikId == user.Id);
            if (doktor != null)
            {
                claims.Add(new Claim("doktorId", doktor.Id.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
