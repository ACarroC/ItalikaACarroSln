using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string TeacherNumber { get; set; } = null!; // único

        public Guid MusicSchoolId { get; set; }

        public MusicSchool? MusicSchool { get; set; }

        public List<Student> Students { get; set; } = new();

        public List<StudentTeacher> StudentTeachers { get; set; } = new();

    }
}
