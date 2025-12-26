using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface ITerminService
    {
        Task<PagedResult<TerminDTO.ReadTerminDTO>> GetAllTermini(string search, PagingParams paging);
        Task<TerminDTO.ReadTerminDTO> GetTerminById(int id);
        Task<TerminDTO.ReadTerminDTO> CreateTermin(TerminDTO.CreateTerminDTO createTerminDTO);
        Task<TerminDTO.ReadTerminDTO> UpdateTermin(int id, TerminDTO.UpdateTerminDTO updateTerminDTO);
        Task<bool> DeleteTermin(int id);
        Task<List<TerminDTO.ReadTerminDTO>> GetTerminiByPacijentId(int pacijentId);
        Task<List<TerminDTO.ReadTerminDTO>> GetTerminiByDoktorId(int doktorId);
        Task<List<TerminDTO.ReadTerminDTO>> GetTerminiByUstanovaId(int ustanovaId);
    }
}
