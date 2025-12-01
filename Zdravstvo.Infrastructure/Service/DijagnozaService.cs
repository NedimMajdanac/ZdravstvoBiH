using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
    public class DijagnozaService : IDijagnozaService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;

        public DijagnozaService(ZdravstvoContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Get All Dijagnoze
        public async Task<List<DijagnozaDTO.ReadDijagnozaDTO>> GetAll()
        {
            var dijagnoze = await _db.Dijagnoze.ToListAsync();
            return _mapper.Map<List<DijagnozaDTO.ReadDijagnozaDTO>>(dijagnoze);
        }

        // Get by ID
        public async Task<DijagnozaDTO.ReadDijagnozaDTO> GetDijagnozaById(int id)
        {
            var dijagnoza = await _db.Dijagnoze.FindAsync(id);
            if(dijagnoza == null)
            {
                return null;
            }
            return _mapper.Map<DijagnozaDTO.ReadDijagnozaDTO>(dijagnoza);
        }

        // Create dijagnoza
        public async Task<DijagnozaDTO.ReadDijagnozaDTO> CreateDijagnozaForPregled(int pregledId, DijagnozaDTO.CreateDijagnozaDTO createDijagnozaDTO, int requestingDoktorId)
        {
            var pregled = await _db.Pregledi.FindAsync(pregledId);
            if (pregled == null) 
                throw new ArgumentException("Pregled ne postoji");
            if (pregled.DoktorId != requestingDoktorId) 
                throw new UnauthorizedAccessException("Niste ovlasteni za ovaj pregled");
            if (await _db.Dijagnoze.AnyAsync(d => d.PregledId == pregledId))
                throw new InvalidOperationException("Dijagnoza za ovaj pregled vec postoji");

            var dijagnoza = _mapper.Map<Dijagnoza>(createDijagnozaDTO);
            dijagnoza.PregledId = pregled.Id;
            _db.Dijagnoze.Add(dijagnoza);
            await _db.SaveChangesAsync();
            return _mapper.Map<DijagnozaDTO.ReadDijagnozaDTO>(dijagnoza);
        }

        // Update dijagnoza
        public async Task<DijagnozaDTO.ReadDijagnozaDTO> UpdateDijagnoza(int id,DijagnozaDTO.UpdateDijagnozaDTO updateDijagnozaDTO)
        {
            var updatedDijagnoza = await _db.Dijagnoze.FindAsync(id);
            if(updatedDijagnoza == null)
            {
                return null;
            }
            _mapper.Map(updateDijagnozaDTO, updatedDijagnoza);

            await _db.SaveChangesAsync();

            return _mapper.Map<DijagnozaDTO.ReadDijagnozaDTO>(updatedDijagnoza);
        }
    }
}
