using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Entities;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Data;

namespace Zdravstvo.Infrastructure.Service
{
    public class TerminService : ITerminService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        

        public TerminService(ZdravstvoContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task ValidateTermin(DateTime dateTime,int doktorId)
        {
            bool exists = await _db.Termini.AnyAsync(x => x.DatumVreme.Hour == dateTime.Hour && x.DoktorId == doktorId);
            if (exists)
                throw new ArgumentException("Doktor već ima zakazan termin u ovom terminu");
            if(dateTime.Date < DateTime.Now.Date)
                throw new ArgumentException("Ne možete zakazati termin u prošlosti");
        }

        public async Task ValidateSelectDoktor(int doktorId,int ustanovaId)
        {
            bool exists = await _db.Doktori.AnyAsync(x => x.Id == doktorId && x.UstanovaId == ustanovaId);
            if (!exists)
                throw new ArgumentException("Izabrani doktor ne radi u odabranoj ustanovi");
        }

        public async Task ValidatePacijentTermin(DateTime dateTime, int pacijentId)
        {
            bool exists = await _db.Termini.AnyAsync(x => x.DatumVreme == dateTime && x.PacijentId == pacijentId);
            if (exists)
                throw new ArgumentException("Već imate zakazan termin u ovom terminu");
        }

        public async Task ValidateTerminLimit(DateTime dateTime,int doktorId)
        {
            var count = await _db.Termini.CountAsync(x => x.DatumVreme.Date == dateTime.Date && x.DoktorId == doktorId);
            if (count >= 20)
                throw new ArgumentException("Dostignut je maksimalan broj termina za izabranog doktora na ovaj dan");
        }

       


        public async Task<List<TerminDTO.ReadTerminDTO>> GetAllTermini()
        {
            var termini = await _db.Termini.ToListAsync();
            return _mapper.Map<List<TerminDTO.ReadTerminDTO>>(termini);
        }

        public async Task<TerminDTO.ReadTerminDTO> GetTerminById(int id)
        {
            var termin = await _db.Termini.FindAsync(id);
            return _mapper.Map<TerminDTO.ReadTerminDTO>(termin);
        }

        public async Task<TerminDTO.ReadTerminDTO> CreateTermin(TerminDTO.CreateTerminDTO createTerminDTO)
        {
            await ValidateTermin(createTerminDTO.DatumVreme, createTerminDTO.DoktorId);
            await ValidateSelectDoktor(createTerminDTO.DoktorId, createTerminDTO.UstanovaId);
            await ValidatePacijentTermin(createTerminDTO.DatumVreme, createTerminDTO.PacijentId);
            await ValidateTerminLimit(createTerminDTO.DatumVreme, createTerminDTO.DoktorId);

            var doktor = await _db.Doktori
                .Include(d => d.Specijalizacija)
                .FirstOrDefaultAsync(d => d.Id == createTerminDTO.DoktorId);
           
            if (doktor == null)
                throw new ArgumentException("Doktor ne postoji");


            var specName = doktor.Specijalizacija?.Naziv ?? string.Empty;

            if(!string.Equals(specName, "Porodična medicina", StringComparison.OrdinalIgnoreCase))
            {
                if (!createTerminDTO.UputnicaId.HasValue)
                    throw new ArgumentException("Za termin kod specijaliste potrebna vam je uputnica");

                var uputnica = await _db.Uputnice.FindAsync(createTerminDTO.UputnicaId.Value);
                if (uputnica == null)
                    throw new ArgumentException("Uputnica ne postoji");

                if (uputnica.IsKoristena)
                    throw new InvalidOperationException("Uputnica je vec iskoristena");

                if (uputnica.PacijentId != createTerminDTO.PacijentId)
                    throw new ArgumentException("Uputnica nije izdata za ovog pacijenta");

                uputnica.IsKoristena = true;
                uputnica.DatumKoristenja = DateTime.Now;
            }

            var termin = _mapper.Map<Termin>(createTerminDTO);
            
            termin.Status = "Zakazan"; // Postavljanje statusa na "Zakazan" pri kreiranju
            
            _db.Termini.Add(termin);
            
            await _db.SaveChangesAsync();
            return _mapper.Map<TerminDTO.ReadTerminDTO>(termin);
        }

        public async Task<TerminDTO.ReadTerminDTO> UpdateTermin(int id, TerminDTO.UpdateTerminDTO updateTerminDTO)
        {
            var termin = await _db.Termini.FindAsync(id);
            if (termin == null)
            {
                return null;
            }
            _mapper.Map(updateTerminDTO, termin);
            await _db.SaveChangesAsync();
            return _mapper.Map<TerminDTO.ReadTerminDTO>(termin);
        }

        public async Task<bool> DeleteTermin(int id)
        {
            var termin = await _db.Termini.FindAsync(id);
            if (termin == null)
            {
                return false;
            }
            _db.Termini.Remove(termin);
            await _db.SaveChangesAsync();
            return true;
        }   

        public async Task<List<TerminDTO.ReadTerminDTO>> GetTerminiByPacijentId(int pacijentId)
        {
            var termini = await _db.Termini.Where(t => t.PacijentId == pacijentId).ToListAsync();
            return _mapper.Map<List<TerminDTO.ReadTerminDTO>>(termini);
        }

        public async Task<List<TerminDTO.ReadTerminDTO>> GetTerminiByDoktorId(int doktorId)
        {
            var termini = await _db.Termini.Where(t => t.DoktorId == doktorId).ToListAsync();
            return _mapper.Map<List<TerminDTO.ReadTerminDTO>>(termini);
        }   

        public async Task<List<TerminDTO.ReadTerminDTO>> GetTerminiByUstanovaId(int ustanovaId)
        {
            var termini = await _db.Termini.Where(t => t.UstanovaId == ustanovaId).ToListAsync();
            return _mapper.Map<List<TerminDTO.ReadTerminDTO>>(termini);
        }


    }
}
