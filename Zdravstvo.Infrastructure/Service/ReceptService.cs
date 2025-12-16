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
    public class ReceptService : IReceptService
    {
        private readonly ZdravstvoContext _db;
        private readonly IMapper _mapper;
        public ReceptService(ZdravstvoContext db, IMapper mapper)
        {
            _db = db;   
            _mapper = mapper;
        }

        // Get all
        public async Task<List<ReceptDTO.ReadReceptDTO>> GetAll()
        {
            var recepti = await _db.Recepti.ToListAsync();
            return _mapper.Map<List<ReceptDTO.ReadReceptDTO>>(recepti);
        }

        // Get by id
        public async Task<ReceptDTO.ReadReceptDTO> GetReceptById(int id)
        {
            var recept = await _db.Recepti.FindAsync(id);
            if (recept == null)
                throw new ArgumentException("Recept ne postoji");
            return _mapper.Map<ReceptDTO.ReadReceptDTO>(recept);
        }

        // Create Recept for pregled
        public async Task<ReceptDTO.ReadReceptDTO> CreateReceptForPregled(int pregledId, ReceptDTO.CreateReceptDTO createReceptDTO,int requestingDoktorId)
        {
            var pregled = await _db.Pregledi.FindAsync(pregledId);
            if (pregled == null)
                throw new ArgumentException("Pregled ne postoji");
            if (pregled.DoktorId != requestingDoktorId)
                throw new UnauthorizedAccessException("Niste ovlasteni za izdavanje recepta");
            if (await _db.Recepti.AnyAsync(r => r.PregledId == pregled.Id))
                throw new InvalidOperationException("Recept za ovaj pregled vec postoji");

            var recept = _mapper.Map<Recepti>(createReceptDTO);
            recept.PregledId = pregledId;   
            _db.Recepti.Add(recept);
            await _db.SaveChangesAsync();
            return _mapper.Map<ReceptDTO.ReadReceptDTO>(recept);
        }

        // Update recept
        public async Task<ReceptDTO.ReadReceptDTO> UpdateRecept(int receptId, ReceptDTO.UpdateReceptDTO updateReceptDTO)
        {
            var updatedRecept = await _db.Recepti.FindAsync(receptId);
            if (updatedRecept == null)
                throw new ArgumentException("Recept ne postoji");
            _mapper.Map(updateReceptDTO, updatedRecept);
            await _db.SaveChangesAsync();
            return _mapper.Map<ReceptDTO.ReadReceptDTO>(updatedRecept);
        }

        // GET recepti by pacijent id
        public async Task<List<ReceptDTO.ReadReceptDTO>> GetReceptiForPacijent(int pacijentId)
        {
            var recepti = await _db.Recepti
                .Include(r => r.Pregled)
                .Where(r => r.Pregled.PacijentId == pacijentId)
                .ToListAsync();
            return _mapper.Map<List<ReceptDTO.ReadReceptDTO>>(recepti);
        }
    }
}
