using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Labs
{
    /// <summary>
    /// Координатный спуск (Randomized Coordinate Descent) для минимизации функции f(x)
    /// </summary>
    public class CoordinateDescent
    {
        private readonly Random _rnd = new Random(42);

        /// <summary>
        /// Минимизирует функцию f(x)
        /// </summary>
        /// <param name="func">Целевая функция: double f(double[] x)</param>
        /// <param name="gradient">Градиент: double[] grad(double[] x). Можно передать null — будет численное дифференцирование</param>
        /// <param name="initialX">Начальная точка</param>
        /// <param name="options">Параметры алгоритма</param>
        /// <returns>Найденная точка минимума и значение функции в ней</returns>
        public OptimizeResult Minimize(
            Func<double[], double> func,
            Func<double[], double[]> gradient = null,
            double[] initialX = null,
            CDOptions options = null)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            options ??= new CDOptions();
            initialX ??= new double[options.Dimension];

            var n = initialX.Length;
            var x = (double[])initialX.Clone();
            var xNew = new double[n];

            // Если градиент не задан — используем численное дифференцирование
            var gradFunc = gradient ?? NumericalGradient(func, options.GradientEpsilon);

            double fCurrent = func(x);

            for (int iter = 0; iter < options.MaxIterations; iter++)
            {
                Array.Copy(x, xNew, n);

                // === Выбор координаты ===
                int coord = ChooseCoordinate(gradFunc, x, options.CoordinateSelection, n);

                // === Одномерный поиск по выбранной координате ===
                double step = OneDimensionalSearch(func, x, coord, options);

                // Обновляем только одну координату
                xNew[coord] = x[coord] + step;

                // Проверяем ограничения (если заданы)
                if (options.LowerBounds != null)
                    xNew[coord] = Math.Max(xNew[coord], options.LowerBounds[coord]);
                if (options.UpperBounds != null)
                    xNew[coord] = Math.Min(xNew[coord], options.UpperBounds[coord]);

                double fNew = func(xNew);

                // Проверка сходимости
                double diff = Math.Abs(fNew - fCurrent);
                if (diff < options.Tolerance)
                {
                    return new OptimizeResult(xNew, fNew, iter + 1, true, "Достигнута точность");
                }

                // L1-регуляризация (если нужна) — добавляем проксимальный оператор
                if (options.L1Lambda > 0)
                {
                    xNew[coord] = SoftThreshold(xNew[coord], options.L1Lambda * options.StepSize);
                }

                x = xNew;
                fCurrent = fNew;
                xNew = new double[n]; // для следующей итерации
            }

            return new OptimizeResult(x, fCurrent, options.MaxIterations, false, "Достигнуто максимальное число итераций");
        }

        private int ChooseCoordinate(Func<double[], double[]> gradFunc, double[] x, CoordinateSelectionRule rule, int n)
        {
            return rule switch
            {
                CoordinateSelectionRule.Cyclic => _iteration % n,
                CoordinateSelectionRule.Random => _rnd.Next(n),
                CoordinateSelectionRule.Greedy => GreedyCoordinate(gradFunc, x, n),
                CoordinateSelectionRule.GaussSouthwell => GaussSouthwellCoordinate(gradFunc, x, n),
                _ => _rnd.Next(n)
            };
        }

        private int _iteration = 0;

        private int GreedyCoordinate(Func<double[], double[]> grad, double[] x, int n)
        {
            var g = grad(x);
            int best = 0;
            double maxAbs = Math.Abs(g[0]);
            for (int i = 1; i < n; i++)
            {
                double absGi = Math.Abs(g[i]);
                if (absGi > maxAbs)
                {
                    maxAbs = absGi;
                    best = i;
                }
            }
            return best;
        }

        private int GaussSouthwellCoordinate(Func<double[], double[]> grad, double[] x, int n)
        {
            var g = grad(x);
            int best = 0;
            double maxVal = Math.Pow(g[0], 2);
            for (int i = 1; i < n; i++)
            {
                double val = Math.Pow(g[i], 2);
                if (val > maxVal)
                {
                    maxVal = val;
                    best = i;
                }
            }
            return best;
        }

        // Простой линейный поиск по координате (можно заменить на более точный)
        private double OneDimensionalSearch(Func<double[], double> func, double[] x, int coord, CDOptions opt)
        {
            double x0 = x[coord];
            double g = NumericalPartialDerivative(func, x, coord, opt.GradientEpsilon);

            // Если градиент почти ноль — ничего не делаем
            if (Math.Abs(g) < 1e-12) return 0.0;

            // Шаг в направлении антиградиента
            double step = -g * opt.StepSize;

            // Простейший backtracking line search (опционально)
            if (opt.UseLineSearch)
            {
                double alpha = opt.StepSize;
                double f0 = func(x);
                var xTest = (double[])x.Clone();

                while (alpha > 1e-12)
                {
                    xTest[coord] = x0 + alpha * (-g);
                    if (func(xTest) < f0 + opt.LineSearchC * alpha * g * (-g))
                        return alpha * (-g);
                    alpha *= opt.LineSearchBeta;
                }
                return 0; // не удалось уменьшить
            }

            return step;
        }

        // Численное вычисление частной производной по координате i
        private double NumericalPartialDerivative(Func<double[], double> f, double[] x, int i, double eps = 1e-8)
        {
            double xOld = x[i];
            x[i] = xOld + eps;
            double fPlus = f(x);
            x[i] = xOld - eps;
            double fMinus = f(x);
            x[i] = xOld;
            return (fPlus - fMinus) / (2 * eps);
        }

        // Численный градиент (если аналитический не задан)
        private Func<double[], double[]> NumericalGradient(Func<double[], double> f, double eps = 1e-8)
        {
            return x =>
            {
                var g = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                    g[i] = NumericalPartialDerivative(f, x, i, eps);
                return g;
            };
        }

        // Soft-thresholding (для L1-регуляризации)
        private double SoftThreshold(double x, double lambda)
        {
            if (x > lambda) return x - lambda;
            if (x < -lambda) return x + lambda;
            return 0.0;
        }
    }

    public enum CoordinateSelectionRule
    {
        Cyclic,          // по порядку
        Random,          // случайно
        Greedy,          // по максимуму |∇f_i|
        GaussSouthwell   // по максимуму (∇f_i)²
    }

    public class CDOptions
    {
        public int Dimension { get; set; } = 10;               // размерность задачи (если не известна заранее)
        public int MaxIterations { get; set; } = 10000;
        public double Tolerance { get; set; } = 1e-8;
        public double StepSize { get; set; } = 0.01;           // начальный размер шага
        public CoordinateSelectionRule CoordinateSelection { get; set; } = CoordinateSelectionRule.Random;

        // L1-регуляризация (Lasso)
        public double L1Lambda { get; set; } = 0.0;

        // Ограничения
        public double[] LowerBounds { get; set; } = null;
        public double[] UpperBounds { get; set; } = null;

        // Line search
        public bool UseLineSearch { get; set; } = true;
        public double LineSearchC { get; set; } = 1e-4;
        public double LineSearchBeta { get; set; } = 0.5;

        public double GradientEpsilon { get; set; } = 1e-8;
    }

    public class OptimizeResult
    {
        public double[] Solution { get; }
        public double Value { get; }
        public int Iterations { get; }
        public bool Converged { get; }
        public string Message { get; }

        public OptimizeResult(double[] solution, double value, int iterations, bool converged, string message)
        {
            Solution = solution;
            Value = value;
            Iterations = iterations;
            Converged = converged;
            Message = message;
        }
    }
    public class OptimizationResult
    {
        public string MethodName { get; set; } = "Неизвестный метод";
        public double[] Solution { get; set; } = Array.Empty<double>();
        public double Value { get; set; }
        public int Iterations { get; set; }
        public List<double> PathX { get; set; } = new();
        public List<double> PathY { get; set; } = new();
    }
}
