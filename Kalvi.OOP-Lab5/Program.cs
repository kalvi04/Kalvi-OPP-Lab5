using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabWork
{
    enum ControlType
    {
        Exam,       
        Credit,     
        CourseWork  
    }

    struct Discipline
    {
        public string Name;            
        public string Teacher;         
        public string GroupName;       
        public int StudentCount;       
        public ControlType FinalControl; 
        public bool HasCourseWork;     
        public string Specialty;       
        public int Semester;           
    }

    class Program
    {
        static string filePath = "disciplines.txt";

        static void Main()
        {
            List<Discipline> disciplines = LoadFromFile();

            if (disciplines.Count == 0)
            {
                Discipline initialDiscipline = new Discipline
                {
                    Name = "Object-oriented programming",
                    Teacher = "Volodymyr Anatoliyovych Gotovych",
                    GroupName = "СТ-21",
                    StudentCount = 10,
                    FinalControl = ControlType.Credit,
                    HasCourseWork = false,
                    Specialty = "information systems and technology",
                    Semester = 3
                };

                disciplines.Add(initialDiscipline);
                AppendToFile(initialDiscipline); 
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Додати нову дисципліну");
                Console.WriteLine("2. Показати всі дисципліни");
                Console.WriteLine("3. Пошук по прізвищу викладача");
                Console.WriteLine("4. Пошук по назві дисципліни");
                Console.WriteLine("5. Пошук за наявністю курсової роботи");
                Console.WriteLine("6. Пошук за номером семестру");
                Console.WriteLine("0. Вихід");

                Console.Write("Оберіть опцію: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDiscipline(disciplines);
                        break;
                    case "2":
                        ShowDisciplines(disciplines);
                        break;
                    case "3":
                        SearchByTeacher(disciplines);
                        break;
                    case "4":
                        SearchByName(disciplines);
                        break;
                    case "5":
                        SearchByCourseWork(disciplines);
                        break;
                    case "6":
                        SearchBySemester(disciplines);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
            }
        }

        static void AddDiscipline(List<Discipline> disciplines)
        {
            Discipline discipline = new Discipline();

            Console.Write("Введіть назву дисципліни: ");
            discipline.Name = Console.ReadLine();

            Console.Write("Введіть П.І.П. викладача: ");
            discipline.Teacher = Console.ReadLine();

            Console.Write("Введіть назву групи: ");
            discipline.GroupName = Console.ReadLine();

            Console.Write("Введіть кількість студентів у групі: ");
            discipline.StudentCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Оберіть вид підсумкового контролю (0 - Exam, 1 - Credit, 2 - CourseWork): ");
            discipline.FinalControl = (ControlType)int.Parse(Console.ReadLine());

            Console.Write("Чи є курсова робота? (true/false): ");
            discipline.HasCourseWork = bool.Parse(Console.ReadLine());

            Console.Write("Введіть назву спеціальності: ");
            discipline.Specialty = Console.ReadLine();

            Console.Write("Введіть номер семестру: ");
            discipline.Semester = int.Parse(Console.ReadLine());

            disciplines.Add(discipline);
            AppendToFile(discipline);
        }

        static void AppendToFile(Discipline discipline)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{discipline.Name}|{discipline.Teacher}|{discipline.GroupName}|{discipline.StudentCount}|{discipline.FinalControl}|{discipline.HasCourseWork}|{discipline.Specialty}|{discipline.Semester}");
            }
        }

        static List<Discipline> LoadFromFile()
        {
            List<Discipline> disciplines = new List<Discipline>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] data = line.Split('|');
                    Discipline discipline = new Discipline
                    {
                        Name = data[0],
                        Teacher = data[1],
                        GroupName = data[2],
                        StudentCount = int.Parse(data[3]),
                        FinalControl = (ControlType)Enum.Parse(typeof(ControlType), data[4]),
                        HasCourseWork = bool.Parse(data[5]),
                        Specialty = data[6],
                        Semester = int.Parse(data[7])
                    };
                    disciplines.Add(discipline);
                }
            }

            return disciplines;
        }

        static void ShowDisciplines(List<Discipline> disciplines)
        {
            Console.WriteLine("\nСписок дисциплін:");
            foreach (var d in disciplines)
            {
                Console.WriteLine($"{d.Name}, {d.Teacher}, {d.GroupName}, {d.StudentCount} студентів, {d.FinalControl}, Курсова: {d.HasCourseWork}, Спеціальність: {d.Specialty}, Семестр: {d.Semester}");
            }
        }

        static void SearchByTeacher(List<Discipline> disciplines)
        {
            Console.Write("Введіть прізвище викладача: ");
            string teacher = Console.ReadLine();
            var results = disciplines.Where(d => d.Teacher.Contains(teacher)).ToList();
            ShowDisciplines(results);
        }

        static void SearchByName(List<Discipline> disciplines)
        {
            Console.Write("Введіть назву дисципліни: ");
            string name = Console.ReadLine();
            var results = disciplines.Where(d => d.Name.Contains(name)).ToList();
            ShowDisciplines(results);
        }

        static void SearchByCourseWork(List<Discipline> disciplines)
        {
            Console.Write("Чи є курсова робота? (true/false): ");
            bool hasCourseWork = bool.Parse(Console.ReadLine());
            var results = disciplines.Where(d => d.HasCourseWork == hasCourseWork).ToList();
            ShowDisciplines(results);
        }

        static void SearchBySemester(List<Discipline> disciplines)
        {
            Console.Write("Введіть номер семестру: ");
            int semester = int.Parse(Console.ReadLine());
            var results = disciplines.Where(d => d.Semester == semester).ToList();
            ShowDisciplines(results);
        }
    }
}
