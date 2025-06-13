using MusicSchools.Domain.Entities;
using MusicSchools.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Application.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public Task<IEnumerable<Student>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Student?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);
        public Task AddAsync(Student student) => _repo.AddEstudentAsync(student);
        public Task UpdateAsync(Student student) => _repo.UpdateEstudentAsync(student);
        public Task DeleteAsync(Guid id) => _repo.DeleteEstudentAsync(id);
    }
}
