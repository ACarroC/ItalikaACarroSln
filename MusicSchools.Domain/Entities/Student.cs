using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string StudentNumber { get; set; } = null!; // único

        public Guid MusicSchoolId { get; set; }
        public MusicSchool? MusicSchool { get; set; }

        public Guid? TeacherId { get; set; }  // opcional
        public Teacher? Teacher { get; set; }

        public List<StudentTeacher> StudentTeachers { get; set; } = new();
    }
}
