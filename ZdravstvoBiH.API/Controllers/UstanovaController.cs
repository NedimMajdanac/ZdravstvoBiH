using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.Interfaces;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UstanovaController : Controller
    {
        private readonly IUstanovaService _ustanovaService;

        public UstanovaController(IUstanovaService ustanovaService)
        {
            _ustanovaService = ustanovaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUstanove()
        {
            var ustanove = await _ustanovaService.GetAllUstanove();
            return Ok(ustanove);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUstanovaById(int id)
        {
            var ustanova = await _ustanovaService.GetUstanovaById(id);
            if (ustanova == null)
            {
                return NotFound();
            }
            return Ok(ustanova);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUstanova([FromBody] Zdravstvo.Core.DTOs.UstanovaDTO.CreateUstanovaDTO createDto)
        {
            var ustanova = await _ustanovaService.CreateUstanova(createDto);
            return CreatedAtAction(nameof(GetUstanovaById), new { id = ustanova.id }, ustanova);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUstanova(int id, [FromBody] Zdravstvo.Core.DTOs.UstanovaDTO.UpdateUstanovaDTO updateDto)
        {
            var ustanova = await _ustanovaService.UpdateUstanova(id, updateDto);
            if (ustanova == null)
            {
                return NotFound();
            }
            return Ok(ustanova);
        }
    }
}
