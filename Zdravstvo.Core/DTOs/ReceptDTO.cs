using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Core.DTOs
{
    public class ReceptDTO
    {
        public class ReadReceptDTO
        {
            public int Id { get; set; }
            public string NazivLijeka { get; set; }
            public string Doziranje { get; set; }
            public DateTime DatumIzdavanja { get; set; }
        }

        public class CreateReceptDTO
        {
            public string NazivLijeka { get; set; }
            public string Doziranje { get; set; }
            public DateTime DatumIzdavanja { get; set; }
        }

        public class UpdateReceptDTO
        {
            public string Doziranje { get; set; }
            public DateTime DatumIzdavanja { get; set; }
        }
    }
}
