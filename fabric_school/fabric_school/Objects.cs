using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fabric_school
{
    public abstract class School_obj
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Teacher : School_obj
    {
        public int Experience { get; set; }
        public List<string> Courses { get; set; } = new List<string>();
       
    }
    public class Student : School_obj
    {
        public List<string> Courses { get; set; } = new List<string>();

    }


    public class Course : School_obj
    {
        public int TeacherId { get; set; }
        public List<string> Students { get; set; } = new List<string>();
    }
}
