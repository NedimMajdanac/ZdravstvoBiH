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

                // Korisnici 
                var korisnikDoktor1 = new Korisnik { Email = "amir.kovacevic@zdrv.gov.ba", PasswordHash = "hash1213", Role = "Doktor" };
                var korisnikPacijent1 = new Korisnik { Email = "Ibro12@gmail.com", PasswordHash="123546", Role = "Pacijent" };
                var korisnikDoktor2 = new Korisnik { Email = "mujo.mujic@zdrv.gov.ba", PasswordHash = "21546", Role = "Doktor" };
                var korisnikPacijent2 = new Korisnik { Email = "Saro@gmail.com", PasswordHash = "78463232", Role = "Pacijent" };
                context.Korisnici.AddRange(korisnikDoktor1,korisnikDoktor2,korisnikPacijent1,korisnikPacijent2);
                context.SaveChanges();


                // 2. Doktori
                var doktor1 = new Doktor { Ime = "Amir", Prezime = "Kovačević", KontaktTelefon = "061/123-456", Email = "amir.kovacevic@zdrv.gov.ba", Ustanova = ustanova1, Specijalizacija = "Pedijatar", BrojLicence = "pdr12BB01",KorisnikId=korisnikDoktor1.Id };
                var doktor2 = new Doktor { Ime = "Mujo", Prezime = "Mujic", KontaktTelefon = "061/123-854", Email = "mujo.mujic@zdrv.gov.ba", Ustanova = ustanova2, Specijalizacija = "Zubar", BrojLicence = "zbr12CC03" ,KorisnikId=korisnikDoktor2.Id};
                context.Doktori.AddRange(doktor1, doktor2);
                context.SaveChanges();

                // 3. Pacijenti
                var pacijent1 = new Pacijent {  Ime = "Ibrahim", Prezime = "Ibric", JMBG = "1234567890123", Email = "Ibro12@gmail.com", Adresa = "Adresa 12", BrojTelefona = "061/987-654", DatumRodjenja = new DateTime(1990, 5, 15), Spol = "Muski", KorisnikId=korisnikPacijent1.Id };
                var pacijent2 = new Pacijent {  Ime = "Sara", Prezime = "Saric", JMBG = "9876543210987", Email = "Saro@gmail.com", Adresa = "Adresa 34", BrojTelefona = "061/654-321", DatumRodjenja = new DateTime(1985, 8, 22), Spol = "Zenski" ,KorisnikId=korisnikPacijent2.Id};
                context.Pacijenti.AddRange(pacijent1, pacijent2);
                context.SaveChanges();

                // 4. Termini
                var termin1 = new Termin { Pacijent = pacijent1, Doktor = doktor1, Ustanova = ustanova1, DatumVreme = new DateTime(2024, 7, 10, 9, 0, 0), Uputnica = false, Status = "Potvrdjeno" };
                var termin2 = new Termin { Pacijent = pacijent2, Doktor = doktor2, Ustanova = ustanova2, DatumVreme = new DateTime(2024, 7, 11, 10, 30, 0), Uputnica = false, Status = "Potvrdjeno" };
                context.Termini.AddRange(termin1, termin2);
                context.SaveChanges();

                // Medicinski karton
                var karton1 = new MedicinskiKarton { Alergije="Nema", Vakcinacija="Covid,2019", KrvnaGrupa="A+", HronicneBolesti="Nema", Operacije="Nema", PorodicnaAnamneza="Nema", Terapije="Penicilin 200mg", Napomena="", Pacijent=pacijent1 };
                var karton2 = new MedicinskiKarton { Alergije="Nema", Vakcinacija="Covid,2019", KrvnaGrupa="A+", HronicneBolesti="Nema", Operacije="Nema", PorodicnaAnamneza="Nema", Terapije="Penicilin 200mg", Napomena="", Pacijent=pacijent2 };
                context.MedicinskiKartoni.AddRange(karton1, karton2);
                context.SaveChanges();

        }
        }
    }

