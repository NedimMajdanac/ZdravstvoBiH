using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IUstanovaService
    {
        Task<List<UstanovaDTO.ReadUstanovaDTO>> GetAllUstanove();
        Task<UstanovaDTO.ReadUstanovaDTO> GetUstanovaById(int id);
        Task<UstanovaDTO.ReadUstanovaDTO> CreateUstanova(UstanovaDTO.CreateUstanovaDTO createDto);
        Task<UstanovaDTO.ReadUstanovaDTO> UpdateUstanova(int id, UstanovaDTO.UpdateUstanovaDTO updateDto);
    }
}
