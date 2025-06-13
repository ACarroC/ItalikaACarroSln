using Microsoft.EntityFrameworkCore;
using MusicSchools.Application.DTOs;
using MusicSchools.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Application.Services
{
    public class QueryService
    {
        private readonly AppDbContext _context;

        public QueryService(AppDbContext context)
        {
            _context = context;
        }

        // Consulta 1: alumnos por profesor, con la escuela
        public async Task<List<StudentWithSchoolDto>> GetStudentsByTeacher(Guid teacherId)
        {
            return await _context.StudentTeachers
                .Where(st => st.TeacherId == teacherId)
                .Include(st => st.Student)
                    .ThenInclude(s => s.MusicSchool)
                .Select(st => new StudentWithSchoolDto
                {
                    StudentId = st.Student.Id,
                    StudentName = st.Student.FirstName + " " + st.Student.LastName,
                    SchoolName = st.Student.MusicSchool != null ? st.Student.MusicSchool.Name : "No asignada"
                })
                .ToListAsync();
        }

        // Consulta 2: escuelas de un profesor y alumnos que enseña en cada una
        public async Task<List<SchoolWithStudentsDto>> GetSchoolsAndStudentsByTeacher(Guid teacherId)
        {
            var studentSchools = await _context.StudentTeachers
                .Where(st => st.TeacherId == teacherId)
                .Include(st => st.Student)
                    .ThenInclude(s => s.MusicSchool)
                .ToListAsync();

            return studentSchools
                .Where(st => st.Student.MusicSchool != null)
                .GroupBy(st => st.Student.MusicSchool!)
                .Select(group => new SchoolWithStudentsDto
                {
                    SchoolId = group.Key.Id,
                    SchoolName = group.Key.Name,
                    StudentNames = group
                        .Select(st => st.Student.FirstName + " " + st.Student.LastName)
                        .ToList()
                })
                .ToList();
        }
    }
}
