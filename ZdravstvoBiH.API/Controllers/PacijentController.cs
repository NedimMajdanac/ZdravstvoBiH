using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacijentController : Controller
    {
        private readonly IPacijentService _pacijentService;
        public PacijentController(IPacijentService pacijentService)
        {
            _pacijentService = pacijentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pacijenti = await _pacijentService.GetAllPacijenti();
            return Ok(pacijenti);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacijentById(int id)
        {
            var pacijent = await _pacijentService.GetPacijentById(id);
            if (pacijent == null) return NotFound();

            return Ok(pacijent);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePacijent([FromBody] PacijentDTO.CreatePacijentDTO createPacijentDTO)
        {
            var noviPacijent = await _pacijentService.CreatePacijent(createPacijentDTO);
            return CreatedAtAction(nameof(GetPacijentById), new { id = noviPacijent.Id }, noviPacijent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePacijent(int id, [FromBody] PacijentDTO.UpdatePacijentDTO updatePacijentDTO)
        {
            try
            {
                var azuriraniPacijent = await _pacijentService.UpdatePacijent(id, updatePacijentDTO);
                return Ok(azuriraniPacijent);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
