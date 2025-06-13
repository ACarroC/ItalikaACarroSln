using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchools.Application.DTOs
{
    public class SchoolWithStudentsDto
    {
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; } = string.Empty;
        public List<string> StudentNames { get; set; } = new();
    }
}
