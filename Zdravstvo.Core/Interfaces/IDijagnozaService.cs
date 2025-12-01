using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IDijagnozaService
    {
        Task<List<DijagnozaDTO.ReadDijagnozaDTO>> GetAll();
        Task<DijagnozaDTO.ReadDijagnozaDTO> GetDijagnozaById(int id);
        Task<DijagnozaDTO.ReadDijagnozaDTO> CreateDijagnozaForPregled(int pregledId, DijagnozaDTO.CreateDijagnozaDTO createDijagnozaDTO, int requestingDoktorId);
        Task<DijagnozaDTO.ReadDijagnozaDTO> UpdateDijagnoza(int id, DijagnozaDTO.UpdateDijagnozaDTO updateDijagnozaDTO);
    }
}
