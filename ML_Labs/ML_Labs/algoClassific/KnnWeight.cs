using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs
{
    public class KnnWeightClassifier : IKnnClassifier
    {
        private readonly int k = 3;
        private readonly List<(double[] Features, string Label)> trainingData = new();

        public KnnWeightClassifier() { }
        public KnnWeightClassifier(int k)
        {
            if (k <= 0) throw new ArgumentException("k должно быть больше 0", nameof(k));
            this.k = k;
        }

        public void Train(double[] features, string label)
        {
            trainingData.Add((features, label));
        }

        public string Classify(double[] features)
        {
            if (trainingData.Count == 0) throw new InvalidOperationException("No data for train");


            var neighbors = trainingData
                .Select(t =>
                {
                    var d = CalculateDistance(features, t.Features);
                    return new { Distance = d, t.Label };
                })
                .OrderBy(t => t.Distance)
                .Take(k);

            var labelScores = neighbors
                .GroupBy(n => n.Label)
                .Select(g => new
                {
                    Label = g.Key,
                    Score = g.Sum(x => 1.0 / (x.Distance + 1e-9))
                })
                .OrderByDescending(g => g.Score)
                .First();

            return labelScores.Label;
        }

        private static double CalculateDistance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }

        public double Evaluate(List<(double[] Features, string Label)> testData)
        {
            int correct = 0;
            foreach (var (features, label) in testData)
            {
                var predicted = Classify(features);
                if (predicted == label) correct++;
            }
            return (double)correct / testData.Count;
        }
    }

}
