using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.DTOs
{
    public class PacijentDTO
    {
        public class ReadPacijentDTO
        {
            public int Id {  get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string BrojTelefona { get; set; }
            public string Spol { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public string JMBG { get; set; }

            public int MedicinskiKartonId { get; set; }
        }

        public class CreatePacijentDTO
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string BrojTelefona { get; set; }
            public string Spol { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public string JMBG { get; set; }
            public string Adresa { get; set; }
            public int MedicinskiKartonId { get; set; }
        }

        public class UpdatePacijentDTO
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string BrojTelefona { get; set; }
            public string Adresa { get; set; }
            // Ostala polja koja se mogu ažurirati (Lozinka...)
        }
    }
}
