using MusicSchools.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Application.Services
{
    public class AssignmentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ITeacherRepository _teacherRepo;

        public AssignmentService(IStudentRepository sRepo, ITeacherRepository tRepo)
        {
            _studentRepo = sRepo;
            _teacherRepo = tRepo;
        }

        public async Task AssignStudentsToTeacher(Guid teacherId, List<Guid> studentIds)
        {
            var teacher = await _teacherRepo.GetByIdAsync(teacherId);
            if (teacher is null)
                throw new Exception("Profesor no encontrado");

            foreach (var studentId in studentIds)
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student is not null)
                {
                    student.TeacherId = teacherId;
                    await _studentRepo.UpdateEstudentAsync(student);
                }
            }
        }
    }
}
