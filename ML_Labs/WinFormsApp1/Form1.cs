using KR1_Malckov;
using ML_Labs;
using ScottPlot;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private List<(double[] Features, string Label)> trainingData;
        private List<(double[] Features, string Label)> testData;
        private string DataMode;
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
            if (DataMode == "Рандом")
            {
                trainingData = StudentDataGenerator.Generate(200);
                testData = StudentDataGenerator.Generate(50);
            }
            var knn = new KnnClassifier(k: 3);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);
            var predictions = testData.Select(t => knn.Classify(t.Features)).ToList();
            double accuracy = knn.Evaluate(testData);
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);
        }

        private void knn_weight_button_Click(object sender, EventArgs e)
        {
            if (DataMode == "Рандом")
            {
                trainingData = StudentDataGenerator.Generate(200);
                testData = StudentDataGenerator.Generate(50);
            }
            var knn = new KnnWeightClassifier(k: 3);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);

            var predictions = testData.Select(t => knn.Classify(t.Features)).ToList();

            double accuracy = knn.Evaluate(testData);
            Console.WriteLine($"Точность: {accuracy:P2}");

            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);
        }

        private void knn_Core_button_Click(object sender, EventArgs e)
        {
            if (DataMode == "Рандом")
            {
                trainingData = StudentDataGenerator.Generate(200);
                testData = StudentDataGenerator.Generate(50);
            }
            var knn = new KernelKnnClassifier(k: 3);
            foreach (var (features, label) in trainingData)
                knn.Train(features, label);

            var predictions = testData.Select(t => knn.Classify(t.Features)).ToList();

            double accuracy = knn.Evaluate(testData);
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);
        }

        private void Stolp_Knn_button_Click(object sender, EventArgs e)
        {
            if (DataMode == "Рандом")
            {
                trainingData = StudentDataGenerator.Generate(200);
                testData = StudentDataGenerator.Generate(50);
            }

            var knnStolp = new StolpKnn<KnnClassifier>(prototypesPerClass: 4);
            knnStolp.Train(trainingData);
            double accuracy = knnStolp.Evaluate(testData);
            var predictions = testData.Select(t => knnStolp.Classify(t.Features)).ToList();
            KnnDataView.PlotStolpResult(trainingData, knnStolp.GetPrototypes());
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);
        }

        private void Stolp_Knn_Core_Button_Click(object sender, EventArgs e)
        {
            if (DataMode == "Рандом")
            {
                trainingData = StudentDataGenerator.Generate(200);
                testData = StudentDataGenerator.Generate(50);
            }
            var KernelknnStolp = new StolpKnn<KernelKnnClassifier>(prototypesPerClass: 4);
            KernelknnStolp.Train(trainingData);
            double accuracy = KernelknnStolp.Evaluate(testData);
            var predictions = testData.Select(t => KernelknnStolp.Classify(t.Features)).ToList();
            KnnDataView.PlotStolpResult(trainingData, KernelknnStolp.GetPrototypes());
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);

        }

        private void Stolp_Knn_Weight_button_Click(object sender, EventArgs e)
        {
            if (DataMode == "Рандом")
            {
                trainingData = StudentDataGenerator.Generate(200);
                testData = StudentDataGenerator.Generate(50);
            }

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
                DataMode = DataSelector.SelectedItem.ToString();

                switch (DataMode)
                {
                    case "Рандом":
                        break;
                    case "Рандом фиксированный":
                        trainingData = StudentDataGenerator.Generate(200);
                        testData = StudentDataGenerator.Generate(50);
                        break;
                    case "Пресет_1":
                        trainingData = Preset_1.TrainingData;
                        testData = Preset_1.TestData;
                        break;
                    case "Пресет_2":
                        trainingData = Preset_2.TrainingData;
                        testData = Preset_2.TestData;
                        break;
                    case "Другое...":
                        break;

                }
            }
        }

        private void density_button_Click(object sender, EventArgs e)
        {
            if (DataMode == "Рандом")
            {
                trainingData = StudentDataGenerator.Generate(200);
                testData = StudentDataGenerator.Generate(50);
            }

            var stolpKnn = new StolpKnn<KernelKnnClassifier>(prototypesPerClass: 5);
            stolpKnn.Train(trainingData);

            var prototypes = stolpKnn.GetPrototypes();
            var predictions = testData.Select(t => stolpKnn.Classify(t.Features)).ToList();

            KnnDataView.PlotMap(trainingData, testData, predictions, prototypes);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string csvPath = "C:/Users/MB_Markelov_Nout/source/repos/KR1_Malckov/data.csv";
            if (!File.Exists(csvPath))
            {
                MessageBox.Show("Файл data.csv не найден! Поместите его в папку с exe.");
                return;
            }

            // 1. Загрузка
            var (allData, featureNames) = OverdoseDataLoader.Load(csvPath);

            // 2. Разделение
            var evaluator = new OverdoseKnnEvaluator();
            var (train, test) = evaluator.SplitData(allData);

            // 3. KNN
            var (predictions, accuracy) = evaluator.RunKnn(train, test, k: 5);

            // 4. График
            KnnDataView.PlotOverdoseEvaluation(train, test, predictions, accuracy, featureNames);
        }
    }
}
