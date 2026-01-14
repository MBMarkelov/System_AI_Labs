using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_Labs
{
    public class KnnMlsClassifier : IKnnClassifier
    {
        private readonly int k = 3;
        private List<(double[] Features, string Label)> trainingData = new();
        private double[] featureWeights; 
        private bool isTrained = false;

        public KnnMlsClassifier() { }

        public KnnMlsClassifier(int k)
        {
            if (k <= 0) throw new ArgumentException("k должно быть больше 0", nameof(k));
            this.k = k;
        }

        public void Train(double[] features, string label)
        {
            trainingData.Add((features, label));
            isTrained = false; 
        }

        public void TrainWeights()
        {
            if (trainingData.Count < 2) return;

            var labels = trainingData.Select(t => t.Label).Distinct().ToList();
            var labelToIndex = labels.Select((label, idx) => (label, idx)).ToDictionary(x => x.label, x => x.idx);

            int numFeatures = trainingData[0].Features.Length;
            featureWeights = new double[numFeatures];

            for (int i = 0; i < numFeatures; i++)
                featureWeights[i] = 1.0;

            OptimizeWeightsWithLeastSquares(labelToIndex);

            isTrained = true;
        }

        private void OptimizeWeightsWithLeastSquares(Dictionary<string, int> labelToIndex)
        {
            int numFeatures = featureWeights.Length;
            int numClasses = labelToIndex.Count;


            int numPairs = Math.Min(1000, trainingData.Count * (trainingData.Count - 1) / 2);
            var distances = new double[numPairs];
            var targets = new double[numPairs];

            var random = new Random();
            int pairIndex = 0;

            for (int i = 0; i < trainingData.Count && pairIndex < numPairs; i++)
            {
                for (int j = i + 1; j < trainingData.Count && pairIndex < numPairs; j++)
                {
                    bool sameClass = trainingData[i].Label == trainingData[j].Label;
                    targets[pairIndex] = sameClass ? 1.0 : 0.0;

                    double weightedDist = 0;
                    for (int f = 0; f < numFeatures; f++)
                    {
                        double diff = trainingData[i].Features[f] - trainingData[j].Features[f];
                        weightedDist += featureWeights[f] * diff * diff;
                    }
                    distances[pairIndex] = Math.Sqrt(weightedDist);

                    pairIndex++;
                }
            }

            double learningRate = 0.01;
            int epochs = 100;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double totalError = 0;

                for (int p = 0; p < numPairs; p++)
                {
                    double normalizedDist = distances[p] / (1.0 + distances[p]);
                    double error = targets[p] - normalizedDist;
                    totalError += error * error;

                    for (int f = 0; f < numFeatures; f++)
                    {
                        double gradient = -2 * error * normalizedDist / (featureWeights[f] + 1e-9);
                        featureWeights[f] -= learningRate * gradient;

                        featureWeights[f] = Math.Max(0.1, Math.Min(10.0, featureWeights[f]));
                    }
                }
            }
        }

        public string Classify(double[] features)
        {
            if (trainingData.Count == 0) throw new InvalidOperationException("Нет данных для обучения");

            if (!isTrained) TrainWeights();

            var neighbors = trainingData
                .Select(t => new
                {
                    WeightedDistance = CalculateWeightedDistance(features, t.Features),
                    t.Label
                })
                .OrderBy(t => t.WeightedDistance)
                .Take(k);

            var labelScores = neighbors
                .GroupBy(n => n.Label)
                .Select(g => new
                {
                    Label = g.Key,
                    Score = g.Sum(x => 1.0 / (x.WeightedDistance + 1e-9))
                })
                .OrderByDescending(g => g.Score)
                .First();

            return labelScores.Label;
        }

        private double CalculateWeightedDistance(double[] a, double[] b)
        {
            if (!isTrained)
            {
                return CalculateEuclideanDistance(a, b);
            }

            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += featureWeights[i] * diff * diff;
            }
            return Math.Sqrt(sum);
        }

        private static double CalculateEuclideanDistance(double[] a, double[] b)
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
            if (!isTrained) TrainWeights();

            int correct = 0;
            foreach (var (features, label) in testData)
            {
                var predicted = Classify(features);
                if (predicted == label) correct++;
            }
            return (double)correct / testData.Count;
        }
        public double[] GetFeatureWeights() => featureWeights?.ToArray() ?? new double[0];
    }
}