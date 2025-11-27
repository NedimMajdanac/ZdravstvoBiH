using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Core.DTOs
{
    public class PregledDTO
    {
        public class ReadPregledDTO
        {
            public int Id { get; set; }
            public DateTime DatumPregleda { get; set; }
            public string Misljenje { get; set; }
            public string Terapija { get; set; }
            public string Napomene { get; set; }

            public int TerminId { get; set; }
            public int DoktorId { get; set; }
            public int PacijentId { get; set; }
        }
        public class CreatePregledDTO
        {
            public DateTime DatumPregleda { get; set; }
            public string Misljenje { get; set; }
            public string Terapija { get; set; }
            public string Napomene { get; set; }

            //public int TerminId { get; set; } // upisuje se po vremenskom terminu
            //public int DoktorId { get; set; } // Id od doktora kod kojeg je bio zakazan termin 
            //public int PacijentId { get; set; } 
        }
        public class UpdatePregledDTO
        {
            public string Misljenje { get; set; }
            public string Terapija { get; set; }
            public string Napomene { get; set; }

        }
    }
}
