using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Helpers;

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
        [HttpPost("termini/{terminId}/pregled")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> CreatePregledForTermin(int terminId, [FromBody] PregledDTO.CreatePregledDTO createPregledDTO)
        {

            try
            {
                var doktorId = User.GetDoktorId();
                var result = await _pregledService.CreatePregledForTermin(terminId, createPregledDTO, doktorId);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            } catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

        }

        // Update existing pregled
        [HttpPut("{id}")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> UpdatePregled(int id, [FromBody] PregledDTO.UpdatePregledDTO updatePregledDTO)
        {
            var updatedPregled = await _pregledService.UpdatePregled(id, updatePregledDTO);
            if (updatedPregled == null)
            {
                return NotFound();
            }
            return Ok(updatedPregled);
        }

        [HttpGet("pacijent/{pacijentId}")]
        [Authorize(Roles = "Pacijent")]
        public async Task<IActionResult> GetPreglediLoggedPacijent(int pacijentId)
        {
            try
            {
                var pregledPacijent = await _pregledService.GetPreglediForLoggedPacient(pacijentId);
                return Ok(pregledPacijent);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("doktor/{doktorId}")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> GetPreglediLoggedDoctor(int doktorId)
        {
            try
            {
                var pregledDoktor = await _pregledService.GetPreglediForLoggedDoktor(doktorId);
                return Ok(pregledDoktor);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
