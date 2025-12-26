using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinskiKartonController : Controller
    {
        private readonly IMedicinskiKartonService _medicinskiKartonService;

        public MedicinskiKartonController(IMedicinskiKartonService medicinskiKartonService)
        {
            _medicinskiKartonService = medicinskiKartonService;
        }

        [HttpGet]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> GetAllMedicinskiKartoni()
        {
            var kartoni = await _medicinskiKartonService.GetAllMedKartoni();
            return Ok(kartoni);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> GetMedicinskiKartonById(int id)
        {
            var karton = await _medicinskiKartonService.GetMedKartonById(id);
            if (karton == null)
            {
                return NotFound();
            }
            return Ok(karton);
        }

        [HttpPost]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> CreateMedicinskiKarton([FromBody] MedicinskiKartonDTO.CreateMedicinskiKartonDTO createMedicinskiKarton)
        {
            var karton = await _medicinskiKartonService.CreateMedicinskiKarton(createMedicinskiKarton);
            return CreatedAtAction(nameof(GetMedicinskiKartonById), new { id = karton }, karton);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Doktor")]
        public async Task<IActionResult> UpdateMedicinskiKarton(int id, [FromBody] MedicinskiKartonDTO.UpdateMedicinskiKartonDTO updateMedicinskiKarton)
        {
            var karton = await _medicinskiKartonService.UpdateMedicinskiKarton(id, updateMedicinskiKarton);
            if (karton == null)
                return NotFound();
            return Ok(karton);
        }
    }
}
