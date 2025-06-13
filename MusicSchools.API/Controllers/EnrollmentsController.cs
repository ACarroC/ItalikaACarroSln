using Microsoft.AspNetCore.Mvc;
using MusicSchools.Application.Services;

namespace MusicSchools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentService _service;

        public EnrollmentsController(EnrollmentService service)
        {
            _service = service;
        }

        public class EnrollSchoolRequest
        {
            public Guid StudentId { get; set; }
            public Guid SchoolId { get; set; }
        }

        public class EnrollTeachersRequest
        {
            public Guid StudentId { get; set; }
            public List<Guid> TeacherIds { get; set; } = new();
        }

        [HttpPost("enroll-school")]
        public async Task<IActionResult> EnrollInSchool([FromBody] EnrollSchoolRequest request)
        {
            await _service.EnrollStudentInSchool(request.StudentId, request.SchoolId);
            return Ok(new { message = "Alumno inscrito en escuela correctamente." });
        }

        [HttpPost("enroll-teachers")]
        public async Task<IActionResult> EnrollInTeachers([FromBody] EnrollTeachersRequest request)
        {
            await _service.EnrollStudentInTeachers(request.StudentId, request.TeacherIds);
            return Ok(new { message = "Alumno inscrito con profesores correctamente." });
        }
    }
}
