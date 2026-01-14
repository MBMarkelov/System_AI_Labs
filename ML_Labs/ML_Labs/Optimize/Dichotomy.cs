using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs.Optimize
{
    public class Dichotomy
    {
        private readonly double eps;
        private readonly double delta;
        private readonly int maxIter;

        public Dichotomy(double eps = 1e-6, double? delta = null, int maxIter = 1000)
        {
            this.eps = eps;
            this.delta = delta ?? eps / 2.0;
            this.maxIter = maxIter;
        }

        public (double X, double FValue, int Iterations) Minimize(Func<double, double> f, double a, double b)
        {
            int it = 0;
            while ((b - a) / 2.0 > eps && it < maxIter)
            {
                double mid = (a + b) / 2.0;
                double x1 = mid - delta;
                double x2 = mid + delta;
                double f1 = f(x1);
                double f2 = f(x2);

                if (f1 <= f2)
                    b = x2;
                else
                    a = x1;

                it++;
            }
            double x = (a + b) / 2.0;
            return (x, f(x), it);
        }
    }
}
