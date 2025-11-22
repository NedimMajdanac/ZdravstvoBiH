using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Termin
    {
        public int Id { get; set; }
        public DateTime DatumVreme { get; set; }
        public string Status { get; set; } // npr. "Zakazan", "Otkazan", "Završen"
        public string Napomena { get; set; } = string.Empty;
        public int? UputnicaId { get; set; }
        public Uputnica Uputnica { get; set; }

        // Relacije
        public int UstanovaId { get; set; }
        public Ustanova Ustanova { get; set; }
        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }
        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; }
    }
}
