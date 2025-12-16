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
    public class SpecijelizacijaService : ISpecijalizacijaService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;

        public SpecijelizacijaService(ZdravstvoContext db,IMapper mapper, ValidationService validationService)
        {
            _db = db;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<List<SpecijelizacijaDTO.ReadSpecijalizacijaDTO>> GetAll()
        {
            var specijalizacije = await _db.Specijalizacija.ToListAsync();
            return _mapper.Map<List<SpecijelizacijaDTO.ReadSpecijalizacijaDTO>>(specijalizacije);
        }

        public async Task<SpecijelizacijaDTO.ReadSpecijalizacijaDTO> GetById(int id)
        {
            var spec = await _db.Specijalizacija.FindAsync(id);
            if (spec == null) return null;
            return _mapper.Map<SpecijelizacijaDTO.ReadSpecijalizacijaDTO>(spec);
        }

        public async Task<SpecijelizacijaDTO.ReadSpecijalizacijaDTO> CreateSpecijalizacija(SpecijelizacijaDTO.CreateSpecijalizacijaDTO specijalizacijaDTO)
        {
            await _validationService.ValidateSpecijalizaciju(specijalizacijaDTO.Naziv);
            var spec = _mapper.Map<Specijalizacija>(specijalizacijaDTO);
            _db.Specijalizacija.Add(spec);
            await _db.SaveChangesAsync();
            return _mapper.Map<SpecijelizacijaDTO.ReadSpecijalizacijaDTO>(spec);
        }

        public async Task<SpecijelizacijaDTO.ReadSpecijalizacijaDTO> UpdateSpecijalizacija(SpecijelizacijaDTO.UpdateSpecijalizacijaDTO specijalizacijaDTO, int id)
        {
            var spec = await _db.Specijalizacija.FindAsync(id);
            if (spec == null) return null;
            _mapper.Map(specijalizacijaDTO, spec);
            await _db.SaveChangesAsync();
            return _mapper.Map<SpecijelizacijaDTO.ReadSpecijalizacijaDTO>(spec);
        }

    }
}
