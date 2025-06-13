using MusicSchools.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task AddEstudentAsync(Student student);
        Task UpdateEstudentAsync(Student student);
        Task DeleteEstudentAsync(Guid id);
    }
}
