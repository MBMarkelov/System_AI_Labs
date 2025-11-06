using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.WinForms;

namespace WinFormsApp1
{
    public class KnnDataView
    {
        // 1. Метод для построения графика с использованием ScottPlot
        public static void PlotData(List<(double[] Features, string Label)> data, string title = "Классификация студентов")
        {
            var plt = new ScottPlot.Plot();
            plt.Title(title);
            plt.XLabel("Средний балл");
            plt.YLabel("Количество пропусков");

            // Группируем данные по категориям
            var excellent = data.Where(d => d.Label == "Отл").ToList();
            var good = data.Where(d => d.Label == "Хор").ToList();
            var satisfactory = data.Where(d => d.Label == "удовл").ToList();
            var unsatisfactory = data.Where(d => d.Label == "неуд").ToList();

            ScottPlot.Plottables.Scatter? spExcellent = null;
            ScottPlot.Plottables.Scatter? spGood = null;
            ScottPlot.Plottables.Scatter? spSatisfactory = null;
            ScottPlot.Plottables.Scatter? spUnsatisfactory = null;

            // Добавляем точки для каждой категории
            if (excellent.Count > 0)
            {
                var xs = excellent.Select(d => d.Features[0]).ToArray();
                var ys = excellent.Select(d => d.Features[1]).ToArray();
                spExcellent = plt.Add.Scatter(xs, ys);
                spExcellent.LegendText = "Отличники";
                spExcellent.MarkerSize = 10;
                spExcellent.MarkerShape = ScottPlot.MarkerShape.FilledDiamond;
                spExcellent.Color = ScottPlot.Colors.Green;
                spExcellent.LineStyle = ScottPlot.LineStyle.None;
            }

            if (good.Count > 0)
            {
                var xs = good.Select(d => d.Features[0]).ToArray();
                var ys = good.Select(d => d.Features[1]).ToArray();
                spGood = plt.Add.Scatter(xs, ys);
                spGood.LegendText = "Хорошисты";
                spGood.MarkerSize = 8;
                spGood.MarkerShape = ScottPlot.MarkerShape.FilledCircle;
                spGood.Color = ScottPlot.Colors.Blue;
                spGood.LineStyle = ScottPlot.LineStyle.None;
            }

            if (satisfactory.Count > 0)
            {
                var xs = satisfactory.Select(d => d.Features[0]).ToArray();
                var ys = satisfactory.Select(d => d.Features[1]).ToArray();
                spSatisfactory = plt.Add.Scatter(xs, ys);
                spSatisfactory.LegendText = "Удовлетворительно";
                spSatisfactory.MarkerSize = 7;
                spSatisfactory.MarkerShape = ScottPlot.MarkerShape.FilledTriangleDown;
                spSatisfactory.Color = ScottPlot.Colors.Orange;
                spSatisfactory.LineStyle = ScottPlot.LineStyle.None;
            }

            if (unsatisfactory.Count > 0)
            {
                var xs = unsatisfactory.Select(d => d.Features[0]).ToArray();
                var ys = unsatisfactory.Select(d => d.Features[1]).ToArray();
                spUnsatisfactory = plt.Add.Scatter(xs, ys);
                spUnsatisfactory.LegendText = "Неудовлетворительно";
                spUnsatisfactory.MarkerSize = 6;
                spUnsatisfactory.MarkerShape = ScottPlot.MarkerShape.FilledSquare;
                spUnsatisfactory.Color = ScottPlot.Colors.Red;
                spUnsatisfactory.LineStyle = ScottPlot.LineStyle.None;
            }

            // Включаем легенду
            plt.ShowLegend();

            // Сохраняем график в файл
            string filename = $"{title.Replace(" ", "_")}.png";
            plt.SavePng(filename, 800, 600);
            Console.WriteLine($"График сохранен в файл: {filename}");

            // Отображаем в окне
            ShowPlotInWindow(plt, title);
        }

        // Метод для отображения графика в отдельном окне
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
            formsPlot.Reset(plt); // Передаём готовый Plot
            formsPlot.Refresh();  // Обновляем отрисовку
            form.Controls.Add(formsPlot);
            form.ShowDialog();
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

            // === 1. Обучающие данные (фон) ===
            PlotScatterGroup(plt, trainingData, "Обучение: Отличники", "Отл", ScottPlot.Colors.Green, MarkerShape.FilledDiamond, 10);
            PlotScatterGroup(plt, trainingData, "Обучение: Хорошисты", "Хор", ScottPlot.Colors.Blue, MarkerShape.FilledCircle, 8);
            PlotScatterGroup(plt, trainingData, "Обучение: Удовлетворительно", "удовл", ScottPlot.Colors.Orange, MarkerShape.FilledTriangleDown, 7);
            PlotScatterGroup(plt, trainingData, "Обучение: Неудовлетворительно", "неуд", ScottPlot.Colors.Red, MarkerShape.FilledSquare, 6);

            // === 2. Тестовые данные: с предсказаниями и подсветкой ошибок ===
            var correct = new List<(double[] Features, string Label)>();
            var incorrect = new List<(double[] Features, string Label)>();

            for (int i = 0; i < testData.Count; i++)
            {
                if (testData[i].Label == predictions[i])
                    correct.Add(testData[i]);
                else
                    incorrect.Add(testData[i]);
            }

            // Правильные — зелёная обводка
            PlotTestPoints(plt, correct, "Тест: Верно", ScottPlot.Colors.Green);

            // Ошибки — красная обводка
            PlotTestPoints(plt, incorrect, "Тест: Ошибка", ScottPlot.Colors.Red);

            // === Сохранение и отображение ===
            string filename = $"{title.Replace(" ", "_")}.png";
            plt.SavePng(filename, 1000, 700);
            Console.WriteLine($"График сохранён: {filename}");

            ShowPlotInWindow(plt, title);
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
    }
}