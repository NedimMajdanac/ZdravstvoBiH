using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUser(DTOs.KorisnikDTO.RegisterKorisnikDTO registerKorisnikDTO);
        Task<string> LoginUser(DTOs.KorisnikDTO.LoginKorisnikDTO loginKorisnikDTO);
    }
}
