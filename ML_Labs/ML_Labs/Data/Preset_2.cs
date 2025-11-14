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
                data.Add((new double[] { x, y }, "A"));
            }

            // "B" — внутреннее кольцо
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = 1.5 + rnd.NextDouble() * 0.5;
                double x = 3.8 + r * Math.Cos(angle);
                double y = 9.0 + r * Math.Sin(angle) * 0.8;
                data.Add((new double[] { x, y }, "B"));
            }

            // "C" — среднее кольцо
            for (int i = 0; i < 20; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = 2.8 + rnd.NextDouble() * 0.7;
                double x = 3.5 + r * Math.Cos(angle) * 0.9;
                double y = 13.0 + r * Math.Sin(angle) * 1.1;
                data.Add((new double[] { x, y }, "C"));
            }

            // "F" — внешнее кольцо
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = 5.0 + rnd.NextDouble() * 1.5;
                double x = 3.0 + r * Math.Cos(angle);
                double y = 20.0 + r * Math.Sin(angle) * 0.7;
                data.Add((new double[] { x, y }, "F"));
            }

            return data;
        }

        private static List<(double[] Features, string Label)> GenerateTest()
        {
            return new()
            {
                (new double[] {4.7, 2.0}, "A"),
                (new double[] {3.8, 9.0}, "B"),
                (new double[] {3.5, 13.0}, "C"),
                (new double[] {3.0, 20.0}, "F"),
                (new double[] {4.0, 15}, "F"),
                (new double[] {5.0, 10}, "C"),     // ← было ?, на самом деле C
                (new double[] {2.0, 25}, "F"),
                (new double[] {4.5, 5}, "B"),      // ← было ?, на самом деле B
                (new double[] {3.2, 18}, "F"),
                (new double[] {1.5, 22}, "F"),
                (new double[] {4.8, 1.5}, "A"),
                (new double[] {3.7, 8.5}, "B"),
                (new double[] {3.6, 14}, "C"),
                (new double[] {2.8, 21}, "F"),
                (new double[] {4.2, 11}, "C"),     // ← было ?, на самом деле C
                (new double[] {3.0, 16}, "F"),
                (new double[] {5.2, 8}, "B"),
                (new double[] {2.5, 10}, "F"),
                (new double[] {4.9, 2.5}, "A"),
                (new double[] {1.8, 28}, "F")
            };
        }
    }
}