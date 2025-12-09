using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IPacijentService
    {
        Task<List<PacijentDTO.ReadPacijentDTO>> GetAllPacijenti();
        Task<PacijentDTO.ReadPacijentDTO> GetPacijentById(int id);
        Task<PacijentDTO.ReadPacijentDTO> CreatePacijent(PacijentDTO.CreatePacijentDTO createPacijentDTO);
        Task<PacijentDTO.ReadPacijentDTO> UpdatePacijent(int id, PacijentDTO.UpdatePacijentDTO updatePacijentDTO);
        Task<MedicinskiKartonDTO.ReadMedicinskiKartonDTO> UpdatePacijentKarton(int pacijentId, MedicinskiKartonDTO.UpdateMedicinskiKartonDTO kartonDTO);
        Task<PacijentDTO.ReadPacijentDTO> GetLoggedPacijent(int korisnikId);
        Task<PacijentDTO.ReadPacijentDTO> UpdateLoggedPacijent(int korisnikId, PacijentDTO.UpdatePacijentDTO pacijentDTO);
    }
}
