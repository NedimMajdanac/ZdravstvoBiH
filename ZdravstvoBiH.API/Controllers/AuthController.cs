using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Helpers;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(KorisnikDTO.RegisterKorisnikDTO registerKorisnikDTO)
        {
            var token = await _authService.RegisterUser(registerKorisnikDTO);
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(KorisnikDTO.LoginKorisnikDTO loginKorisnikDTO)
        {
            var token = await _authService.LoginUser(loginKorisnikDTO);
            return Ok(new { Token = token });
        }
        [HttpPost("/register/register-for-profile")]
        public async Task<IActionResult> RegisterUserProfile(KorisnikDTO.RegisterKorisnikForProfile dto)
        {
            var token = await _authService.RegisterUserForProfile(dto);
            return Ok(new { Token = token });
        }
        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> CurrentUser()
        {
            var korisnikId = User.GetKorisnikId();
            var currentUser = await _authService.GetLoggedUser(korisnikId);
            if (currentUser == null)
            {
                return NotFound();
            }
            return Ok(currentUser);
        }
    }
}
