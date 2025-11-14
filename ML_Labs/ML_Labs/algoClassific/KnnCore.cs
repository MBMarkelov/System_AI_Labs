using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_Labs
{
    public enum KernelType
    {
        Gaussian,
        Epanechnikov,
        Uniform
    }

    public class KernelKnnClassifier : IKnnClassifier
    {
        private readonly int k = 10;
        private readonly double bandwidth = 0.7;
        private readonly KernelType kernel = KernelType.Gaussian;
        private readonly List<(double[] Features, string Label)> trainingData = new();

        public KernelKnnClassifier(){}
        public KernelKnnClassifier(int k, double bandwidth = 1.0, KernelType kernel = KernelType.Gaussian)
        {
            if (k <= 0) throw new ArgumentException("k > 0");
            if (bandwidth <= 0) throw new ArgumentException("bandwidth > 0");
            this.k = k;
            this.bandwidth = bandwidth;
            this.kernel = kernel;
        }

        public void Train(double[] features, string label)
        {
            trainingData.Add((features, label));
        }

        public string Classify(double[] features)
        {
            if (trainingData.Count == 0)
                throw new InvalidOperationException("Нет данных");

            var weightedNeighbors = trainingData
                .Select(t => new
                {
                    Distance = CalculateDistance(features, t.Features),
                    Label = t.Label
                })
                .OrderBy(n => n.Distance)
                .Take(k)
                .Select(n => new
                {
                    n.Label,
                    Weight = KernelWeight(n.Distance)
                });

            var labelScore = weightedNeighbors
                .GroupBy(n => n.Label)
                .Select(g => new
                {
                    Label = g.Key,
                    Score = g.Sum(x => x.Weight)
                })
                .OrderByDescending(g => g.Score)
                .First();

            return labelScore.Label;
        }

        private double KernelWeight(double distance)
        {
            double u = distance / bandwidth;

            return kernel switch
            {
                KernelType.Gaussian => Math.Exp(-0.5 * u * u),
                KernelType.Epanechnikov => u <= 1 ? 0.75 * (1 - u * u) : 0,
                KernelType.Uniform => u <= 1 ? 1.0 : 0,
                _ => throw new NotImplementedException()
            };
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
            if (testData.Count == 0) return 0.0;
            int correct = 0;
            foreach (var (f, l) in testData)
                if (Classify(f) == l) correct++;
            return (double)correct / testData.Count;
        }
    }
}