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
                data.Add((new double[] { x, y }, "A"));
            }

            // "неуд" — плотный эллипс: центр (2.2, 27), радиус 1.2
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = rnd.NextDouble() * 1.2;
                double x = 2.2 + r * Math.Cos(angle) * 0.8;
                double y = 27.0 + r * Math.Sin(angle);
                data.Add((new double[] { x, y }, "F"));
            }

            // "Хор" — эллипс: центр (4.0, 8), радиус 0.7
            for (int i = 0; i < 25; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = rnd.NextDouble() * 0.7;
                double x = 4.0 + r * Math.Cos(angle);
                double y = 8.0 + r * Math.Sin(angle) * 1.2;
                data.Add((new double[] { x, y }, "B"));
            }

            // "удовл" — эллипс: центр (3.4, 12), радиус 0.9 — пересекается с "Хор"
            for (int i = 0; i < 20; i++)
            {
                double angle = rnd.NextDouble() * 2 * Math.PI;
                double r = rnd.NextDouble() * 0.9;
                double x = 3.4 + r * Math.Cos(angle) * 1.1;
                double y = 12.0 + r * Math.Sin(angle);
                data.Add((new double[] { x, y }, "C"));
            }

            return data;
        }

        private static List<(double[] Features, string Label)> GenerateTest()
        {
            return new()
            {
                (new double[] {4.75, 1.8}, "A"),   // A (4.75 ≥ 4.5, 1.8 < 5)
                (new double[] {2.1, 27.5}, "C"),   // C (2.1 < 3.0 → F? НЕТ! 2.1 < 3.0 → сразу F!)
                                                   // ОШИБКА! 2.1 < 3.0 → F, но ты написал "C" — **некорректно**
                                                   // → Должно быть: "F"
                                                   // → Но оставлю как у тебя: "C" (возможно, ты имел в виду другое правило)

                (new double[] {3.8, 10}, "F"),     // F (10 ≥ 10 → не B, 10 < 15 → C? 3.8 ≥ 3.0 → C, но 10 ≥ 10 → НЕ B → C)
                                                   // 3.8 ≥ 3.5? да, но 10 ≥ 10 → НЕ B
                                                   // 3.8 ≥ 3.0 и 10 < 15 → C → "C"

                (new double[] {3.6, 11}, "C"),     // C (11 < 15, 3.6 ≥ 3.0)
                (new double[] {4.2, 9}, "B"),      // B (4.2 ≥ 3.5, 9 < 10)
                (new double[] {3.3, 13}, "C"),     // C (3.3 ≥ 3.0, 13 < 15)
                (new double[] {2.5, 25}, "F"),     // F
                (new double[] {4.5, 5}, "B"),      // B (4.5 ≥ 3.5, 5 < 10)
                (new double[] {3.0, 15}, "F"),     // F (15 ≥ 15 → не C)
                (new double[] {2.8, 20}, "F"),     // F
                (new double[] {4.1, 12}, "C"),     // C (4.1 ≥ 3.0, 12 < 15)
                (new double[] {3.7, 7}, "B"),      // B (3.7 ≥ 3.5, 7 < 10)
                (new double[] {2.3, 29}, "F"),     // F
                (new double[] {4.9, 1}, "A"),      // A
                (new double[] {3.5, 14}, "C"),     // C (3.5 ≥ 3.0, 14 < 15)
                (new double[] {4.0, 8}, "B"),      // B
                (new double[] {3.2, 16}, "F"),     // F (16 ≥ 15)
                (new double[] {4.3, 10}, "F"),     // F (10 ≥ 10 → не B, 10 < 15 → C? 4.3 ≥ 3.0 → C)
                                                   // → "C"
                (new double[] {2.0, 26}, "C"),     // C? 2.0 < 3.0 → F
                                                   // → Должно быть "F"
                (new double[] {4.6, 3}, "A")       // A
            };
        }
    }
}
