using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IDoktorService
    {
        Task<List<DoktorDTO.ReadDoktorDTO>> GetAllDoktors();
        Task<DoktorDTO.ReadDoktorDTO> GetDoktorById(int id);
        Task<DoktorDTO.ReadDoktorDTO> CreateDoktor(DoktorDTO.CreateDoktorDTO createDoktorDTO);
        Task<DoktorDTO.ReadDoktorDTO> UpdateDoktor(int id, DoktorDTO.UpdateDoktorDTO updateDoktorDTO);
        Task<DoktorDTO.ReadDoktorDTO> GetLoggedDoktor(int korisnikId);
        Task<DoktorDTO.ReadDoktorDTO> UpdateCurrentDoktor(int korisnikId, DoktorDTO.UpdateDoktorDTO doktorDTO);
    }
}
