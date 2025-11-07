using ML_Labs;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataSelector.SelectedIndexChanged += DataSelector_SelectedIndexChanged;
            DataSelector.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void knn_button_Click(object sender, EventArgs e)
        {
            var knn = new KnnClassifier(k: 3);
            var trainingData = StudentDataGenerator.Generate(100);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);

            var testData = StudentDataGenerator.Generate(20);
            var predictions = testData.Select(t => knn.Classify(t.Features)).ToList();

            double accuracy = knn.Evaluate(testData);
            Console.WriteLine($"Точность: {accuracy:P2}");

            // === ВИЗУАЛИЗАЦИЯ ОЦЕНКИ ===
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);

            // Пример одного студента
            double[] student = [4.2, 8];
            string predicted = knn.Classify(student);
            Console.WriteLine($"Студент [{student[0]}, {student[1]}] → {predicted}");
        }

        private void knn_weight_button_Click(object sender, EventArgs e)
        {
            var knn = new KnnWeightClassifier(k: 3);
            var trainingData = StudentDataGenerator.Generate(200);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);

            var testData = StudentDataGenerator.Generate(30);
            var predictions = testData.Select(t => knn.Classify(t.Features)).ToList();

            double accuracy = knn.Evaluate(testData);
            Console.WriteLine($"Точность: {accuracy:P2}");

            // === ВИЗУАЛИЗАЦИЯ ОЦЕНКИ ===
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);

            // Пример одного студента
            double[] student = [4.2, 8];
            string predicted = knn.Classify(student);
            Console.WriteLine($"Студент [{student[0]}, {student[1]}] → {predicted}");
        }

        private void knn_Core_button_Click(object sender, EventArgs e)
        {
            var knn = new KernelKnnClassifier(k: 3);
            var trainingData = StudentDataGenerator.Generate(200);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);

            var testData = StudentDataGenerator.Generate(30);
            var predictions = testData.Select(t => knn.Classify(t.Features)).ToList();

            double accuracy = knn.Evaluate(testData);
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);
        }

        private void Stolp_Knn_button_Click(object sender, EventArgs e)
        {
            var trainingData = StudentDataGenerator.Generate(200);
            var testData = StudentDataGenerator.Generate(50);
            var knnStolp = new StolpKnn<KnnClassifier>(prototypesPerClass: 4);
            knnStolp.Train(trainingData);
            double accuracy = knnStolp.Evaluate(testData);
            var predictions = testData.Select(t => knnStolp.Classify(t.Features)).ToList();
            KnnDataView.PlotStolpResult(trainingData, knnStolp.GetPrototypes());
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);
        }

        private void Stolp_Knn_Core_Button_Click(object sender, EventArgs e)
        {
            var trainingData = StudentDataGenerator.Generate(200);
            var testData = StudentDataGenerator.Generate(50);
            var KernelknnStolp = new StolpKnn<KernelKnnClassifier>(prototypesPerClass: 4);
            KernelknnStolp.Train(trainingData);
            double accuracy = KernelknnStolp.Evaluate(testData);
            var predictions = testData.Select(t => KernelknnStolp.Classify(t.Features)).ToList();
            KnnDataView.PlotStolpResult(trainingData, KernelknnStolp.GetPrototypes());
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);

        }

        private void Stolp_Knn_Weight_button_Click(object sender, EventArgs e)
        {
            var trainingData = StudentDataGenerator.Generate(200);
            var testData = StudentDataGenerator.Generate(50);
            var knnWeightStolp = new StolpKnn<KnnWeightClassifier>(prototypesPerClass: 4);
            knnWeightStolp.Train(trainingData);
            double accuracy = knnWeightStolp.Evaluate(testData);
            var predictions = testData.Select(t => knnWeightStolp.Classify(t.Features)).ToList();
            KnnDataView.PlotStolpResult(trainingData, knnWeightStolp.GetPrototypes());
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);
        }

        private void DataSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataSelector.SelectedItem != null)
            {
                string selectedValue = DataSelector.SelectedItem.ToString();

                switch (selectedValue)
                {
                    case "Рандом":
                        // Код для рандом
                        break;
                    case "Рандом фиксированный":
                        // Код для фиксированного рандома
                        MessageBox.Show("Выбран режим: Рандом фиксированный");
                        break;
                    case "Пресет_1":
                        // Код для пресета 1
                        MessageBox.Show("Выбран режим: Пресет_1");
                        break;
                    case "Пресет_2":
                        // Код для пресета 2
                        MessageBox.Show("Выбран режим: Пресет_2");
                        break;
                    case "Другое...":
                        // Код для открытия дополнительных опций
                        MessageBox.Show("Открыть дополнительные настройки...");
                        break;

                }
            }
        }
    }
}
