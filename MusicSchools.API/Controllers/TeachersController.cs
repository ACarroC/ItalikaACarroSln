using Microsoft.AspNetCore.Mvc;
using MusicSchools.Application.Services;
using MusicSchools.Domain.Entities;

namespace MusicSchools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : Controller
    {
        private readonly TeacherService _service;
        public TeachersController(TeacherService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var s = await _service.GetByIdAsync(id);
            return s is null ? NotFound() : Ok(s);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher s)
        {
            await _service.AddAsync(s);
            return CreatedAtAction(nameof(Get), new { id = s.Id }, s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Teacher s)
        {
            if (id != s.Id) return BadRequest();
            await _service.UpdateAsync(s);
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
