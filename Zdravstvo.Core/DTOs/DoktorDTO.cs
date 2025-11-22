using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Core.DTOs
{
    public class DoktorDTO
    {
        public class  ReadDoktorDTO
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string KontaktTelefon { get; set; }
            public string Email { get; set; }
            public string BrojLicence { get; set; }
            public Ustanova Ustanove { get; set; }  // pregled admina za ustanove u kojem radi doktor
            public Specijalizacija Specijalizacija { get; set; } // pregled specijalizacije ako je ima
        }

        public class UpdateDoktorDTO
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string KontaktTelefon { get; set; }
            public string Email { get; set; }   
            public Ustanova Ustanova { get; set; }  // mogucnost promjene ustanove
        }

        public class CreateDoktorDTO
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string KontaktTelefon { get; set; }
            public int SpecijalizacijaId { get; set; } // dodjeljivanje specijalizacije pri kreiranju doktora
            public int UstanovaId { get; set; } // dodjeljivanje ustanove pri kreiranju doktora
        }
    }
}
