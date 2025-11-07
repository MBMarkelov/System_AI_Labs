using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs
{
    public interface IKnnClassifier
    {
        void Train(double[] features, string label);
        string Classify(double[] features);
        double Evaluate(List<(double[] Features, string Label)> testData);
    }
}
