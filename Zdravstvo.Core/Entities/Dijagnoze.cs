using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Dijagnoze
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string ICD10Sifra { get; set; } 
        public DateTime DatumDijagnoze { get; set; }
        public string Napomena { get; set; }

        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }
        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; }
    }
}
