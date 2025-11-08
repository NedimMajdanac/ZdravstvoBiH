using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    public class DoktorService : IDoktorService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;

        public DoktorService(ZdravstvoContext db,IMapper mapper,ValidationService validationService)
        {
            _db = db;
            _mapper = mapper;
            _validationService = validationService;
        }

        // Get All Doktori
        public async Task<List<DoktorDTO.ReadDoktorDTO>> GetAllDoktors()
        {
            var doktori = await _db.Doktori.ToListAsync();
            return _mapper.Map<List<DoktorDTO.ReadDoktorDTO>>(doktori);
        }

        // Get Doktor by ID
        public async Task<DoktorDTO.ReadDoktorDTO> GetDoktorById(int id)
        {
            var doktor = await _db.Doktori.FindAsync(id);
            if (doktor == null) return null;

            return _mapper.Map<DoktorDTO.ReadDoktorDTO>(doktor);
        }
        
        // Update Doktor
        public async Task<DoktorDTO.ReadDoktorDTO> CreateDoktor(DoktorDTO.CreateDoktorDTO createDoktorDTO)
        {
            var doktor = _mapper.Map<Doktor>(createDoktorDTO);

            _db.Doktori.Add(doktor);

            await _db.SaveChangesAsync();

            return _mapper.Map<DoktorDTO.ReadDoktorDTO>(doktor);
        }
            
        // Create Doktor
        public async Task<DoktorDTO.ReadDoktorDTO> UpdateDoktor(int id, DoktorDTO.UpdateDoktorDTO updateDoktorDTO)
        {
            var doktor = await _db.Doktori.FindAsync(id);
            if (doktor == null) return null;
            
            _mapper.Map(updateDoktorDTO, doktor);

            await _db.SaveChangesAsync();
            return _mapper.Map<DoktorDTO.ReadDoktorDTO>(doktor);
        }

    }
}
