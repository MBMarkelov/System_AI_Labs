using ML_Labs;
using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class KnnDataView
    {
        public static void PlotMap(
            List<(double[] Features, string Label)> trainingData,
            List<(double[] Features, string Label)> testData,
            List<string> predictions,
            List<(double[] Features, string Label)> prototypes,
            string title = "STOLP: эталоны и удалённые точки")
        {
            var pointsForKde = trainingData.Select(d => d.Features).ToList();
            var kde = new KernelDensity2D(pointsForKde);
            var (density, minX, maxX, minY, maxY) = kde.GetHeatmap(width: 200, height: 200);

            var plt = new ScottPlot.Plot();
            plt.Title("KNN + STOLP + Ядерное сглаживание (KDE фон)");
            plt.XLabel("Средний балл");
            plt.YLabel("Количество пропусков");

            var hm = plt.Add.Heatmap(density);
            hm.Rectangle = new CoordinateRect(minX, maxX, minY, maxY);
            hm.FlipVertically = true;

            foreach (var p in prototypes)
            {
                var sp = plt.Add.Scatter(p.Features[0], p.Features[1]);
                sp.Color = GetColor(p.Label);
                sp.MarkerSize = 12;
                sp.MarkerShape = ScottPlot.MarkerShape.Eks;
            }

            for (int i = 0; i < testData.Count; i++)
            {
                var point = testData[i];
                bool isCorrect = point.Label == predictions[i];

                var sp = plt.Add.Scatter(point.Features[0], point.Features[1]);
                sp.MarkerSize = 10;
                sp.MarkerShape = isCorrect ? ScottPlot.MarkerShape.FilledCircle : ScottPlot.MarkerShape.OpenCircle;
                sp.Color = isCorrect ? ScottPlot.Colors.Lime : ScottPlot.Colors.Red;
                sp.MarkerLineWidth = 3;
                sp.LegendText = isCorrect ? "Тест: верно" : "Тест: ошибка";
            }

            plt.Axes.SetLimits(minX, maxX, minY, maxY);

            ShowPlotInWindow(plt, "KNN + KDE + STOLP");
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
        public static void PlotEvaluation(
            List<(double[] Features, string Label)> trainingData,
            List<(double[] Features, string Label)> testData,
            List<string> predictions,
            double accurence,
            string title = "amogus"
            )
        {
            var plt = new ScottPlot.Plot();
            plt.Title($"Точность: { accurence.ToString()}");
            plt.XLabel("Средний балл");
            plt.YLabel("Количество пропусков");

            PlotScatterGroup(plt, trainingData, "Обучение: Отличники", "Отл", ScottPlot.Colors.Green, MarkerShape.FilledDiamond, 10);
            PlotScatterGroup(plt, trainingData, "Обучение: Хорошисты", "Хор", ScottPlot.Colors.Blue, MarkerShape.FilledCircle, 8);
            PlotScatterGroup(plt, trainingData, "Обучение: Удовлетворительно", "удовл", ScottPlot.Colors.Orange, MarkerShape.FilledTriangleDown, 7);
            PlotScatterGroup(plt, trainingData, "Обучение: Неудовлетворительно", "неуд", ScottPlot.Colors.Red, MarkerShape.FilledSquare, 6);

            var correct = new List<(double[] Features, string Label)>();
            var incorrect = new List<(double[] Features, string Label)>();
            var errorGroups = new Dictionary<(string True, string Pred), List<double[]>>();

            for (int i = 0; i < testData.Count; i++)
            {
                var point = testData[i];
                double x = point.Features[0];
                double y = point.Features[1];
                string trueLabel = point.Label;
                string predLabel = predictions[i];

                if (trueLabel == predLabel)
                {
                    var sp = plt.Add.Scatter(new[] { x }, new[] { y });
                    sp.MarkerSize = 12;
                    sp.MarkerShape = ScottPlot.MarkerShape.FilledCircle;
                    sp.Color = ScottPlot.Colors.Green;
                    sp.LegendText = $"{trueLabel} → {predLabel}";  

                    string text = $"{trueLabel} → {predLabel}";
                    var txt = plt.Add.Text(text, x, y);
                    txt.FontSize = 10;
                    txt.FontColor = ScottPlot.Colors.Red;
                    txt.Bold = true;
                    txt.OffsetX = 15;
                    txt.OffsetY = -10;
                }
                else
                {
                    var sp = plt.Add.Scatter(new[] { x }, new[] { y });
                    sp.MarkerSize = 14;
                    sp.MarkerShape = ScottPlot.MarkerShape.OpenCircle;
                    sp.Color = ScottPlot.Colors.Red;
                    sp.MarkerLineWidth = 3;
                    sp.LegendText = $"{trueLabel} → {predLabel}";

                    string text = $"{trueLabel} → {predLabel}";
                    var txt = plt.Add.Text(text, x, y);
                    txt.FontSize = 10;
                    txt.FontColor = ScottPlot.Colors.Red;
                    txt.Bold = true;
                    txt.OffsetX = 15;
                    txt.OffsetY = -10;
                }
            }

            PlotTestPoints(plt, correct, "Тест: Верно", ScottPlot.Colors.Green);

            PlotTestPoints(plt, incorrect, "Тест: Ошибка", ScottPlot.Colors.Red);

            string filename = $"{title.Replace(" ", "_")}.png";
            plt.SavePng(filename, 1000, 700);
            Console.WriteLine($"График сохранён: {filename}");

            ShowPlotInWindow(plt, title);
        }

        public static void PlotOverdoseEvaluation(
            List<(double[] Features, string Label)> trainingData,
            List<(double[] Features, string Label)> testData,
            List<string> predictions,
            double accuracy,
            string[] featureNames)
        {
            var plt = new ScottPlot.Plot();
            plt.Title($"KNN: До/После 2010 | Точность: {accuracy:P2}");
            plt.XLabel("Смертность (ESTIMATE)");
            plt.YLabel("Код группы (STUB_LABEL_NUM)");

            int xIdx = 5;
            int yIdx = 3;

            PlotScatterGroup(plt, trainingData, "Обучение: До 2010", "0", Colors.Blue, MarkerShape.FilledCircle, 6);
            PlotScatterGroup(plt, trainingData, "Обучение: После 2010", "1", Colors.Orange, MarkerShape.FilledDiamond, 6);

            var correct = new List<(double x, double y)>();
            var incorrect = new List<(double x, double y)>();

            for (int i = 0; i < testData.Count; i++)
            {
                double x = testData[i].Features[xIdx];
                double y = testData[i].Features[yIdx];
                if (testData[i].Label == predictions[i])
                    correct.Add((x, y));
                else
                    incorrect.Add((x, y));
            }

            foreach (var (x, y) in correct)
            {
                plt.Add.Circle(x, y, 1);
                plt.Add.Circle(x, y, 1);
            }

            foreach (var (x, y) in incorrect)
            {
                plt.Add.Circle(x, y, 1);
            }

            plt.Legend.IsVisible = true;
            plt.Axes.AutoScale();

            string filename = "KNN_Overdose_Classification.png";
            plt.SavePng(filename, 1000, 700);
            Console.WriteLine($"График сохранён: {filename}");

            ShowPlotInWindow(plt, "KNN: Передозировки — До/После 2010");
        }
        private static void PlotScatterGroup(
            ScottPlot.Plot plt,
            List<(double[] Features, string Label)> data,
            string legendText,
            string label,
            ScottPlot.Color color,
            ScottPlot.MarkerShape shape,
            float size)
        {
            var group = data.Where(d => d.Label == label).ToList();
            if (group.Count == 0) return;

            var xs = group.Select(d => d.Features[0]).ToArray();
            var ys = group.Select(d => d.Features[1]).ToArray();

            var sp = plt.Add.Scatter(xs, ys);
            sp.LegendText = legendText;
            sp.MarkerSize = size;
            sp.MarkerShape = shape;
            sp.Color = color;
            sp.LineStyle = ScottPlot.LineStyle.None;
        }

        private static void PlotTestPoints(
            ScottPlot.Plot plt,
            List<(double[] Features, string Label)> points,
            string legendText,
            ScottPlot.Color borderColor)
        {
            if (points.Count == 0) return;

            var xs = points.Select(d => d.Features[0]).ToArray();
            var ys = points.Select(d => d.Features[1]).ToArray();

            var sp = plt.Add.Scatter(xs, ys);
            sp.LegendText = legendText;
            sp.MarkerSize = 12;
            sp.MarkerShape = ScottPlot.MarkerShape.OpenCircle;
            sp.Color = ScottPlot.Colors.Transparent;
            sp.MarkerLineWidth = 3;
            sp.MarkerLineColor = borderColor;
            sp.LineStyle = ScottPlot.LineStyle.None;
        }
        
        
        public static void PlotStolpResult(
            List<(double[] Features, string Label)> fullData,
            List<(double[] Features, string Label)> prototypes,
            string title = "STOLP: эталоны и удалённые точки")
        {
            var plt = new ScottPlot.Plot();
            plt.Title(title);
            plt.XLabel("Средний балл");
            plt.YLabel("Количество пропусков");

            foreach (var label in new[] { "Отл", "Хор", "удовл", "неуд" })
            {
                var points = fullData.Where(d => d.Label == label).ToList();
                if (points.Count == 0) continue;
                var xs = points.Select(d => d.Features[0]).ToArray();
                var ys = points.Select(d => d.Features[1]).ToArray();
                var sp = plt.Add.Scatter(xs, ys);
                sp.Color = GetColor(label).WithAlpha(50);
                sp.MarkerSize = 6;
                sp.LineStyle = ScottPlot.LineStyle.None;
                sp.LegendText = $"Фон: {label}";
            }

            foreach (var p in prototypes)
            {
                var sp = plt.Add.Scatter(p.Features[0], p.Features[1]);
                sp.Color = GetColor(p.Label);
                sp.MarkerSize = 14;
                sp.MarkerShape = ScottPlot.MarkerShape.Asterisk;
                sp.LegendText = $"Эталон: {p.Label}";
            }

            plt.ShowLegend();
            ShowPlotInWindow(plt, title);
        }

        private static ScottPlot.Color GetColor(string label) => label switch
        {
            "Отл" => ScottPlot.Colors.Green,
            "Хор" => ScottPlot.Colors.Blue,
            "удовл" => ScottPlot.Colors.Orange,
            "неуд" => ScottPlot.Colors.Red,
            _ => ScottPlot.Colors.Gray
        };
    }
}