using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.DTOs
{
    public class SpecijelizacijaDTO
    {
        public class ReadSpecijalizacijaDTO
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }
        public class CreateSpecijalizacijaDTO
        {
            public string Naziv { get; set; }
        }
        public class UpdateSpecijalizacijaDTO
        {
            public string Naziv { get; set; }
        }
    }
}
