using MusicSchools.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Application.Services
{
    public class TeacherService
    {
        public readonly ITeacherRepository _repo;
        public TeacherService(ITeacherRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public Task<IEnumerable<Domain.Entities.Teacher>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Domain.Entities.Teacher?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);
            
        public Task AddAsync(Domain.Entities.Teacher teacher) => _repo.AddTeacherAsync(teacher);
        public Task UpdateAsync(Domain.Entities.Teacher teacher) => _repo.UpdateTeacherAsync(teacher);
        
        public Task DeleteAsync(Guid id) => _repo.DeleteTeacherAsync(id);

    }
}
