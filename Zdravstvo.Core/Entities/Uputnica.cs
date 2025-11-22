using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Uputnica
    {
        public int Id { get; set; }
        
        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }
        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; }
        public int SpecijalizacijaId { get; set; }
        public Specijalizacija Specijalizacija { get; set; }
        
        public DateTime DatumIzdavanja { get; set; } 
        public string SifraUputnice { get; set; }
        public bool IsKoristena { get; set; } = false;
        public DateTime? DatumKoristenja { get; set; }
        
    }
}
