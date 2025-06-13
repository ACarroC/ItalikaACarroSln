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
    public class MusicSchoolRepository : IMusicSchoolRepository
    {
        private readonly AppDbContext _context;

        public MusicSchoolRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<MusicSchool>> GetAllAsync()
        {
            return await _context.MusicSchools.ToListAsync();
        }

        public async Task AddSchoolAsync(MusicSchool school)
        {
            var sql = "EXEC sp_ManageSchool @Operation = {0}, @Name = {1}, @Description = {2}, @Code = {3}";
            await _context.Database.ExecuteSqlRawAsync(sql, "CREATE", school.Name, school.Description, school.Code);
        }

        public async Task<MusicSchool?> GetByIdAsync(Guid id)
        {
            var sql = "EXEC sp_ManageSchool @Operation = {0}, @Id = {1}";

            var result = await _context.MusicSchools
                .FromSqlRaw(sql, "READ", id)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task UpdateSchoolAsync(MusicSchool school)
        {
            var sql = "EXEC sp_ManageSchool @Operation = {0}, @Id = {1}, @Name = {2}, @Description = {3}, @Code = {4}";
            await _context.Database.ExecuteSqlRawAsync(sql, "UPDATE", school.Id, school.Name, school.Description, school.Code);
        }

        public async Task DeleteSchoolAsync(Guid id)
        {
            var sql = "EXEC sp_ManageSchool @Operation = {0}, @Id = {1}";
            await _context.Database.ExecuteSqlRawAsync(sql, "DELETE", id);
        }
    }
}
