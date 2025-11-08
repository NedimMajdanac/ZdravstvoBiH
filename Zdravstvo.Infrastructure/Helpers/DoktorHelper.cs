using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Infrastructure.Helpers
{
    public class DoktorHelper
    {
        public static string GenerateBrojLicence()
        {
            int randomNumber = new Random().Next(1000, 9999);
            return $"DOC-{DateTime.Now.Year}-{randomNumber}";
        }

        public static string GenerateEmail(string ime, string prezime)
        {
            string domain = "zdravstvo.com.ba";
            string email = $"{ime.ToLower()}.{prezime.ToLower()}@{domain}";
            return email;
        }
    }
}
