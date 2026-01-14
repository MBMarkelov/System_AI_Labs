using System;
using System.Collections.Generic;

namespace ML_Labs.Optimize
{
    public class Fibonacci
    {
        private readonly double eps;
        private readonly int maxIter;

        public Fibonacci(double eps = 1e-6, int maxIter = 50)
        {
            this.eps = eps;
            this.maxIter = maxIter;
        }

        public (double X, double FValue, int Iterations) Minimize(Func<double, double> f, double a, double b)
        {
            var fib = GenerateFibonacciSequence((b - a) / eps);
            int n = fib.Count - 1;

            if (n < 1)
                return ((a + b) / 2.0, f((a + b) / 2.0), 0);

            double x1 = a + (double)fib[n - 2] / fib[n] * (b - a);
            double x2 = a + (double)fib[n - 1] / fib[n] * (b - a);

            double f1 = f(x1);
            double f2 = f(x2);

            int iterations = 0;

            for (int k = 1; k <= n - 1 && iterations < maxIter; k++)
            {
                iterations++;

                if (f1 > f2)
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;

                    if (k == n - 1)
                    {
                        x2 = a + 0.5 * (b - a); 
                        f2 = f(x2);
                    }
                    else
                    {
                        x2 = a + (double)fib[n - k - 1] / fib[n - k] * (b - a);
                        f2 = f(x2);
                    }
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;

                    if (k == n - 1)
                    {
                        x1 = a + 0.5 * (b - a);
                        f1 = f(x1);
                    }
                    else
                    {
                        x1 = a + (double)fib[n - k - 2] / fib[n - k] * (b - a);
                        f1 = f(x1);
                    }
                }

                if (Math.Abs(b - a) < eps)
                    break;
            }

            double x_optimal = (a + b) / 2.0;
            return (x_optimal, f(x_optimal), iterations);
        }
        private List<long> GenerateFibonacciSequence(double L_over_eps)
        {
            var fib = new List<long> { 1, 1 };

            while (fib[fib.Count - 1] < L_over_eps && fib.Count < maxIter + 10)
            {
                long nextFib = fib[fib.Count - 1] + fib[fib.Count - 2];
                fib.Add(nextFib);
            }

            return fib;
        }
    }
}