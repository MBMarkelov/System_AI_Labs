using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_Labs.preprocessing
{
    public class StolpSelector
    {
        private readonly int prototypesPerClass;  // t — сколько эталонов на класс
        private readonly double outlierThreshold; // d — порог для выбросов
        private readonly int maxOutliersToRemove; // l — сколько выбросов удалить

        public StolpSelector(int prototypesPerClass = 5, double outlierThreshold = 1.5, int maxOutliersToRemove = 10)
        {
            this.prototypesPerClass = prototypesPerClass;
            this.outlierThreshold = outlierThreshold;
            this.maxOutliersToRemove = maxOutliersToRemove;
        }

        /// <summary>
        /// Отбирает эталоны из обучающей выборки
        /// </summary>
        public List<(double[] Features, string Label)> SelectPrototypes(
            List<(double[] Features, string Label)> data)
        {
            if (data.Count == 0) return new List<(double[] Features, string Label)>();

            // Шаг 1: Удаляем выбросы
            var cleanData = RemoveOutliers(data);

            // Шаг 2: Инициализация эталонов (по одному на класс с максимальным выступом)
            var prototypes = InitializePrototypes(cleanData);

            // Шаг 3: Итеративный отбор эталонов
            int targetCount = cleanData.Select(d => d.Label).Distinct().Count() * prototypesPerClass;
            while (prototypes.Count < targetCount && prototypes.Count < cleanData.Count)
            {
                var candidates = cleanData.Except(prototypes).ToList();
                if (candidates.Count == 0) break;

                var bestCandidate = candidates
                    .OrderBy(c => CalculateRisk(c, prototypes))
                    .First();

                prototypes.Add(bestCandidate);
            }

            Console.WriteLine($"STOLP: отобрано {prototypes.Count} эталонов из {data.Count} объектов " +
                              $"(сокращение: {100 - prototypes.Count * 100.0 / data.Count:F1}%)");

            return prototypes;
        }

        // Удаление выбросов по порогу риска
        private List<(double[] Features, string Label)> RemoveOutliers(
            List<(double[] Features, string Label)> data)
        {
            var risks = data.ToDictionary(
                d => d,
                d => OutlierRisk(d, data)
            );

            var outliers = risks
                .Where(kv => kv.Value > outlierThreshold)
                .OrderByDescending(kv => kv.Value)
                .Take(maxOutliersToRemove)
                .Select(kv => kv.Key)
                .ToList();

            var clean = data.Except(outliers).ToList();
            Console.WriteLine($"STOLP: удалено {outliers.Count} выбросов");
            return clean;
        }

        // Риск выброса: насколько объект "выпадает" из своего класса
        private double OutlierRisk((double[] Features, string Label) point, List<(double[] Features, string Label)> data)
        {
            var sameClass = data.Where(d => d.Label == point.Label && !d.Equals(point)).ToList();
            var otherClass = data.Where(d => d.Label != point.Label).ToList();

            if (sameClass.Count == 0) return double.MaxValue;

            double avgDistToSame = sameClass.Average(p => EuclideanDistance(point.Features, p.Features));
            double minDistToOther = otherClass.Min(p => EuclideanDistance(point.Features, p.Features));

            return avgDistToSame / (minDistToOther + 1e-9);
        }

        // Инициализация: по одному эталону на класс (с максимальным выступом)
        private List<(double[] Features, string Label)> InitializePrototypes(
            List<(double[] Features, string Label)> data)
        {
            var prototypes = new List<(double[] Features, string Label)>();
            var classes = data.Select(d => d.Label).Distinct();

            foreach (var cls in classes)
            {
                var classPoints = data.Where(d => d.Label == cls).ToList();
                var best = classPoints
                    .OrderByDescending(p => Protrusion(p, data))
                    .First();
                prototypes.Add(best);
            }

            Console.WriteLine($"STOLP: инициализировано {prototypes.Count} эталонов (по одному на класс)");
            return prototypes;
        }

        // Выступ (protrusion): насколько объект близок к чужому классу
        private double Protrusion((double[] Features, string Label) point, List<(double[] Features, string Label)> data)
        {
            var otherClass = data.Where(d => d.Label != point.Label).ToList();
            if (otherClass.Count == 0) return 0;
            return -otherClass.Min(p => EuclideanDistance(point.Features, p.Features)); // минус, чтобы максимизировать близость
        }

        // Риск объекта на основе текущих эталонов
        private double CalculateRisk((double[] Features, string Label) point, List<(double[] Features, string Label)> prototypes)
        {
            var distances = prototypes.Select(p => EuclideanDistance(point.Features, p.Features)).ToList();
            if (distances.Count == 0) return double.MaxValue;

            double nearestSame = prototypes
                .Where(p => p.Label == point.Label)
                .Select(p => EuclideanDistance(point.Features, p.Features))
                .DefaultIfEmpty(double.MaxValue)
                .Min();

            double nearestOther = prototypes
                .Where(p => p.Label != point.Label)
                .Select(p => EuclideanDistance(point.Features, p.Features))
                .DefaultIfEmpty(double.MaxValue)
                .Min();

            return nearestSame / (nearestOther + 1e-9);
        }

        // Евклидово расстояние
        private double EuclideanDistance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }
    }
}