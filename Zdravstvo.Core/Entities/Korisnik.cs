using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Korisnik
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Pacijent" ili "Doktor"

        // Navigacijska svojstva
        public Doktor Doktor { get; set; }
        public Pacijent Pacijent { get; set; }
    }
}
