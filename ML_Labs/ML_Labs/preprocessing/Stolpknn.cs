using ML_Labs.preprocessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_Labs
{
    public class StolpKnn<T> where T : IKnnClassifier, new()
    {
        private readonly T knn;
        private readonly StolpSelector stolp;
        private List<(double[] Features, string Label)> prototypes = new();

        public StolpKnn(
            int prototypesPerClass = 5,
            double outlierThreshold = 1.8,
            int maxOutliersToRemove = 15)
        {
            knn = new T();
            stolp = new StolpSelector(prototypesPerClass, outlierThreshold, maxOutliersToRemove);
        }

        /// <summary>
        /// Обучает с автоматическим отбором эталонов через STOLP
        /// </summary>
        public void Train(List<(double[] Features, string Label)> data)
        {
            if (data.Count == 0) throw new ArgumentException("Данные пусты");

            // 1. STOLP отбирает эталоны
            prototypes = stolp.SelectPrototypes(data);

            // 2. Обучаем KNN только на эталонах
            foreach (var (f, l) in prototypes)
                knn.Train(f, l);

            Console.WriteLine($"StolpKnn<{typeof(T).Name}>: обучено на {prototypes.Count} эталонах");
        }

        public string Classify(double[] features)
        {
            if (prototypes.Count == 0)
                throw new InvalidOperationException("Модель не обучена");
            return knn.Classify(features);
        }

        public double Evaluate(List<(double[] Features, string Label)> testData)
        {
            if (prototypes.Count == 0)
                throw new InvalidOperationException("Модель не обучена");
            return knn.Evaluate(testData);
        }

        /// <summary>
        /// Возвращает эталоны (для визуализации)
        /// </summary>
        public List<(double[] Features, string Label)> GetPrototypes() => prototypes;
    }
}