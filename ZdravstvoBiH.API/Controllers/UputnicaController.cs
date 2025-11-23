using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> CreateUputnica([FromBody] Zdravstvo.Core.DTOs.UputnicaDTO.CreateUputnicaDTO createUputnicaDTO)
        {
            var uputnica = await _uputnicaService.CreateUputnica(createUputnicaDTO);
            return CreatedAtAction(nameof(GetUputnicaById), new { id = uputnica.Id }, uputnica);
        }

    }
}
