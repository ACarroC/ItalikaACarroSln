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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly  AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return Task.FromResult(_context.Teachers.Include(s => s.MusicSchool).AsEnumerable());
        }

        public async Task AddTeacherAsync(Teacher teacher)
        {
            var sql = "EXEC sp_ManageTeacher @Operation = {0}, @FirstName = {1}, @LastName = {2}, @TeacherNumber = {3}, @MusicSchoolId = {4}";
            await _context.Database.ExecuteSqlRawAsync(sql, "CREATE", teacher.FirstName, teacher.LastName, teacher.TeacherNumber, teacher.MusicSchoolId);
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            var sql = "EXEC sp_ManageTeacher @Operation = {0}, @Id = {1}";

            var result = await _context.Teachers
                .FromSqlRaw(sql, "READ", id)
                .AsNoTracking()
                .ToListAsync(); // Traer todo en lista

            return result.FirstOrDefault(); // Luego filtras en memoria
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            var sql = "EXEC sp_ManageTeacher @Operation = {0}, @Id = {1}, @FirstName = {2}, @LastName = {3}, @TeacherNumber = {4}, @MusicSchoolId = {5}";
            await _context.Database.ExecuteSqlRawAsync(sql, "UPDATE", teacher.Id, teacher.FirstName, teacher.LastName, teacher.TeacherNumber, teacher.MusicSchoolId);
        }

        public async Task DeleteTeacherAsync(Guid id)
        {
            var sql = "EXEC sp_ManageTeacher @Operation = {0}, @Id = {1}";
            await _context.Database.ExecuteSqlRawAsync(sql, "DELETE", id);
        }
    }
}
