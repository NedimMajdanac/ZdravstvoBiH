using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;
using System.Text.Json;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerminController : Controller
    {
        private readonly ITerminService _terminService;
        public TerminController(ITerminService terminService)
        {
            _terminService = terminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTermini([FromQuery] string search, [FromQuery] PagingParams paging)
        {
            var result = await _terminService.GetAllTermini(search, paging);
            var meta = new { result.TotalCount, result.Page, result.PageSize, result.TotalPages };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(meta));
            return Ok(result.Items);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTerminById(int id)
        {
            var termin = await _terminService.GetTerminById(id);
            if (termin == null)
            {
                return NotFound();
            }
            return Ok(termin);
        }

        [HttpGet("pacijent/{pacijentId}")]
        public async Task<IActionResult> GetTerminiByPacijentId(int pacijentId)
        {
            var termini = await _terminService.GetTerminiByPacijentId(pacijentId);
            return Ok(termini);
        }

        [HttpGet("doktor/{doktorId}")]
        public async Task<IActionResult> GetTerminiByDoktorId(int doktorId)
        {
            var termini = await _terminService.GetTerminiByDoktorId(doktorId);
            return Ok(termini);
        }

        [HttpGet("ustanova/{ustanovaId}")]
        public async Task<IActionResult> GetTerminiByUstanovaId(int ustanovaId)
        {
            var termini = await _terminService.GetTerminiByUstanovaId(ustanovaId);
            return Ok(termini);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTermin([FromBody] TerminDTO.CreateTerminDTO createTerminDTO)
        {
            var noviTermin = await _terminService.CreateTermin(createTerminDTO);
            return Ok(noviTermin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTermin(int id, [FromBody] TerminDTO.UpdateTerminDTO updateTerminDTO)
        {
            var azuriraniTermin = await _terminService.UpdateTermin(id, updateTerminDTO);
            if (azuriraniTermin == null)
            {
                return NotFound();
            }
            return Ok(azuriraniTermin);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTermin(int id)
        {
            var rezultat = await _terminService.DeleteTermin(id);
            if (!rezultat)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
