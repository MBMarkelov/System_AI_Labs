using ML_Labs.Optimize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs
{
    public class CoordinateDescentWithPath
    {
        private readonly CoordinateDescent _cd = new CoordinateDescent();

        public OptimizationResult Run(
            Func<double[], double> func,
            double[] start,
            CDOptions? options = null)
        {
            options ??= new CDOptions
            {
                Dimension = start.Length,
                MaxIterations = 50000,
                Tolerance = 1e-10,
                StepSize = 0.001,               // отлично работает на Розенброке
                CoordinateSelection = CoordinateSelectionRule.Cyclic,
                UseLineSearch = true
            };

            var pathX = new List<double> { start[0] };
            var pathY = new List<double> { start[1] };

            // Оборачиваем функцию, чтобы на каждой итерации сохранять точку
            double Wrapper(double[] x)
            {
                // Для двухмерной задачи (x.Length == 2)
                if (x.Length == 2)
                {
                    pathX.Add(x[0]);
                    pathY.Add(x[1]);
                }
                return func(x);
            }

            var result = _cd.Minimize(
                func: Wrapper,
                initialX: (double[])start.Clone(),
                options: options
            );

            var optResult = new OptimizationResult
            {
                MethodName = "Координатный спуск (твой мощный)",
                Solution = result.Solution,
                Value = result.Value,
                Iterations = result.Iterations,
                PathX = pathX,
                PathY = pathY
            };

            return optResult;
        }
    }
}
