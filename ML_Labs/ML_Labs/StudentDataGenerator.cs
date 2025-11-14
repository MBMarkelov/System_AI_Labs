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
            if (grade >= 4.5 && absences < 5) return "A";
            if (grade >= 3.5 && absences < 10) return "B";
            if (grade >= 3.0 && absences < 15) return "C";
            return "F";
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
