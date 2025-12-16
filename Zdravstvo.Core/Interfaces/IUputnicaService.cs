using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IUputnicaService
    {
        Task<List<UputnicaDTO.ReadUputnicaDTO>> GetAllUputnice();
        Task<UputnicaDTO.ReadUputnicaDTO> GetUputnicaById(int id);
        Task<List<UputnicaDTO.ReadUputnicaDTO>> GetUputniceForPacijent(int pacijentId);
        Task<UputnicaDTO.ReadUputnicaDTO> CreateUputnicaForTermin(int terminId, UputnicaDTO.CreateUputnicaDTO createUputnicaDTO, int requestingDoktor);  }
}
