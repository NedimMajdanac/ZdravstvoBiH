using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Recepti
    {
        public int Id { get; set; }
        public int PregledId { get; set; }
        public Pregled Pregled { get; set; }
        public string NazivLijeka { get; set; }
        public string Doziranje { get; set; }
        public DateTime DatumIzdavanja { get; set; }
    }
}
