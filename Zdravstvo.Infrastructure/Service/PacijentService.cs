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
    public class PacijentService : IPacijentService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;

        public PacijentService(ZdravstvoContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Get all pacijenti
        public async Task<List<PacijentDTO.ReadPacijentDTO>> GetAllPacijenti()
        {
            var pacijenti = await _db.Pacijenti.ToListAsync();
            return _mapper.Map<List<PacijentDTO.ReadPacijentDTO>>(pacijenti);
        }

        // Get pacijent by ID
        public async Task<PacijentDTO.ReadPacijentDTO> GetPacijentById(int id)
        {
            var pacijent = await _db.Pacijenti.FindAsync(id);
            if (pacijent == null) return null;
            return _mapper.Map<PacijentDTO.ReadPacijentDTO>(pacijent);
        }

        // Create new pacijent
        public async Task<PacijentDTO.ReadPacijentDTO> CreatePacijent(PacijentDTO.CreatePacijentDTO createPacijentDTO)
        {
            var pacijent = _mapper.Map<Pacijent>(createPacijentDTO);
            _db.Pacijenti.Add(pacijent);

            await _db.SaveChangesAsync();   

            return _mapper.Map<PacijentDTO.ReadPacijentDTO>(pacijent);
        }

        // Update pacijent
        public async Task<PacijentDTO.ReadPacijentDTO> UpdatePacijent(int id, PacijentDTO.UpdatePacijentDTO updatePacijentDTO)
        {
            var pacijent = await _db.Pacijenti.FindAsync(id);
            if (pacijent == null) throw new Exception("Pacijent ne postoji");

            _mapper.Map(updatePacijentDTO, pacijent);
            await _db.SaveChangesAsync();

            return _mapper.Map<PacijentDTO.ReadPacijentDTO>(pacijent);
        }
    }
}
