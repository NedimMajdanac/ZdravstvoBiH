using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Pregled
    {
        public int Id { get; set; }
        public DateTime DatumPregleda { get; set; }
        public string Misljenje { get; set; }
        public string Terapija { get; set; }
        public string Napomene { get; set; }

        public int TerminId { get; set; }
        public Termin Termin { get; set; }
        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; }
        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }
        public Dijagnoza? Dijagnoza { get; set; }
    }
}
