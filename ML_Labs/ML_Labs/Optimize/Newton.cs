using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs.Optimize
{
    public class Newton
    {
        private readonly double eps;
        private readonly int maxIter;
        private readonly double h;

        public Newton(double eps = 1e-8, int maxIter = 200, double h = 1e-6)
        {
            this.eps = eps;
            this.maxIter = maxIter;
            this.h = h;
        }

        private double Derivative(Func<double, double> f, double x)
            => (f(x + h) - f(x - h)) / (2.0 * h);

        private double SecondDerivative(Func<double, double> f, double x)
            => (f(x + h) - 2.0 * f(x) + f(x - h)) / (h * h);

        public (double X, double FValue, int Iterations) Minimize(Func<double, double> f, double a, double b, double x0)
        {
            double x = x0;
            int it = 0;
            for (it = 1; it <= maxIter; it++)
            {
                double g = Derivative(f, x);
                double gg = SecondDerivative(f, x);

                if (Math.Abs(gg) < 1e-12)
                    break;

                double step = -g / gg;
                double xNew = x + step;

                if (xNew < a) xNew = a;
                if (xNew > b) xNew = b;

                if (Math.Abs(xNew - x) < eps)
                {
                    x = xNew;
                    break;
                }
                x = xNew;
            }
            return (x, f(x), it);
        }
    }
}
