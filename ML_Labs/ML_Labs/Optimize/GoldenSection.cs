using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs.Optimize
{
    public class GoldenSection
    {
        private readonly double eps;
        private readonly int maxIter;
        private readonly double phi = (1 + Math.Sqrt(5)) / 2.0;

        public GoldenSection(double eps = 1e-6, int maxIter = 1000)
        {
            this.eps = eps;
            this.maxIter = maxIter;
        }

        public (double X, double FValue, int Iterations) Minimize(Func<double, double> f, double a, double b)
        {
            int it = 0;
            double invphi = 1.0 / phi;
            double x1 = b - invphi * (b - a);
            double x2 = a + invphi * (b - a);
            double f1 = f(x1);
            double f2 = f(x2);

            while ((b - a) > eps && it < maxIter)
            {
                if (f1 <= f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = b - invphi * (b - a);
                    f1 = f(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + invphi * (b - a);
                    f2 = f(x2);
                }
                it++;
            }

            double x = (a + b) / 2.0;
            return (x, f(x), it);
        }
    }
}
