using Microsoft.AspNetCore.Mvc;
using MusicSchools.Application.Services;
using MusicSchools.Domain.Entities;

namespace MusicSchools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicSchoolsController : ControllerBase
    {
        private readonly MusicSchoolService _service;
        public MusicSchoolsController(MusicSchoolService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MusicSchool school)
        {
            await _service.AddAsync(school);
            return CreatedAtAction(nameof(Get), new { id = school.Id }, school);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, MusicSchool updated)
        {
            if (id != updated.Id) return BadRequest();
            await _service.UpdateAsync(updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
