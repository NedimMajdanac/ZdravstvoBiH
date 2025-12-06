using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Helpers;

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
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> GetAll()
        {
            var dijagnoze = await _dijagnozaService.GetAll();
            return Ok(dijagnoze);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> GetById(int id)
        {
            var dijagnoza = await _dijagnozaService.GetDijagnozaById(id);
            if(dijagnoza == null)
            {
                return NotFound();
            }
            return Ok(dijagnoza);
        }
        [HttpPost("pregledi/{pregledId}/dijagnoza")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> CreateDijagnozaForPregled(int pregledId, [FromBody] DijagnozaDTO.CreateDijagnozaDTO createDijagnozaDTO, int doktorId)
        {
            try
            {
                var reqDoktorId = User.GetDoktorId();
                var result = await _dijagnozaService.CreateDijagnozaForPregled(pregledId, createDijagnozaDTO, reqDoktorId);
                return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Doktor")]
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
