using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Doktor
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public string Specijalizacija { get; set; }
        public string BrojLicence { get; set; }

        public int UstanovaId { get; set; }
        public Ustanova Ustanova { get; set; }

        //public ICollection<Nalaz> Nalazi { get; set; }
        public ICollection<Termin> Termini { get; set; }
    }
}
