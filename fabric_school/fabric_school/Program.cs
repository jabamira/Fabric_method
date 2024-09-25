using fabric_school;
using System.Collections.Generic;
using static Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{


    static List<School_obj> Data = new List<School_obj>(); 
    static void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath)) return;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split('|');
                var chasti = parts[1].Split('!');
                int id = Convert.ToInt32(chasti[0]);
                string name = chasti[1];
                if (parts[0] == "student") 
                {
                    List<string> courses = new List<string>();

                    for (int i = 2; i < chasti.Length; i++) 
                    {
                        courses.Add(chasti[i]);
                    }

                    School_obj student = null;
                    Creator_school_obj creator_student = null;

                    creator_student = new Student_creator();
                    student = creator_student.FactoryMethod(id, name, courses);

                    Data.Add(student);
                }
                if (parts[0] == "course") 
                {
                  
                    List<string> students = new List<string>();
                    int Teacherid = Convert.ToInt32(chasti[2]);
                    for (int i = 3; i < chasti.Length ; i++) 
                    {
                        students.Add(chasti[i]);
                    }
                    
                    School_obj course = null;
                    Creator_school_obj creator_course = null;

                    creator_course = new Curse_creator();
                    course = creator_course.FactoryMethod(id, name, students, Teacherid);

                    Data.Add(course);
                }
                if (parts[0] == "teacher")
                {
                    List<string> students = new List<string>();
                    int Expirience = Convert.ToInt32(chasti[2]);
                    for (int i = 3; i < chasti.Length; i++)
                    {
                        students.Add(chasti[i]);
                    }

                    School_obj teacher = null;
                    Creator_school_obj creator_teacher = null;

                    creator_teacher = new Teacher_creator();
                    teacher = creator_teacher.FactoryMethod(id, name, students, Expirience);

                    Data.Add(teacher);
                }

            }
        }
    }
    public static void Show_info()
    {
        foreach (School_obj obj in Data)
        {
           

            if (obj is Student student)
            {
                Console.WriteLine("STUDENT");
                Console.WriteLine("ID: " + obj.Id);
                Console.WriteLine("Name: " + obj.Name);
                Console.Write("Course:");
                foreach (string _course in student.Courses)
                { 
                    Console.Write(_course+" ");
                }
            }

            if (obj is Teacher teacher)
            {
                Console.WriteLine("TEACHER");
                Console.WriteLine("ID: "+obj.Id);
                Console.WriteLine("Name: "+obj.Name);
                Console.WriteLine(teacher.Experience);
                Console.Write("Courses:");
                foreach (string courses in teacher.Courses)
                {
                    Console.Write(courses + " ");
                }
            }

            if (obj is Course course)
            {
                Console.WriteLine("COURSE");
                Console.WriteLine("ID: "+ obj.Id);
                Console.WriteLine("Name: " + obj.Name);
                Console.WriteLine(course.TeacherId);
                Console.Write("Students:");
                foreach (string students in course.Students)
                {
                    Console.Write(students+ " ");
                }
            }
            Console.WriteLine("\n");
            Console.WriteLine("=================================");
        }
        
    }
    static void SaveToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var obj in Data)
            {
                switch (obj)
                {
                    case Student student:
                        writer.WriteLine($"student|{student.Id}!{student.Name}!" + string.Join("!", student.Courses));
                        break;
                    case Teacher teacher:
                        writer.WriteLine($"teacher|{teacher.Id}!{teacher.Name}!{teacher.Experience}!" + string.Join("!", teacher.Courses));
                        break;
                    case Course course:
                        writer.WriteLine($"course|{course.Id}!{course.Name}!{course.TeacherId}!" + string.Join("!", course.Students));
                        break;
                }
            }
        }
    }
    static void AddStudent()
    {
        Console.Write("Enter ID Student: ");
        int id = int.Parse(Console.ReadLine());
        var existingStudent = Data.OfType<Student>().FirstOrDefault(c => c.Id == id);
        if (existingStudent != null)
        {
            Console.WriteLine("Student already exsist");
            return;
        }


        Console.Write("Endter Name Student: ");
        string name = Console.ReadLine();
      

        Console.Write("Enter ID Courses by space: ");
        var coursesInput = Console.ReadLine();

        List<string> coursesIds = new List<string>();



        if (!string.IsNullOrWhiteSpace(coursesInput))
        {
            coursesIds = coursesInput.Split(' ').Select(course => course.Trim()).ToList();
        }
        foreach (var course in coursesIds)
        {
            course.Trim();
            var existingCourse = Data.OfType<Course>().FirstOrDefault(c => c.Id == Convert.ToInt32(course));
            if (existingCourse == null)
            {
                Console.WriteLine($"Course with ID {course} not found. Teacher could not be added.");
                return;
            }
        }
            School_obj student = null;
        Creator_school_obj creator_student = null;

        creator_student = new Student_creator();
        student = creator_student.FactoryMethod(id, name, coursesIds);



  
        Data.Add(student);
    }
    static void AddTeacher()
    {
        Console.Write("Enter ID Teacher: ");
        int id = int.Parse(Console.ReadLine());
        var existingTeacher = Data.OfType<Teacher>().FirstOrDefault(c => c.Id == id);
        if (existingTeacher != null) 
        {
            Console.WriteLine("Teacher already exsist");
            return;
        }


            Console.Write("Enter name Teacher: ");
        string name = Console.ReadLine();


        Console.Write("Enter ID Courses  by space ");
        var coursesInput = Console.ReadLine();
        List<string> Courses = new List<string>();

        if (!string.IsNullOrWhiteSpace(coursesInput))
        {
            Courses = coursesInput.Split(' ').Select(course => course.Trim()).ToList();
        }
        foreach (var course in Courses)
        {
            course.Trim();
            var existingCourse = Data.OfType<Course>().FirstOrDefault(c => c.Id ==  Convert.ToInt32(course));
            if (existingCourse == null)
            {
                Console.WriteLine($"Course with ID {course} not found. Teacher could not be added.");
                return;
            }
        }
       
        Console.Write("Enter Expirience Teacher ");
        int exp = Convert.ToInt32(Console.ReadLine());
        School_obj teacher = null;
        Creator_school_obj creator_teacher = null;

        creator_teacher = new Teacher_creator();
        teacher = creator_teacher.FactoryMethod(id, name, Courses, exp);
        Data.Add(teacher);

    }
    static void AddCourse()
    {
        Console.Write("Enter ID Course: ");
        int id = int.Parse(Console.ReadLine());
        var existingCourse = Data.OfType<Course>().FirstOrDefault(c => c.Id == id);
        if (existingCourse != null)
        {
            Console.WriteLine("Course already exsist");
            return;
        }


        Console.Write("Enter name Course: ");
        string name = Console.ReadLine();


        Console.Write("Enter ID students by space ");
        var coursesInput = Console.ReadLine();
        List<string> Students = new List<string>();

        if (!string.IsNullOrWhiteSpace(coursesInput))
        {
            Students = coursesInput.Split(' ').Select(course => course.Trim()).ToList();
        }
        foreach (var student in Students)
        {
            student.Trim();
            var existingstudent = Data.OfType<Student>().FirstOrDefault(c => c.Id == Convert.ToInt32(student));
            if (existingstudent == null)
            {
                Console.WriteLine($"student with ID {student} not found. Course could not be added.");
                return;
            }
        }
        Console.Write("Enter Teacherid");
        int Teacherid = Convert.ToInt32(Console.ReadLine());
        var existingTeacherid = Data.OfType<Teacher>().FirstOrDefault(c => c.Id == Teacherid);
        if (existingTeacherid == null)
        {
            Console.WriteLine("Teacher is not found");
            return;
        }
        School_obj course = null;
        Creator_school_obj creator_course = null;

        creator_course = new Curse_creator();
        course = creator_course.FactoryMethod(id, name, Students, Teacherid);
        Data.Add(course);
    }
    static void ShowRelatedObjects()
    {
        Console.Write("Enter the object type (student, teacher, course): ");
        string objectType = Console.ReadLine()?.Trim().ToLower();

        Console.Write("Enter the object ID to show related objects: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        switch (objectType)
        {
            case "student":
                var student = Data.OfType<Student>().FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    Console.WriteLine($"Student: ID = {student.Id}, Name = {student.Name}");
                    Console.WriteLine($"Courses: {string.Join(", ", student.Courses)}");
                    return;
                }
                break;

            case "teacher":
                var teacher = Data.OfType<Teacher>().FirstOrDefault(t => t.Id == id);
                if (teacher != null)
                {
                    Console.WriteLine($"Teacher: ID = {teacher.Id}, Name = {teacher.Name}");
                    Console.WriteLine($"Courses: {string.Join(", ", teacher.Courses)}");
                    return;
                }
                break;

            case "course":
                var course = Data.OfType<Course>().FirstOrDefault(c => c.Id == id);
                if (course != null)
                {
                    Console.WriteLine($"Course: ID = {course.Id}, Name = {course.Name}");
                    Console.WriteLine($"Teacher ID: {course.Id}");
                    Console.WriteLine($"Students: {string.Join(", ", course.Id)}");
                    return;
                }
                break;

            default:
                Console.WriteLine("Invalid object type. Please enter 'student', 'teacher' or 'course'.");
                return;
        }
    }
    static void Main()
    {
        string filepath = "test.txt";
        LoadFromFile(filepath);
        Show_info();
        
        while (true)
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Add a student");
            Console.WriteLine("2. Add a teacher");
            Console.WriteLine("3. Add a course");
            Console.WriteLine("4. Save data");
            Console.WriteLine("5. Show all data");
            Console.WriteLine("6. Show related objects");
            Console.WriteLine("7. Exit");


            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    AddTeacher();
                    break;
                case "3":
                    AddCourse();
                    break;
                case "4":
                    SaveToFile(filepath);
                    Console.WriteLine("Save is complited");
                    break;
                case "5":
                    Show_info();
                    break;
                case "6":
                    ShowRelatedObjects();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Wrong choice. Plese try again.");
                    break;
            }
        }
    
    
       
    }
}