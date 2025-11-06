using ML_Labs;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void knn_button_Click(object sender, EventArgs e)
        {
            var knn = new KnnClassifier(k: 3);
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
            Console.WriteLine($"Точность: {accuracy:P2}");

            // === ВИЗУАЛИЗАЦИЯ ОЦЕНКИ ===
            KnnDataView.PlotEvaluation(trainingData, testData, predictions, accuracy);

            // Пример одного студента
            double[] student = [4.2, 8];
            string predicted = knn.Classify(student);
            Console.WriteLine($"Студент [{student[0]}, {student[1]}] → {predicted}");
        }
    }
}
