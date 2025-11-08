using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zdravstvo.Infrastructure.Data;

namespace Zdravstvo.Infrastructure.Service
{
    public class ValidationService
    {
        private readonly ZdravstvoContext _db;

        public ValidationService(ZdravstvoContext db)
        {
            _db = db;
        }

        public async Task ValidatePacijentEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Obavezno polje");

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Uneseni format email adrese nije ispravan");

            bool exists = await _db.Pacijenti.AnyAsync(x => x.Email == email);

            if (exists)
                throw new ArgumentException("Pacijent sa unesenom email adresom već postoji"); 
        }

        public async Task ValidatePacijentTelefon(string telefon)
        {
            if(!Regex.IsMatch(telefon, @"^\+?\d{6,15}$"))
                throw new ArgumentException("Uneseni format broja telefona nije ispravan");
        }

        public async Task ValidatePacijentJMBG(string jmbg)
        {
            if (jmbg.Length != 13)
                throw new ArgumentException("JMBG mora da ima 13 cifri");

            bool exists = await _db.Pacijenti.AnyAsync(x => x.Email == jmbg);

            if (exists)
                throw new ArgumentException("Pacijent sa unesenim JMBG-om već postoji");
        }
    }
}
