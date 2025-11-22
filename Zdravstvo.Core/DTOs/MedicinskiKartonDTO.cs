using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.DTOs
{
    public class MedicinskiKartonDTO
    {
        public class ReadMedicinskiKartonDTO
        {
            public string Alergije { get; set; }
            public string HronicneBolesti { get; set; }
            public string Terapije { get; set; }
            public string Vakcinacija { get; set; }
            public string KrvnaGrupa { get; set; }
            public string Operacije { get; set; }
            public string PorodicnaAnamneza { get; set; }
            public string Napomena { get; set; }
        }

        public class CreateMedicinskiKartonDTO
        {
            public string Alergije { get; set; }
            public string HronicneBolesti { get; set; }
            public string Terapije { get; set; }
            public string Vakcinacija { get; set; }
            public string KrvnaGrupa { get; set; }
            public string Operacije { get; set; }
            public string PorodicnaAnamneza { get; set; }
            public string Napomena { get; set; }
        }

        public class UpdateMedicinskiKartonDTO
        {
            public string Alergije { get; set; }
            public string HronicneBolesti { get; set; }
            public string Terapije { get; set; }
            public string Vakcinacija { get; set; }
            public string Operacije { get; set; }
            public string PorodicnaAnamneza { get; set; }
            public string Napomena { get; set; }
        }
    }
}
