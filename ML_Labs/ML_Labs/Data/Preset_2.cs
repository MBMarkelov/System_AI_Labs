namespace ML_Labs
{
    public class Preset_2
    {
        public static List<(double[] Features, string Label)> TrainingData => GenerateTraining();
        public static List<(double[] Features, string Label)> TestData => GenerateTest();

        private static List<(double[] Features, string Label)> GenerateTraining()
        {
            var data = new List<(double[], string)>();
            var rnd = new Random(123);

            // "Отл" — центр
            for (int i = 0; i < 30; i++)
            {
                double x = 4.7 + (rnd.NextDouble() - 0.5) * 0.6;
                double y = 2.0 + (rnd.NextDouble() - 0.5) * 0.6;
                data.Add((new double[] { x, y }, "Отл"));
            }

            // "Хор" — внутреннее кольцо
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = 1.5 + rnd.NextDouble() * 0.5;
                double x = 3.8 + r * Math.Cos(angle);
                double y = 9.0 + r * Math.Sin(angle) * 0.8;
                data.Add((new double[] { x, y }, "Хор"));
            }

            // "удовл" — среднее кольцо
            for (int i = 0; i < 20; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = 2.8 + rnd.NextDouble() * 0.7;
                double x = 3.5 + r * Math.Cos(angle) * 0.9;
                double y = 13.0 + r * Math.Sin(angle) * 1.1;
                data.Add((new double[] { x, y }, "удовл"));
            }

            // "неуд" — внешнее кольцо
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = 5.0 + rnd.NextDouble() * 1.5;
                double x = 3.0 + r * Math.Cos(angle);
                double y = 20.0 + r * Math.Sin(angle) * 0.7;
                data.Add((new double[] { x, y }, "неуд"));
            }

            return data;
        }

        private static List<(double[] Features, string Label)> GenerateTest()
        {
            return new()
            {
                (new double[] {4.7, 2.0}, "Отл"),       // центр
                (new double[] {3.8, 9.0}, "Хор"),       // внутреннее кольцо
                (new double[] {3.5, 13.0}, "удовл"),    // среднее
                (new double[] {3.0, 20.0}, "неуд"),     // внешнее
                (new double[] {4.0, 15}, "?"),          // между удовл и неуд
                (new double[] {5.0, 10}, "?"),          // выброс?
                (new double[] {2.0, 25}, "неуд"),       // в кольце
                (new double[] {4.5, 5}, "?"),           // между Отл и Хор
                (new double[] {3.2, 18}, "?"),          // между удовл и неуд
                (new double[] {1.5, 22}, "?"),          // выброс
                (new double[] {4.8, 1.5}, "Отл"),
                (new double[] {3.7, 8.5}, "Хор"),
                (new double[] {3.6, 14}, "удовл"),
                (new double[] {2.8, 21}, "неуд"),
                (new double[] {4.2, 11}, "?"),
                (new double[] {3.0, 16}, "?"),
                (new double[] {5.2, 8}, "?"),
                (new double[] {2.5, 10}, "?"),
                (new double[] {4.9, 2.5}, "Отл"),
                (new double[] {1.8, 28}, "неуд")
            };
        }
    }
}