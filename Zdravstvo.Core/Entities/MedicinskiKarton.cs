using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class MedicinskiKarton
    {
        public int Id { get; set; } // 1, 2, 3...
        public string Alergije { get; set; } // "Penicilin, polen"
        public string Vakcinacija { get; set; } // "COVID-19, Grip"
        public string KrvnaGrupa { get; set; } // "A+, O-, B+" 
        public string HronicneBolesti { get; set; } // "Dijabetes, Hipertenzija"
        public string Operacije { get; set; } // "Appendektomija 2015, Operacija srca 2020"
        public string PorodicnaAnamneza { get; set; } // "Rak dojke u porodici, Dijabetes tip 2"
        public string Terapije { get; set; } // "Metformin 500mg dnevno, Lisinopril 10mg dnevno"
        public string Napomena { get; set; } // "Pacijent treba redovne kontrole"

       
    }
}
