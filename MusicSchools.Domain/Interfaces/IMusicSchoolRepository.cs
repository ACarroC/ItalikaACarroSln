using MusicSchools.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Domain.Interfaces
{
    public interface IMusicSchoolRepository
    {
        Task<IEnumerable<MusicSchool>> GetAllAsync();
        Task<MusicSchool?> GetByIdAsync(Guid id);
        Task AddSchoolAsync(MusicSchool school);
        Task UpdateSchoolAsync(MusicSchool school);
        Task DeleteSchoolAsync(Guid id);
    }
}
