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
        public async Task<DijagnozaDTO.ReadDijagnozaDTO> CreateDijagnoza(DijagnozaDTO.CreateDijagnozaDTO createDijagnozaDTO)
        {
            var dijagnoza =  _mapper.Map<Dijagnoza>(createDijagnozaDTO);

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
