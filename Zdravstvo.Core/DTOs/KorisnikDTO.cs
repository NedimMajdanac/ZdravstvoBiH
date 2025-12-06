using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.DTOs
{
    public class KorisnikDTO
    {
        public class RegisterKorisnikDTO
        {
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string Role { get; set; } // "Pacijent" ili "Doktor"
        }
        public class LoginKorisnikDTO
        {
            public string Email { get; set; }
            public string PasswordHash { get; set; }
        }
        public class ReadKorisnikDTO
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Role { get; set; } // "Pacijent" ili "Doktor"
        }
        public class UpdateKorisnikDTO
        {
            public string PasswordHash { get; set; }
            public string Email { get; set; }
        }
        public class RegisterKorisnikForProfile
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string JMBG { get; set; }
            public string BrojTelefona { get; set; }
            public DateTime DatumRodjenja {  get; set; }
            public string Adresa { get; set; }
            public string Spol {  get; set; }
        }

    }
}
