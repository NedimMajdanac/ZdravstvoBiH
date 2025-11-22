using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Entities
{
    public class Pacijent
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string JMBG { get; set; }
        public string Email { get; set; }
        public string BrojTelefona { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string Spol { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int MedicinskiKartonId { get; set; }
        public MedicinskiKarton MedicinskiKarton { get; set; }

        public ICollection<Termin> Termini { get; set; } = new List<Termin>();
    
    }
}
