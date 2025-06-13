using Microsoft.EntityFrameworkCore;
using MusicSchools.Domain.Entities;
using MusicSchools.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Application.Services
{
    public class EnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task EnrollStudentInSchool(Guid studentId, Guid schoolId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student is null) throw new Exception("Alumno no encontrado");

            student.MusicSchoolId = schoolId;
            await _context.SaveChangesAsync();
        }

        public async Task EnrollStudentInTeachers(Guid studentId, List<Guid> teacherIds)
        {
            var existing = await _context.StudentTeachers
                .Where(st => st.StudentId == studentId)
                .ToListAsync();

            _context.StudentTeachers.RemoveRange(existing); // limpia los anteriores

            foreach (var teacherId in teacherIds)
            {
                _context.StudentTeachers.Add(new StudentTeacher
                {
                    StudentId = studentId,
                    TeacherId = teacherId
                });
            }

            await _context.SaveChangesAsync();
        }
    }

}
