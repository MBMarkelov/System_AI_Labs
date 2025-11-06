using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_Labs
{
    class Program
    {
        static void Main()
        {
            var knn = new KnnClassifier(k: 3);

            var trainingData = StudentDataGenerator.Generate(100);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);

            var testData = StudentDataGenerator.Generate(30);

            double accuracy = knn.Evaluate(testData);
            Console.WriteLine($"Точность на тестовых данных: {accuracy:P2}");

            double[] student = [4.2, 8];
            string predicted = knn.Classify(student);

            Console.WriteLine($"Студент {predicted}");
        }
    }
}