using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ZdravstvoContext(serviceProvider.GetRequiredService<DbContextOptions<ZdravstvoContext>>());

            if (context.Ustanove.Any()) return;

            // 1. Ustanove
            var ustanova1 = new Ustanova { Naziv = "Dom Zdravlja Travnik", Adresa = "Vezirska", Grad = "Travnik", Tip = "Dom zdravlja", Telefon = "030/123-456" };
            var ustanova2 = new Ustanova { Naziv = "Dom Zdravlja Zenica", Adresa = "Travnicka", Grad = "Zenica", Tip = "Dom zdravlja", Telefon = "032/123-456" };
            context.Ustanove.AddRange(ustanova1, ustanova2);
            context.SaveChanges();

            // 2. Doktori
            var doktor1 = new Doktor { Ime = "Amir", Prezime = "Kovačević", KontaktTelefon = "061/123-456", Email = "amir.kovacevic@zdrv.gov.ba", Ustanova = ustanova1, Specijalizacija = "Pedijatar", BrojLicence = "pdr12BB01" };
            var doktor2 = new Doktor { Ime = "Mujo", Prezime = "Mujic", KontaktTelefon = "061/123-854", Email = "mujo.mujic@zdrv.gov.ba", Ustanova = ustanova2, Specijalizacija = "Zubar", BrojLicence = "zbr12CC03" };
            context.Doktori.AddRange(doktor1, doktor2);
            context.SaveChanges();

            // 3. Pacijenti
            var pacijent1 = new Pacijent { Id=1,Ime = "Ibrahim", Prezime = "Ibric", JMBG = "1234567890123", Email = "Ibro12@gmail.com", Adresa = "Adresa 12", BrojTelefona = "061/987-654", DatumRodjenja = new DateTime(1990, 5, 15), Spol = "Muski"};
            var pacijent2 = new Pacijent { Id=2,Ime = "Sara", Prezime = "Saric", JMBG = "9876543210987", Email = "Saro@gmail.com", Adresa = "Adresa 34", BrojTelefona = "061/654-321", DatumRodjenja = new DateTime(1985, 8, 22), Spol = "Zenski" };
            context.Pacijenti.AddRange(pacijent1, pacijent2);
            context.SaveChanges();

            // 4. Termini
            var termin1 = new Termin { Pacijent = pacijent1, Doktor = doktor1, Ustanova = ustanova1, DatumVreme = new DateTime(2024, 7, 10, 9, 0, 0), Uputnica=false, Status="Potvrdjeno" };
            var termin2 = new Termin { Pacijent = pacijent2, Doktor = doktor2, Ustanova = ustanova2, DatumVreme = new DateTime(2024, 7, 11, 10, 30, 0), Uputnica = false , Status = "Potvrdjeno" };
            context.Termini.AddRange(termin1, termin2);
            context.SaveChanges();
        }
    }
}
