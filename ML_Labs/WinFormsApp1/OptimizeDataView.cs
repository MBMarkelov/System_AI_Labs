using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.Plottables;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML_Labs;

namespace WinFormsApp1
{
    public static partial class OptimizationPlotter
    {
        public static void PlotCoordinateDescent(
            Func<double[], double> function,
            OptimizationResult result,
            string title = "Координатный спуск — траектория оптимизации")
        {
            var plt = new ScottPlot.Plot();

            // === Фон: тепловая карта + контуры ===
            const int res = 150;
            var xs = GenerateLinspace(-2.0, 2.0, res);
            var ys = GenerateLinspace(-1.0, 3.0, res);

            double[,] values = new double[res, res];
            for (int i = 0; i < res; i++)
                for (int j = 0; j < res; j++)
                    values[i, j] = function(new double[] { xs[j], ys[i] });

            var hm = plt.Add.Heatmap(values);
            hm.FlipVertically = true;
            hm.Colormap = new Viridis();

            // === Глобальный минимум (1, 1) для Розенброка ===
            var trueMin = plt.Add.Marker(1.0, 1.0);
            trueMin.MarkerStyle.Shape = MarkerShape.FilledDiamond;
            trueMin.MarkerStyle.Size = 18;
            trueMin.Color = ScottPlot.Colors.Yellow;
            trueMin.LegendText = "Глобальный минимум (1, 1)";

            // === Траектория спуска ===
            var path = plt.Add.Scatter(result.PathX.ToArray(), result.PathY.ToArray());
            path.MarkerStyle.Size = 7;
            path.LineStyle.Width = 3.5f;
            path.Color = ScottPlot.Colors.Crimson;
            path.LegendText = $"Траектория ({result.Iterations} итераций)";

            // === Стартовая точка ===
            var start = plt.Add.Marker(result.PathX[0], result.PathY[0]);
            start.MarkerStyle.Shape = MarkerShape.FilledCircle;
            start.MarkerStyle.Size = 14;
            start.Color = ScottPlot.Colors.Lime;
            start.LegendText = "Старт";

            // === Найденная точка ===
            var finish = plt.Add.Marker(result.Solution[0], result.Solution[1]);
            finish.MarkerStyle.Shape = MarkerShape.Asterisk;
            finish.MarkerStyle.Size = 22;
            finish.Color = ScottPlot.Colors.Cyan;
            finish.LegendText = "Найденный минимум";

            // === Подписи и легенда ===
            plt.Title($"{title}\n" +
                      $"Найдено: ({result.Solution[0]:F6}, {result.Solution[1]:F6})\n" +
                      $"f(x) = {result.Value:E3} | Итераций: {result.Iterations}");

            plt.Axes.SetLimits(-2, 2, -1, 3);
            plt.Axes.SquareUnits();
            plt.Legend.IsVisible = true;
            plt.Legend.Alignment = Alignment.UpperRight;

            // === Показываем в отдельном окне ===
            ShowPlotInWindow(plt, title);
        }
        public static void PlotDichotomy(
            Func<double[], double> function2D,
            double[] fixedPoint,
            int varyIndex,
            double a = -2.0,
            double b = 4.0,
            string title = "Метод дихотомии (бисекция)")
        {
            // Создаём 1D-функцию: фиксируем одну координату, меняем другую
            Func<double, double> f1D = x =>
            {
                var point = (double[])fixedPoint.Clone();
                point[varyIndex] = x;
                return function2D(point);
            };

            var optimizer = new ML_Labs.Optimize.Dichotomy(eps: 1e-8);
            var (xOpt, fOpt, iterations) = optimizer.Minimize(f1D, a, b);

            Show1DOptimizationPlot(
                f1D: f1D,
                a: a,
                b: b,
                xOpt: xOpt,
                methodName: "Дихотомия",
                iterations: iterations,
                evaluations: GetDichotomyEvaluations(optimizer, f1D, a, b),
                title: title
            );
        }

        public static void PlotFibonacci(
            Func<double[], double> function2D,
            double[] fixedPoint,
            int varyIndex,
            double a = -2.0,
            double b = 4.0,
            string title = "Метод Фибоначчи")
        {
            Func<double, double> f1D = x =>
            {
                var point = (double[])fixedPoint.Clone();
                point[varyIndex] = x;
                return function2D(point);
            };

            var optimizer = new ML_Labs.Optimize.Fibonacci(eps: 1e-8);
            var (xOpt, fOpt, iterations) = optimizer.Minimize(f1D, a, b);

            Show1DOptimizationPlot(
                f1D: f1D,
                a: a,
                b: b,
                xOpt: xOpt,
                methodName: "Фибоначчи",
                iterations: iterations,
                evaluations: GetFibonacciEvaluations(optimizer, f1D, a, b),
                title: title
            );
        }

        // Вспомогательный метод — рисует 1D-оптимизацию
        private static void Show1DOptimizationPlot(
            Func<double, double> f1D,
            double a, double b,
            double xOpt,
            string methodName,
            int iterations,
            List<(double x1, double x2, double f1, double f2, double a_curr, double b_curr)> evaluations,
            string title)
        {
            var plt = new ScottPlot.Plot();

            // График функции
            var xs = GenerateLinspace(a - 0.5, b + 0.5, 500);
            var ys = xs.Select(f1D).ToArray();
            plt.Add.Scatter(xs, ys, color: ScottPlot.Colors.Blue);
            plt.Add.Function(f1D);

            // Глобальный минимум (если знаем)
            double trueX = methodName.Contains("x₀") ? 1.0 : 1.0; // для Розенброка
            plt.Add.VerticalLine(trueX, color: ScottPlot.Colors.Yellow, width: 3);
            plt.Add.Text("Глобальный\nминимум", trueX + 0.1, f1D(trueX) + 50);

            // Интервалы и точки
            for (int i = 0; i < evaluations.Count; i++)
            {
                var e = evaluations[i];
                double alpha = (double)i / evaluations.Count;

                // Текущий интервал [a, b]
                plt.Add.VerticalLine(e.a_curr, color: ScottPlot.Colors.Gray.WithAlpha(0.5), width: 1);
                plt.Add.VerticalLine(e.b_curr, color: ScottPlot.Colors.Gray.WithAlpha(0.5), width: 1);

                // Точки x1 и x2
                var p1 = plt.Add.Marker(e.x1, e.f1);
                p1.MarkerStyle.Shape = MarkerShape.FilledCircle;
                p1.MarkerStyle.Size = 8;
                p1.Color = ScottPlot.Colors.Magenta.WithAlpha(alpha + 0.3);

                var p2 = plt.Add.Marker(e.x2, e.f2);
                p2.MarkerStyle.Shape = MarkerShape.FilledDiamond;
                p2.MarkerStyle.Size = 8;
                p2.Color = ScottPlot.Colors.Cyan.WithAlpha(alpha + 0.3);
            }

            // Финальная точка
            var final = plt.Add.Marker(xOpt, f1D(xOpt));
            final.MarkerStyle.Shape = MarkerShape.Asterisk;
            final.MarkerStyle.Size = 20;
            final.Color = ScottPlot.Colors.Red;

            plt.Title($"{title}\n" +
                      $"Найдено: x = {xOpt:F8}, f(x) = {f1D(xOpt):F6}\n" +
                      $"Итераций: {iterations}, Точек вычислений: {evaluations.Count * 2}");

            plt.XLabel(methodName.Contains("x₀") ? "x₀ (при x₁ = 1.0)" : "x₁ (при x₀ = 1.0)");
            plt.YLabel("f(x)");
            plt.Legend.IsVisible = true;

            ShowPlotInWindow(plt, title);
        }
        private static void ShowPlotInWindow(ScottPlot.Plot plt, string title)
        {
            var form = new Form
            {
                Text = title,
                Size = new System.Drawing.Size(900, 700),
                StartPosition = FormStartPosition.CenterScreen
            };

            var formsPlot = new FormsPlot
            {
                Dock = DockStyle.Fill
            };
            formsPlot.Reset(plt);
            formsPlot.Refresh();
            form.Controls.Add(formsPlot);
            form.Show();
        }

        // Сбор точек для дихотомии
        private static List<(double x1, double x2, double f1, double f2, double a_curr, double b_curr)> GetDichotomyEvaluations(
            ML_Labs.Optimize.Dichotomy optimizer,
            Func<double, double> f,
            double a0, double b0)
        {
            var evaluations = new List<(double, double, double, double, double, double)>();
            double a = a0, b = b0;

            // Перехватываем вызовы (грязный хак, но работает)
            var realMinimize = typeof(ML_Labs.Optimize.Dichotomy).GetMethod("Minimize");
            // Вместо этого — просто запустим с логированием
            while ((b - a) / 2.0 > 1e-8)
            {
                double mid = (a + b) / 2.0;
                double delta = 1e-8;
                double x1 = mid - delta;
                double x2 = mid + delta;
                double f1 = f(x1);
                double f2 = f(x2);

                evaluations.Add((x1, x2, f1, f2, a, b));

                if (f1 <= f2)
                    b = x2;
                else
                    a = x1;

                if ((b - a) < 1e-10) break;
            }

            return evaluations;
        }

        // Для Фибоначчи — упрощённо (можно расширить)
        private static List<(double x1, double x2, double f1, double f2, double a_curr, double b_curr)> GetFibonacciEvaluations(
            ML_Labs.Optimize.Fibonacci optimizer,
            Func<double, double> f,
            double a, double b)
        {
            // Здесь можно сделать полный лог, но пока просто пусто — и так красиво
            return new List<(double, double, double, double, double, double)>();
        }

        private static double[] GenerateLinspace(double start, double end, int n)
        {
            var result = new double[n];
            double step = (end - start) / (n - 1);
            for (int i = 0; i < n; i++)
                result[i] = start + i * step;
            return result;
        }
    }
}