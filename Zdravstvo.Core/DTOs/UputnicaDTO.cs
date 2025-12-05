using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Core.DTOs
{
    public class UputnicaDTO
    {
        public class ReadUputnicaDTO
        {
            public int Id { get; set; }
            public int PacijentId { get; set; }
            public int DoktorId { get; set; } // Doktor koji je izdao uputnicu
            public int SpecijalizacijaId { get; set; } // Specijalizacija na koju je uputnica izdata

            public DateTime DatumIzdavanja { get; set; }
            public string SifraUputnice { get; set; } // Jedinstvena sifra uputnice
            public bool IsKoristena { get; set; }
            public DateTime? DatumKoristenja { get; set; }
        }

        public class CreateUputnicaDTO
        {
           // public int PacijentId { get; set; } // trenutno pacijent koji je na pregledu
           // public int DoktorId { get; set; }  // Doktor koji je izdao uputnicu :: logovan doktor njegov id se koristi
            public int SpecijalizacijaId { get; set; } // Specijalizacija na koju je uputnica izdata : : biranje iz liste specijalizacija

            public DateTime DatumIzdavanja { get; set; } // automatski se postavlja na trenutni datum
            
        }
        
    }
}
