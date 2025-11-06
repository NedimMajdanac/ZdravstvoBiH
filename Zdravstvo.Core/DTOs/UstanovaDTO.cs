using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.DTOs
{
    public class UstanovaDTO
    {
        public class ReadUstanovaDTO
        {
            public int id { get; set; }
            public string Naziv { get; set; }
            public string Adresa { get; set; }
            public string Grad { get; set; }
            public string Tip { get; set; }
            public string Telefon { get; set; }

            // Mogu se dodati dodatna polja ako je potrebno

        }

        public class CreateUstanovaDTO
        {
            public string Naziv { get; set; }
            public string Adresa { get; set; }
            public string Grad { get; set; }
            public string Tip { get; set; }
            public string Telefon { get; set; }
        }

        public class UpdateUstanovaDTO
        {
            public string Naziv { get; set; }
            public string Telefon { get; set; }
        }
    }
}
