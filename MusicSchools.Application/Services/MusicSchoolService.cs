using MusicSchools.Domain.Entities;
using MusicSchools.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Application.Services
{
    public class MusicSchoolService
    {
        private readonly IMusicSchoolRepository _repo;

        public MusicSchoolService(IMusicSchoolRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<MusicSchool>> GetAllAsync() => _repo.GetAllAsync();
        public Task<MusicSchool?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);
        public Task AddAsync(MusicSchool school) => _repo.AddSchoolAsync(school);
        public Task UpdateAsync(MusicSchool school) => _repo.UpdateSchoolAsync(school);
        public Task DeleteAsync(Guid id) => _repo.DeleteSchoolAsync(id);
    }
}
