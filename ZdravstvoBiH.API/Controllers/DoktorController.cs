using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.Interfaces;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoktorController : Controller
    {
        private readonly IDoktorService _doktorService;

        public DoktorController(IDoktorService doktorService)
        {
            _doktorService = doktorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doktori = await _doktorService.GetAllDoktors();
            return Ok(doktori);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoktorById(int id)
        {
            var doktor = await _doktorService.GetDoktorById(id);
            if (doktor == null) return NotFound();
            return Ok(doktor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoktor([FromBody] Zdravstvo.Core.DTOs.DoktorDTO.CreateDoktorDTO createDoktorDTO)
        {
            var noviDoktor = await _doktorService.CreateDoktor(createDoktorDTO);
            return CreatedAtAction(nameof(GetDoktorById), new { id = noviDoktor.Id }, noviDoktor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoktor(int id, [FromBody] Zdravstvo.Core.DTOs.DoktorDTO.UpdateDoktorDTO updateDoktorDTO)
        {
            try
            {
                var azuriraniDoktor = await _doktorService.UpdateDoktor(id, updateDoktorDTO);
                return Ok(azuriraniDoktor);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
