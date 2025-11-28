using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Core.DTOs
{
    public class DijagnozaDTO
    {
        public class ReadDijagnozaDTO 
        {
            public int Id { get; set; }
            public int PregledId { get; set; } // ID pregleda kojem dijagnoza pripada 
            public string Naziv { get; set; } 
            public string ICD10 { get; set; } // Medjunarodna klasifikacija bolesti :: generisati iz liste ili baze
            public string Napomene { get; set; } 
        }
        public class CreateDijagnozaDTO
        {
            public int PregledId { get; set; } // ID pregleda kojem dijagnoza pripada :: upisuje se prilikom kreiranja
            public string Naziv { get; set; } 
            public string ICD10 { get; set; } // Medjunarodna klasifikacija bolesti :: generisati iz liste ili baze
            public string Napomene { get; set; }
        }

        public class UpdateDijagnozaDTO
        {
            public string Napomene { get; set; }
        }
    }
}
