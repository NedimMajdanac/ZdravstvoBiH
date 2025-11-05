using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Ustanova
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Tip { get; set; }
        public string Telefon { get; set; }

        public ICollection<Doktor> Doktori { get; set; }
        public ICollection<Termin> Termini { get; set; }
    }
}
