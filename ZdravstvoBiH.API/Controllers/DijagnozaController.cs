using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DijagnozaController : Controller
    {
        private readonly IDijagnozaService _dijagnozaService;
        public DijagnozaController(IDijagnozaService dijagnozaService)
        {
            _dijagnozaService = dijagnozaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dijagnoze = await _dijagnozaService.GetAll();
            return Ok(dijagnoze);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dijagnoza = await _dijagnozaService.GetDijagnozaById(id);
            if(dijagnoza == null)
            {
                return NotFound();
            }
            return Ok(dijagnoza);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDijagnoza([FromBody] DijagnozaDTO.CreateDijagnozaDTO createDijagnozaDTO)
        {
            var newDijagnoza = await _dijagnozaService.CreateDijagnoza(createDijagnozaDTO);
            return CreatedAtAction(nameof(GetById), new {id = newDijagnoza.Id}, newDijagnoza);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDijagnoza(int id, [FromBody] DijagnozaDTO.UpdateDijagnozaDTO updateDijagnozaDTO)
        {
            var updatedDijagnoza = await _dijagnozaService.UpdateDijagnoza(id, updateDijagnozaDTO);
            if(updatedDijagnoza == null)
            {
                return NotFound();
            }
            return Ok(updatedDijagnoza);
        }
    }
}
