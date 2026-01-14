using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs
{
    public class KnnClassifier : IKnnClassifier
    {
        protected readonly int k;
        protected readonly List<(double[] Features, string Label)> trainingData = new();

        public KnnClassifier(int k = 3)
        {
            if (k <= 0) throw new ArgumentException("k должно быть больше 0", nameof(k));
            this.k = k;
        }

        public virtual void Train(double[] features, string label)
        {
            if (features == null) throw new ArgumentNullException(nameof(features));
            trainingData.Add((features.ToArray(), label));
        }

        public virtual string Classify(double[] features)
        {
            if (trainingData.Count == 0) throw new InvalidOperationException("Нет данных для обучения");

            var neighbors = trainingData
                .Select(t => new
                {
                    Distance = EuclideanDistance(features, t.Features),
                    Label = t.Label
                })
                .OrderBy(x => x.Distance)
                .Take(k);

            return neighbors
                .GroupBy(n => n.Label)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }

        public virtual double Evaluate(List<(double[] Features, string Label)> testData)
        {
            int correct = 0;
            foreach (var (f, label) in testData)
            {
                if (Classify(f) == label) correct++;
            }
            return (double)correct / testData.Count;
        }

        protected static double EuclideanDistance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double d = a[i] - b[i];
                sum += d * d;
            }
            return Math.Sqrt(sum);
        }

    }
}
