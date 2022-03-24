using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Enrolled { get; set; }


        public ICollection<StudentCourse> Enrollment { get; set; } = new HashSet<StudentCourse>();

    }
}
