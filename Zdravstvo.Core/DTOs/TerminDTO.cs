using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.DTOs
{
    public  class TerminDTO
    {
        public class ReadTerminDTO
        {
            public int Id { get; set; }
            public DateTime DatumVreme { get; set; }
            public bool Uputnica { get; set; }
            public string Status { get; set; }
            public int PacijentId { get; set; } // Pacijent koji je zakazao termin
            public int DoktorId { get; set; } // Doktor kod kojeg je zakazan termin
            public int UstanovaId { get; set; } // Ustanova u kojoj je termin zakazan
        }

        public class UpdateTerminDTO
        {
            public string Status { get; set; }
            public DateTime DatumVreme { get; set; }

        }

        public class CreateTerminDTO
        {
            public string Status { get; set; } // generisati status "Zakazan" pri kreiranju preko servisa
            public DateTime DatumVreme { get; set; }
            public bool Uputnica { get; set; } // Ukoliko je doktor izdao uputnicu za termin, true ako doktor ima specijalizaciju
            public int DoktorId { get; set; } // Doktor kod kojeg se zakazuje termin
            public int PacijentId { get; set; } // Pacijent koji zakazuje termin
            public int UstanovaId { get; set; } // Ustanova u kojoj se zakazuje termin

        }
    }
}
