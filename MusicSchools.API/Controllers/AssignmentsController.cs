using Microsoft.AspNetCore.Mvc;
using MusicSchools.Application.Services;

namespace MusicSchools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly AssignmentService _service;

        public AssignmentsController(AssignmentService service)
        {
            _service = service;
        }

        public class AssignStudentsRequest
        {
            public Guid TeacherId { get; set; }
            public List<Guid> StudentIds { get; set; } = new();
        }

        [HttpPost("assign-students")]
        public async Task<IActionResult> AssignStudents([FromBody] AssignStudentsRequest request)
        {
            await _service.AssignStudentsToTeacher(request.TeacherId, request.StudentIds);
            return Ok(new { message = "Estudiantes asignados correctamente." });
        }

    }
}

