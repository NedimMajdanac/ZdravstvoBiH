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

        public AuthService(ZdravstvoContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
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

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
