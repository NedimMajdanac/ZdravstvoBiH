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
    public class MedicinskiKartonService : IMedicinskiKartonService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;

        public MedicinskiKartonService(ZdravstvoContext db, IMapper mapper, ValidationService validationService)
        {
            _db = db;
            _mapper = mapper;
            _validationService = validationService;
        }

        // Get all MedicinskiKartons
        public async Task<List<MedicinskiKartonDTO.ReadMedicinskiKartonDTO>> GetAllMedKartoni()
        {
            var medKartoni = await _db.MedicinskiKartoni.ToListAsync();
            return _mapper.Map<List<MedicinskiKartonDTO.ReadMedicinskiKartonDTO>>(medKartoni);
        }

        // Get MedicinskiKarton by ID
        public async Task<MedicinskiKartonDTO.ReadMedicinskiKartonDTO> GetMedKartonById(int id)
        {
            var medKarton = await _db.MedicinskiKartoni.FindAsync(id);
            if (medKarton == null) return null;
            return _mapper.Map<MedicinskiKartonDTO.ReadMedicinskiKartonDTO>(medKarton);
        }

        // Create MedicinskiKarton
        public async Task<MedicinskiKartonDTO.ReadMedicinskiKartonDTO> CreateMedicinskiKarton(MedicinskiKartonDTO.CreateMedicinskiKartonDTO createMedicinskiKarton)
        {
            await _validationService.ValidateKrvnaGrupa(createMedicinskiKarton.KrvnaGrupa);

            var medKarton = _mapper.Map<MedicinskiKarton>(createMedicinskiKarton);
            
            _db.MedicinskiKartoni.Add(medKarton);
            await _db.SaveChangesAsync();
            return _mapper.Map<MedicinskiKartonDTO.ReadMedicinskiKartonDTO>(medKarton);
        }

        // Update MedicinskiKarton
        public async Task<MedicinskiKartonDTO.ReadMedicinskiKartonDTO> UpdateMedicinskiKarton(int id,MedicinskiKartonDTO.UpdateMedicinskiKartonDTO updateMedicinskiKarton)
        {
            var medKarton = await _db.MedicinskiKartoni.FindAsync(id);
            if (medKarton == null) return null;
            _mapper.Map(updateMedicinskiKarton, medKarton);
            await _db.SaveChangesAsync();
            return _mapper.Map<MedicinskiKartonDTO.ReadMedicinskiKartonDTO>(medKarton);
        }
    }
}
