using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ML_Labs
{
    public static class StudentDataGenerator
    {
        private static readonly Random rnd = new();

        public static List<(double[] Features, string Label)> Generate(int count)
        {
            var data = new List<(double[], string)>();

            for (int i = 0; i < count; i++)
            {
                double grade = Math.Round(rnd.NextDouble() * 3 + 2, 2);
                double absences = rnd.Next(0, 30);

                string label = ClassifyStudent(grade, absences);
                data.Add((new double[] { grade, absences }, label));
            }

            return data;
        }

        private static string ClassifyStudent(double grade, double absences)
        {
            if (grade >= 4.5 && absences < 5) return "Отл";
            if (grade >= 3.5 && absences < 10) return "Хор";
            if (grade >= 3.0 && absences < 15) return "удовл";
            return "неуд";
        }



        // 2. Метод для сохранения данных в JSON файл
        public static void SaveToJson(List<(double[] Features, string Label)> data, string filename = "student_data.json")
        {
            var students = data.Select((item, index) => new
            {
                Id = index + 1,
                Grade = item.Features[0],
                Absences = item.Features[1],
                Category = item.Label
            }).ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(students, options);
            File.WriteAllText(filename, json);

            Console.WriteLine($"\nДанные сохранены в файл: {filename}");
            Console.WriteLine($"Сохранено записей: {students.Count}");
        }

        // Метод для загрузки данных из JSON файла
        public static List<(double[] Features, string Label)> LoadFromJson(string filename = "student_data.json")
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Файл {filename} не найден");
            }

            string json = File.ReadAllText(filename);
            var students = JsonSerializer.Deserialize<List<StudentRecord>>(json);

            return students.Select(s => (new double[] { s.Grade, s.Absences }, s.Category)).ToList();
        }

        private class StudentRecord
        {
            public int Id { get; set; }
            public double Grade { get; set; }
            public double Absences { get; set; }
            public string Category { get; set; } = string.Empty;
        }
    }

}
