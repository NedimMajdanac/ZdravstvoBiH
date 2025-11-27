using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IPregledService
    {
        Task<List<PregledDTO.ReadPregledDTO>> GetAllPregledi();
        Task<PregledDTO.ReadPregledDTO> GetPregledById(int id);
        Task<PregledDTO.ReadPregledDTO> CreatePregled(PregledDTO.CreatePregledDTO createPregledDTO);
        Task<PregledDTO.ReadPregledDTO> UpdatePregled(int id, PregledDTO.UpdatePregledDTO updatePregledDTO);
    }
}
