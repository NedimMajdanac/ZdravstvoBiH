using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Interfaces;

namespace ZdravstvoBiH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecijalizacijaController : Controller
    {
        private readonly ISpecijalizacijaService _specijalizacijaService;

        public SpecijalizacijaController(ISpecijalizacijaService specijalizacijaService)
        {
            _specijalizacijaService = specijalizacijaService;
        }

        [HttpGet]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var specijalizacije = await _specijalizacijaService.GetAll();
                return Ok(specijalizacije);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var spec = await _specijalizacijaService.GetById(id);
                return Ok(spec);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSpecijalizaciju([FromBody] SpecijelizacijaDTO.CreateSpecijalizacijaDTO specijalizacijaDTO)
        {
            try
            {
                var newSpec = await _specijalizacijaService.CreateSpecijalizacija(specijalizacijaDTO);
                return CreatedAtAction(nameof(GetById), new { Id = newSpec.Id }, newSpec);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSpecijalizacija([FromBody] SpecijelizacijaDTO.UpdateSpecijalizacijaDTO specijalizacijaDTO,int id)
        {
            try
            {
                var updatedSpec = await _specijalizacijaService.UpdateSpecijalizacija(specijalizacijaDTO, id);
                return Ok(updatedSpec);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
