using ML_Labs;

public class KnnWithNormalization : KnnClassifier
{
    private readonly NormalizationType normalizationType;

    private double[] min, max, mean, std, median, q1, q3, maxAbs;
    private bool scalerFitted = false;

    public KnnWithNormalization(int k = 5, NormalizationType normalization = NormalizationType.Standard)
        : base(k)
    {
        this.normalizationType = normalization;
    }

    public override void Train(double[] features, string label)
    {
        base.Train(features, label); 

        if (!scalerFitted)
        {
            FitScaler();
            scalerFitted = true;

            for (int i = 0; i < trainingData.Count; i++)
            {
                var (f, l) = trainingData[i];
                trainingData[i] = (Scale(f), l);
            }
        }
    }

    public override string Classify(double[] features)
    {
        if (!scalerFitted && normalizationType != NormalizationType.None)
            throw new InvalidOperationException("Модель не обучена или нормализация не применялась");

        double[] input = normalizationType == NormalizationType.None
            ? features
            : Scale(features);

        return base.Classify(input); 
    }


    private void FitScaler()
    {
        if (trainingData.Count == 0) return;

        int dim = trainingData[0].Features.Length;

        min = new double[dim]; max = new double[dim];
        mean = new double[dim]; std = new double[dim];
        median = new double[dim]; q1 = new double[dim];
        q3 = new double[dim]; maxAbs = new double[dim];

        var valuesByFeature = new List<double>[dim];
        for (int i = 0; i < dim; i++) valuesByFeature[i] = new List<double>();

        Array.Fill(min, double.MaxValue);
        Array.Fill(max, double.MinValue);

        foreach (var (features, _) in trainingData)
        {
            for (int j = 0; j < dim; j++)
            {
                double v = features[j];
                valuesByFeature[j].Add(v);

                min[j] = Math.Min(min[j], v);
                max[j] = Math.Max(max[j], v);
                mean[j] += v;
                maxAbs[j] = Math.Max(maxAbs[j], Math.Abs(v));
            }
        }

        for (int j = 0; j < dim; j++) mean[j] /= trainingData.Count;

        for (int j = 0; j < dim; j++)
        {
            var list = valuesByFeature[j];
            list.Sort();

            median[j] = Percentile(list, 50);
            q1[j] = Percentile(list, 25);
            q3[j] = Percentile(list, 75);

            double variance = trainingData.Sum(t => Math.Pow(t.Features[j] - mean[j], 2)) / (trainingData.Count - 1);
            std[j] = trainingData.Count > 1 ? Math.Sqrt(variance) : 1.0;
        }
    }

    private double[] Scale(double[] features)
    {
        if (normalizationType == NormalizationType.None || !scalerFitted)
            return features.ToArray();

        var result = new double[features.Length];

        for (int i = 0; i < features.Length; i++)
        {
            double x = features[i];

            result[i] = normalizationType switch
            {
                NormalizationType.MinMax => (max[i] - min[i] > 1e-12) ? (x - min[i]) / (max[i] - min[i]) : 0,
                NormalizationType.Standard => std[i] > 1e-12 ? (x - mean[i]) / std[i] : 0,
                NormalizationType.Robust => (q3[i] - q1[i] > 1e-12) ? (x - median[i]) / (q3[i] - q1[i]) : 0,
                NormalizationType.MaxAbs => maxAbs[i] > 1e-12 ? x / maxAbs[i] : 0,
                _ => x
            };
        }
        return result;
    }

    private static double Percentile(List<double> sorted, double p)
    {
        if (sorted.Count == 0) return 0;
        if (sorted.Count == 1) return sorted[0];

        double index = (p / 100.0) * (sorted.Count - 1);
        int lo = (int)index;
        double frac = index - lo;

        if (lo + 1 >= sorted.Count) return sorted[^1];
        return sorted[lo] + frac * (sorted[lo + 1] - sorted[lo]);
    }
}