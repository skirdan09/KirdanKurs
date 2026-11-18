using System.Collections.Generic;
using System.Linq;

namespace AttendanceApp
{
    public static class Database
    {
        public static List<Group> Groups { get; set; } = new List<Group>();
        public static List<Student> Students { get; set; } = new List<Student>();
        public static List<Discipline> Disciplines { get; set; } = new List<Discipline>();
        public static List<User> Users { get; set; } = new List<User>();
        public static User? CurrentUser { get; set; }

        static Database()
        {
            Groups.Add(new Group { Id = 1, Name = "ЗПК1" });
            Groups.Add(new Group { Id = 2, Name = "ЗПК2" });
            Groups.Add(new Group { Id = 3, Name = "1СА1" });
            Groups.Add(new Group { Id = 4, Name = "3ИС1" });
            Groups.Add(new Group { Id = 5, Name = "3СА1" });
            Groups.Add(new Group { Id = 6, Name = "3СА3" });

            Disciplines.Add(new Discipline { Id = 1, Code = "ОП.01", Name = "Экономика организации", Category = "Общепрофессиональные", Teacher = "Иванова А.П.", Hours = 120 });
            Disciplines.Add(new Discipline { Id = 2, Code = "ОП.02", Name = "Статистика", Category = "Общепрофессиональные", Teacher = "Петров С.И.", Hours = 90 });
            Disciplines.Add(new Discipline { Id = 3, Code = "ОП.03", Name = "Менеджмент", Category = "Общепрофессиональные", Teacher = "Сидорова Е.В.", Hours = 108 });
            Disciplines.Add(new Discipline { Id = 4, Code = "МДК.01.01", Name = "Разработка программных модулей", Category = "МДК", Teacher = "Козлов Д.А.", Hours = 250 });
            Disciplines.Add(new Discipline { Id = 5, Code = "МДК.01.02", Name = "Системное программирование", Category = "МДК", Teacher = "Смирнов В.В.", Hours = 180 });
            Disciplines.Add(new Discipline { Id = 6, Code = "МДК.03.01", Name = "Разработка мобильных приложений", Category = "МДК", Teacher = "Федоров А.А.", Hours = 160 });
            Disciplines.Add(new Discipline { Id = 7, Code = "ЕН.01", Name = "Математика", Category = "Общеобразовательные", Teacher = "Николаева О.М.", Hours = 150 });
            Disciplines.Add(new Discipline { Id = 8, Code = "ОГСЭ.01", Name = "Основы философии", Category = "Общеобразовательные", Teacher = "Михайлова Т.Н.", Hours = 48 });
            Disciplines.Add(new Discipline { Id = 9, Code = "ОП.04", Name = "Базы данных", Category = "Общепрофессиональные", Teacher = "Кузнецов А.С.", Hours = 130 });
            Disciplines.Add(new Discipline { Id = 10, Code = "ОП.05", Name = "ООП", Category = "Общепрофессиональные", Teacher = "Морозов Д.Д.", Hours = 110 });
            Disciplines.Add(new Discipline { Id = 11, Code = "ЕН.02", Name = "Информатика", Category = "Общеобразовательные", Teacher = "Федоров А.А.", Hours = 140 });
            Disciplines.Add(new Discipline { Id = 12, Code = "МДК.02.01", Name = "Web-технологии", Category = "МДК", Teacher = "Соколова Е.Ю.", Hours = 200 });
            Disciplines.Add(new Discipline { Id = 13, Code = "ОГСЭ.02", Name = "История", Category = "Общеобразовательные", Teacher = "Васильев П.П.", Hours = 56 });
            Disciplines.Add(new Discipline { Id = 14, Code = "МДК.04.01", Name = "Тестирование ПО", Category = "МДК", Teacher = "Дмитриев О.К.", Hours = 120 });

            Students.Add(new Student { Id = 1, FullName = "Иванов И.И.", GroupId = 1 });
            Students.Add(new Student { Id = 2, FullName = "Петров П.П.", GroupId = 2 });
            Students.Add(new Student { Id = 3, FullName = "Сидорова А.В.", GroupId = 1 });
        }

        public static List<Student> GetStudents() => Students;

        public static void AddStudent(Student student)
        {
            student.Id = Students.Count > 0 ? Students.Max(s => s.Id) + 1 : 1;
            Students.Add(student);
        }

        public static void DeleteStudent(Student student)
        {
            Students.Remove(student);
        }
    }

    public class User
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public string GroupName => Groups.Find(g => g.Id == GroupId)?.Name ?? "Неизвестно";
        public static List<Group> Groups => Database.Groups;
    }

    public class Discipline
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public int Hours { get; set; }
    }
}