using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IReceptService
    {
        Task<List<ReceptDTO.ReadReceptDTO>> GetAll();
        Task<ReceptDTO.ReadReceptDTO> GetReceptById(int id);
        Task<ReceptDTO.ReadReceptDTO> CreateReceptForPregled(int pregledId, ReceptDTO.CreateReceptDTO createReceptDTO, int requestingDoktorId);
        Task<ReceptDTO.ReadReceptDTO> UpdateRecept(int receptId, ReceptDTO.UpdateReceptDTO updateReceptDTO);

    }
}
