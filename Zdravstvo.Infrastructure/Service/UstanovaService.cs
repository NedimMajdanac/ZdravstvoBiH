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
    public class UstanovaService : IUstanovaService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;

        public UstanovaService(ZdravstvoContext db, IMapper mapper, ValidationService validationService)
        {
            _db = db;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<List<UstanovaDTO.ReadUstanovaDTO>> GetAllUstanove()
        {
            var ustanove = await _db.Ustanove.ToListAsync();
            return _mapper.Map<List<UstanovaDTO.ReadUstanovaDTO>>(ustanove);
        }

        public async Task<UstanovaDTO.ReadUstanovaDTO> GetUstanovaById(int id)
        {
            var ustanova = await _db.Ustanove.FindAsync(id);
            return _mapper.Map<UstanovaDTO.ReadUstanovaDTO>(ustanova);
        }

        public async Task<UstanovaDTO.ReadUstanovaDTO> CreateUstanova(UstanovaDTO.CreateUstanovaDTO createDto)
        {

            await _validationService.ValidateUstanove(createDto.Adresa, createDto.Tip);
            await _validationService.ValidateBrojTelefonaUstanove(createDto.Telefon);

            var ustanova = _mapper.Map<Ustanova>(createDto);
            _db.Ustanove.Add(ustanova);
            await _db.SaveChangesAsync();
            return _mapper.Map<UstanovaDTO.ReadUstanovaDTO>(ustanova);
        }

        public async Task<UstanovaDTO.ReadUstanovaDTO> UpdateUstanova(int id, UstanovaDTO.UpdateUstanovaDTO updateDto)
        {
            await _validationService.ValidateBrojTelefonaUstanove(updateDto.Telefon);

            var ustanova = await _db.Ustanove.FindAsync(id);
            if (ustanova == null)
            {
                return null;
            }
            _mapper.Map(updateDto, ustanova);
            await _db.SaveChangesAsync();
            return _mapper.Map<UstanovaDTO.ReadUstanovaDTO>(ustanova);
        }
    }
}
