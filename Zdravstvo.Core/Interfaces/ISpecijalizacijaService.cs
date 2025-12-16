using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface ISpecijalizacijaService
    {
        Task<List<SpecijelizacijaDTO.ReadSpecijalizacijaDTO>> GetAll();
        Task<SpecijelizacijaDTO.ReadSpecijalizacijaDTO> GetById(int id);
        Task<SpecijelizacijaDTO.ReadSpecijalizacijaDTO> CreateSpecijalizacija(SpecijelizacijaDTO.CreateSpecijalizacijaDTO specijalizacijaDTO);
        Task<SpecijelizacijaDTO.ReadSpecijalizacijaDTO> UpdateSpecijalizacija(SpecijelizacijaDTO.UpdateSpecijalizacijaDTO specijalizacijaDTO, int id);
    }
}
