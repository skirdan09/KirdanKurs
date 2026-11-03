using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace AttendanceApp
{
    public static class Database
    {
        private static string connectionString = "Data Source=app.db";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Students (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT NOT NULL,
                    GroupName TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Notifications (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentName TEXT NOT NULL,
                    GroupName TEXT NOT NULL,
                    Discipline TEXT NOT NULL,
                    MissedCount INTEGER NOT NULL,
                    DebtInfo TEXT NOT NULL
                );";
            command.ExecuteNonQuery();

            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM Students";
            long count = Convert.ToInt64(checkCmd.ExecuteScalar());

            if (count == 0)
            {
                var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = @"
                    INSERT INTO Students (FullName, GroupName) VALUES 
                    ('Иванов И.И.', '3ПК-1'), ('Петров П.П.', '3ПК-1'), ('Сидорова А.В.', '3ПК-2');
                    
                    INSERT INTO Notifications (StudentName, GroupName, Discipline, MissedCount, DebtInfo) VALUES 
                    ('Иванов И.И.', '3ПК-1', 'Базы данных', 12, 'Нет зачёта'),
                    ('Петров П.П.', '3ПК-1', 'ООП', 8, 'Не сдана практика №3'),
                    ('Сидорова А.В.', '3ПК-2', 'Веб-технологии', 15, 'Экзамен не сдан');";
                insertCmd.ExecuteNonQuery();
            }
        }

        public static int GetStudentsCount()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Students";
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static List<Notification> GetNotifications()
        {
            var list = new List<Notification>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT StudentName, GroupName, Discipline, MissedCount, DebtInfo FROM Notifications";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Notification
                {
                    StudentName = reader.GetString(0),
                    GroupName = reader.GetString(1),
                    Discipline = reader.GetString(2),
                    MissedCount = reader.GetInt32(3),
                    DebtInfo = reader.GetString(4)
                });
            }
            return list;
        }

        public static List<Student> GetStudents()
        {
            var list = new List<Student>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, FullName, GroupName FROM Students";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Student
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    GroupName = reader.GetString(2)
                });
            }
            return list;
        }

        public static void AddStudent(string fullName, string groupName)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Students (FullName, GroupName) VALUES (@name, @group)";
            cmd.Parameters.AddWithValue("@name", fullName);
            cmd.Parameters.AddWithValue("@group", groupName);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteStudent(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Students WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? GroupName { get; set; }
    }
}