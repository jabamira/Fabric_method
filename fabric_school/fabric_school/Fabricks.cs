using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace fabric_school
{
    abstract class Creator_school_obj
    {
        School_obj shool_obj;


        public virtual School_obj FactoryMethod(int id,string name,int expirience)
        {
            return null;
        }
        public virtual School_obj FactoryMethod(int id, string name, List<string> Courses)
        {
            return null;
        }
        public virtual School_obj FactoryMethod(int id, string name, List<string> List, int expirience)
        {
            return null;
        }

    }
    class Teacher_creator : Creator_school_obj
    {
        public override School_obj FactoryMethod(int id, string name, List<string> courses, int experience)
        {

            return new Teacher
            {
                Id = id,
                Name = name,
                Experience = experience,
                Courses = courses
            };
        }
    }
    class Student_creator : Creator_school_obj
    {
        public override School_obj FactoryMethod(int id, string name, List<string> courses)
        {
            return new Student()
            {
                Id = id,
                Name = name,
                Courses = courses
            };
        }
    }
    class Curse_creator : Creator_school_obj
    {
        public override School_obj FactoryMethod(int id, string name, List<string> students, int teacher)
        {
            return new Course()
            {
                Id = id,
                Name = name,
                Students = students,
                TeacherId = teacher
            };
        }
    }
}
