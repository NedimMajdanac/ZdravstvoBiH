using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IMedicinskiKartonService
    {
        Task<List<MedicinskiKartonDTO.ReadMedicinskiKartonDTO>> GetAllMedKartoni();
        Task<MedicinskiKartonDTO.ReadMedicinskiKartonDTO> GetMedKartonById(int id);
        Task<MedicinskiKartonDTO.ReadMedicinskiKartonDTO> CreateMedicinskiKarton(MedicinskiKartonDTO.CreateMedicinskiKartonDTO createMedicinskiKarton);
        Task<MedicinskiKartonDTO.ReadMedicinskiKartonDTO> UpdateMedicinskiKarton(int id, MedicinskiKartonDTO.UpdateMedicinskiKartonDTO updateMedicinskiKarton);
    }
}
