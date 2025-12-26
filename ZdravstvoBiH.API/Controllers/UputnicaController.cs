using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Helpers;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UputnicaController : Controller
    {
        private readonly IUputnicaService _uputnicaService;
        public UputnicaController(IUputnicaService uputnicaService)
        {
            _uputnicaService = uputnicaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUputnice()
        {
            var uputnice = await _uputnicaService.GetAllUputnice();
            return Ok(uputnice);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUputnicaById(int id)
        {
            var uputnica = await _uputnicaService.GetUputnicaById(id);
            if (uputnica == null)
            {
                return NotFound();
            }
            return Ok(uputnica);
        }

        [HttpPost("/{terminId}")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> CreateUputnica(int terminId, [FromBody] UputnicaDTO.CreateUputnicaDTO createUputnicaDTO)
        {
            var doktor = User.GetDoktorId();
            var uputnica = await _uputnicaService.CreateUputnicaForTermin(terminId,createUputnicaDTO, doktor);
            return CreatedAtAction(nameof(GetUputnicaById), new { id = uputnica.Id }, uputnica);
        }
        [HttpPost("pacijenti/{pacijentId}")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> CreateUputnicaForPacijent(int pacijentId, [FromBody] UputnicaDTO.CreateUputnicaDTO createUputnicaDTO)
        {
            try
            {
                var uputnice = await _uputnicaService.GetUputniceForPacijent(pacijentId);
                return Ok(uputnice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
