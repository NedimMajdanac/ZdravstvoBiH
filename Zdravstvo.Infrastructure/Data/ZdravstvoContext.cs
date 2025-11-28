using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Infrastructure.Data
{
    public class ZdravstvoContext : DbContext
    {
        public ZdravstvoContext(DbContextOptions<ZdravstvoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Doktor ↔ Ustanova
            modelBuilder.Entity<Doktor>()
                .HasOne(d => d.Ustanova)
                .WithMany(u => u.Doktori)
                .HasForeignKey(d => d.UstanovaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Termin ↔ Doktor
            modelBuilder.Entity<Termin>()
                .HasOne(t => t.Doktor)
                .WithMany(d => d.Termini)
                .HasForeignKey(t => t.DoktorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Termin ↔ Pacijent
            modelBuilder.Entity<Termin>()
                .HasOne(t => t.Pacijent)
                .WithMany(p => p.Termini)
                .HasForeignKey(t => t.PacijentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Korisnik>()
                .HasOne(k => k.Doktor)
                .WithOne(d => d.Korisnik)
                .HasForeignKey<Doktor>(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Korisnik>()
                .HasOne(k => k.Pacijent)
                .WithOne(p => p.Korisnik)
                .HasForeignKey<Pacijent>(p => p.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Specijalizacija>()
                .HasIndex(s => s.Naziv)
                .IsUnique();

            modelBuilder.Entity<Uputnica>()
                .HasOne(u => u.Specijalizacija)
                .WithMany()
                .HasForeignKey(u => u.SpecijalizacijaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pacijent>()
                .HasOne(p => p.MedicinskiKarton)
                .WithOne()
                .HasForeignKey<Pacijent>(p => p.MedicinskiKartonId)
                .OnDelete(DeleteBehavior.Restrict);

           modelBuilder.Entity<Pregled>()
                .HasOne(p => p.Dijagnoza)
                .WithOne(d => d.Pregled)
                .HasForeignKey<Dijagnoza>(d => d.PregledId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Dijagnoza>()
                .HasIndex(d => d.PregledId)
                .IsUnique();

        }


        public DbSet<Pacijent> Pacijenti { get; set; }
        public DbSet<Doktor> Doktori { get; set; }
        public DbSet<Ustanova> Ustanove { get; set; }
        public DbSet<Termin> Termini { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<MedicinskiKarton> MedicinskiKartoni { get; set; }
        public DbSet<Pregled> Pregledi { get; set; }
        public DbSet<Dijagnoza> Dijagnoze { get; set; }
        public DbSet<Recepti> Recepti { get; set; }
        public DbSet<Specijalizacija> Specijalizacija { get; set; }
        public DbSet<Uputnica> Uputnice { get; set; }
        }
}
