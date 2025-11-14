using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs
{
    public class OverdoseKnnEvaluator
    {
        public (List<(double[] Features, string Label)> Train, List<(double[] Features, string Label)> Test) SplitData(
            List<(double[] Features, string Label)> data, double trainRatio = 0.8, int? seed = 42)
        {
            var random = seed.HasValue ? new Random(seed.Value) : new Random();
            var shuffled = data.OrderBy(x => random.Next()).ToList();
            int split = (int)(shuffled.Count * trainRatio);
            return (shuffled.Take(split).ToList(), shuffled.Skip(split).ToList());
        }

        public (List<string> Predictions, double Accuracy) RunKnn(
            List<(double[] Features, string Label)> train,
            List<(double[] Features, string Label)> test,
            int k = 5)
        {
            var knn = new KnnClassifier(k);
            foreach (var (f, l) in train)
                knn.Train(f, l);

            var predictions = test.Select(t => knn.Classify(t.Features)).ToList();
            double accuracy = knn.Evaluate(test);

            return (predictions, accuracy);
        }
    }
}
