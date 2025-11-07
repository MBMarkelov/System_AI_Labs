namespace ML_Labs
{
    public class Preset_1
    {
        public static List<(double[] Features, string Label)> TrainingData => GenerateTraining();
        public static List<(double[] Features, string Label)> TestData => GenerateTest();

        private static List<(double[] Features, string Label)> GenerateTraining()
        {
            var data = new List<(double[], string)>();
            var rnd = new Random(42);

            // "Отл" — плотный эллипс: центр (4.7, 2), радиус 0.4
            for (int i = 0; i < 30; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = rnd.NextDouble() * 0.4;
                double x = 4.7 + r * Math.Cos(angle);
                double y = 2.0 + r * Math.Sin(angle) * 0.6;
                data.Add((new double[] { x, y }, "Отл"));
            }

            // "неуд" — плотный эллипс: центр (2.2, 27), радиус 1.2
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = rnd.NextDouble() * 1.2;
                double x = 2.2 + r * Math.Cos(angle) * 0.8;
                double y = 27.0 + r * Math.Sin(angle);
                data.Add((new double[] { x, y }, "неуд"));
            }

            // "Хор" — эллипс: центр (4.0, 8), радиус 0.7
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = rnd.NextDouble() * 0.7;
                double x = 4.0 + r * Math.Cos(angle);
                double y = 8.0 + r * Math.Sin(angle) * 1.2;
                data.Add((new double[] { x, y }, "Хор"));
            }

            // "удовл" — эллипс: центр (3.4, 12), радиус 0.9 — пересекается с "Хор"
            for (int i = 0; i < 20; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = rnd.NextDouble() * 0.9;
                double x = 3.4 + r * Math.Cos(angle) * 1.1;
                double y = 12.0 + r * Math.Sin(angle);
                data.Add((new double[] { x, y }, "удовл"));
            }

            return data;
        }

        private static List<(double[] Features, string Label)> GenerateTest()
        {
            return new()
            {
                (new double[] {4.75, 1.8}, "Отл"),      // в центре Отл
                (new double[] {2.1, 27.5}, "неуд"),     // в центре неуд
                (new double[] {3.8, 10}, "?"),          // на границе Хор/удовл
                (new double[] {3.6, 11}, "?"),          // в пересечении
                (new double[] {4.2, 9}, "?"),           // ближе к Хор
                (new double[] {3.3, 13}, "?"),          // ближе к удовл
                (new double[] {2.5, 25}, "?"),          // между неуд и удовл
                (new double[] {4.5, 5}, "?"),           // между Отл и Хор
                (new double[] {3.0, 15}, "?"),          // сложный случай
                (new double[] {2.8, 20}, "?"),          // на границе неуд/удовл
                (new double[] {4.1, 12}, "?"),          // в пересечении Хор/удовл
                (new double[] {3.7, 7}, "?"),           // ближе к Хор
                (new double[] {2.3, 29}, "неуд"),       // в неуд
                (new double[] {4.9, 1}, "Отл"),         // в Отл
                (new double[] {3.5, 14}, "?"),          // в удовл
                (new double[] {4.0, 8}, "Хор"),         // в Хор
                (new double[] {3.2, 16}, "?"),          // на границе удовл/неуд
                (new double[] {4.3, 10}, "?"),          // в пересечении
                (new double[] {2.0, 26}, "неуд"),       // в неуд
                (new double[] {4.6, 3}, "Отл")          // в Отл
            };
        }
    }
}
