using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUser(DTOs.KorisnikDTO.RegisterKorisnikDTO registerKorisnikDTO);
        Task<string> LoginUser(DTOs.KorisnikDTO.LoginKorisnikDTO loginKorisnikDTO);
        Task<string> RegisterUserForProfile(KorisnikDTO.RegisterKorisnikForProfile registerKorisnikForProfileDTO);
        Task<KorisnikDTO.CurrentUserDTO> GetLoggedUser(int korisnikId);
    }
}
