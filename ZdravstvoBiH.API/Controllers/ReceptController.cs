using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Helpers;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptController : Controller
    {
        private readonly IReceptService _receptService;
        public ReceptController(IReceptService receptService)
        {
            _receptService = receptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recepti = await _receptService.GetAll();
            return Ok(recepti);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recept = await _receptService.GetReceptById(id);
            if (recept == null)
                return NotFound();
            return Ok(recept);
        }
        [HttpPost("pregledi/{pregledId}/recepti")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> CreateRecept(int pregledId, [FromBody] ReceptDTO.CreateReceptDTO createReceptDTO)
        {
            try
            {
                var reqDoktorId = User.GetDoktorId();
                var result = await _receptService.CreateReceptForPregled(pregledId, createReceptDTO, reqDoktorId);
                return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
            }
            catch(UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecept(int id, [FromBody] ReceptDTO.UpdateReceptDTO updateReceptDTO)
        {
            var recept = await _receptService.UpdateRecept(id, updateReceptDTO);
            if(recept == null)
                return NotFound();
            return Ok(recept);
        }

    }
}
