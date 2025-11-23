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
    public class UputnicaService : IUputnicaService
    {
        private readonly IMapper _mapper;
        private readonly ZdravstvoContext _db;
        
        public UputnicaService(IMapper mapper,ZdravstvoContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        // Get All Uputnice
        public async Task<List<UputnicaDTO.ReadUputnicaDTO>> GetAllUputnice()
        {
            var uputnice = await _db.Uputnice.ToListAsync();
            return _mapper.Map<List<UputnicaDTO.ReadUputnicaDTO>>(uputnice);
        }

        // Get Uputnica by Id
        public async Task<UputnicaDTO.ReadUputnicaDTO> GetUputnicaById(int id)
        {
            var uputnica = await _db.Uputnice.FindAsync(id);
            if(uputnica == null) return null;
            return _mapper.Map<UputnicaDTO.ReadUputnicaDTO>(uputnica);
        }
        // Create Uputnica
        public async Task<UputnicaDTO.ReadUputnicaDTO> CreateUputnica(UputnicaDTO.CreateUputnicaDTO createUputnicaDTO)
        {
            var uputnica = _mapper.Map<Uputnica>(createUputnicaDTO);
            _db.Uputnice.Add(uputnica);
            await _db.SaveChangesAsync();
            return _mapper.Map<UputnicaDTO.ReadUputnicaDTO>(uputnica);
        }
    }
}
