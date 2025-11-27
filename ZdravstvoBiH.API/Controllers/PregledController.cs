using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PregledController : Controller
    {
        private readonly IPregledService _pregledService;
        public PregledController(IPregledService pregledService)
        {
            _pregledService = pregledService;
        }

        // Get all pregledi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pregledi = await _pregledService.GetAllPregledi();
            return Ok(pregledi);
        }

        // Get pregled by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var pregled = await _pregledService.GetPregledById(Id);
            if (pregled == null)
            {
                return NotFound();
            }
            return Ok(pregled);
        }

        // Create new pregled
        [HttpPost]
        public async Task<IActionResult> CreatePregled([FromBody] PregledDTO.CreatePregledDTO createPregledDTO)
        {
            var newPregled = await _pregledService.CreatePregled(createPregledDTO);
            return CreatedAtAction(nameof(GetById), new { id = newPregled.Id }, newPregled);
        }

        // Update existing pregled
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePregled(int id, [FromBody] PregledDTO.UpdatePregledDTO updatePregledDTO)
        {
            var updatedPregled = await _pregledService.UpdatePregled(id, updatePregledDTO);
            if (updatedPregled == null)
            {
                return NotFound();
            }
            return Ok(updatedPregled);
        }
    }
}
