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
        }


        public DbSet<Pacijent> Pacijenti { get; set; }
        public DbSet<Doktor> Doktori { get; set; }
        public DbSet<Ustanova> Ustanove { get; set; }
        public DbSet<Termin> Termini { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<MedicinskiKarton> MedicinskiKartoni { get; set; }
        }
}
