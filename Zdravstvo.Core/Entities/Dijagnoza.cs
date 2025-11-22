using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Dijagnoza
    {
        public int Id { get; set; }
        public int PregledId { get; set; }
        public Pregled Pregled { get; set; }
        public string Naziv { get; set; }
        public string ICD10 { get; set; }
        public string Napomene { get; set; }
    }
}
