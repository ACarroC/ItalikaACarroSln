using Microsoft.EntityFrameworkCore;
using MusicSchools.Domain.Entities;
using MusicSchools.Domain.Interfaces;
using MusicSchools.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return Task.FromResult(_context.Students.Include(s => s.MusicSchool).AsEnumerable());
        }

        public async Task AddEstudentAsync(Student student)
        {
            var sql = "EXEC sp_ManageStudent @Operation = {0}, @FirstName = {1}, @LastName = {2}, @BirthDate = {3}, @StudentNumber = {4}, @MusicSchoolId = {5}, @TeacherId = {6}";
            await _context.Database.ExecuteSqlRawAsync(sql, "CREATE", student.FirstName, student.LastName, student.BirthDate, student.StudentNumber, student.MusicSchoolId, student.TeacherId);
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            var sql = "EXEC sp_ManageStudent @Operation = {0}, @Id = {1}";

            var result = await _context.Students
                .FromSqlRaw(sql, "READ", id)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task UpdateEstudentAsync(Student student)
        {
            var sql = "EXEC sp_ManageStudent @Operation = {0}, @Id = {1}, @FirstName = {2}, @LastName = {3}, @BirthDate = {4}, @StudentNumber = {5}, @MusicSchoolId = {6}, @TeacherId = {7}";
            await _context.Database.ExecuteSqlRawAsync(sql, "UPDATE", student.Id, student.FirstName, student.LastName, student.BirthDate, student.StudentNumber, student.MusicSchoolId, student.TeacherId);
        }

        public async Task DeleteEstudentAsync(Guid id)
        {
            var sql = "EXEC sp_ManageStudent @Operation = {0}, @Id = {1}";
            await _context.Database.ExecuteSqlRawAsync(sql, "DELETE", id);
        }
    }
}
