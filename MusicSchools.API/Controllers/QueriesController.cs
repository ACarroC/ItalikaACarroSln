using Microsoft.AspNetCore.Mvc;
using MusicSchools.Application.Services;

namespace MusicSchools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueriesController : ControllerBase
    {
        private readonly QueryService _queryService;

        public QueriesController(QueryService queryService)
        {
            _queryService = queryService;
        }

        /// Consulta 1: alumnos por profesor
        [HttpGet("students-by-teacher/{teacherId}")]
        public async Task<IActionResult> GetStudentsByTeacher(Guid teacherId)
        {
            var result = await _queryService.GetStudentsByTeacher(teacherId);
            return Ok(result);
        }

        /// Consulta 2: escuelas y alumnos por profesor
        [HttpGet("schools-and-students-by-teacher/{teacherId}")]
        public async Task<IActionResult> GetSchoolsAndStudentsByTeacher(Guid teacherId)
        {
            var result = await _queryService.GetSchoolsAndStudentsByTeacher(teacherId);
            return Ok(result);
        }
    }
}
