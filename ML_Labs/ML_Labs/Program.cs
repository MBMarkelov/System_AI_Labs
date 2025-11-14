using ML_Labs.Optimize;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ML_Labs
{
    class Program
    {
        static void Main()
        {
            testOptimize();
        }

        private void testKnn()
        {
            var knn = new KnnClassifier(k: 3);

            var trainingData = StudentDataGenerator.Generate(100);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);

            var testData = StudentDataGenerator.Generate(30);

            double accuracy = knn.Evaluate(testData);
            Console.WriteLine($"Accuracy: {accuracy:P2}");

            double[] student = [4.2, 8];
            string predicted = knn.Classify(student);

            Console.WriteLine($"Student {predicted}");
        }
        private static void testOptimize()
        {
            Func<double, double> f1 = x => Math.Pow(x - 2.0, 2) + 1.0; // минимум при x=2
            Func<double, double> f2 = x => Math.Pow(x - 0.75, 4) + Math.Pow(x - 0.75, 2) + 0.5;
            Func<double, double> f3 = x => Math.Pow(x, 4) - 3 * Math.Pow(x, 3) + 2;

            double a = -5.0, b = 5.0;

            Console.WriteLine("=== Test function f1(x) = (x-2)^2 + 1 ===");
            TestAllMethods(f1, a, b, 0.0);

            Console.WriteLine("\n=== Test function f2(x) = (x-0.75)^4 + (x-0.75)^2 + 0.5 ===");
            TestAllMethods(f2, -2.0, 3.0, 1.0);

            Console.WriteLine("\n=== Test function f3(x) = x^4 - 3x^3 + 2 ===");
            TestAllMethods(f3, 0.0, 2.0, 1.0);
        }
        static void TestAllMethods(Func<double, double> f, double a, double b, double x0)
        {
            var dichotomy = new Dichotomy(eps: 1e-6);
            var golden = new GoldenSection(eps: 1e-6);
            var fib = new Fibonacci(eps: 1e-6);
            var newton = new Newton(eps: 1e-10);

            var r1 = dichotomy.Minimize(f, a, b);
            var r2 = golden.Minimize(f, a, b);
            var r3 = fib.Minimize(f, a, b);
            var r4 = newton.Minimize(f, a, b, x0);

            Console.WriteLine($"Dichotomy: x*={r1.X:F6}, f(x*)={r1.FValue:F6}, iterarion={r1.Iterations}");
            Console.WriteLine($"GoldenSection: x*={r2.X:F6}, f(x*)={r2.FValue:F6}, iterarion={r2.Iterations}");
            Console.WriteLine($"Fibonachi: x*={r3.X:F6}, f(x*)={r3.FValue:F6}, iterarion={r3.Iterations}");
            Console.WriteLine($"Newton: x*={r4.X:F6}, f(x*)={r4.FValue:F6}, iterarion={r4.Iterations}");
        }
    }
}