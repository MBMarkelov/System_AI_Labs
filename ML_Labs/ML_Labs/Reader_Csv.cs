using System.Globalization;

public static class OverdoseDataLoader
{
    public static (List<(double[] Features, string Label)> Data, string[] FeatureNames) Load(string path)
    {
        var data = new List<(double[] Features, string Label)>();
        var lines = File.ReadAllLines(path).Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length < 15) continue;

            if (!int.TryParse(parts[2], out int panelNum)) continue;
            if (!int.TryParse(parts[4], out int unitNum)) continue;
            if (!int.TryParse(parts[6], out int stubNameNum)) continue;
            if (!double.TryParse(parts[8], NumberStyles.Any, CultureInfo.InvariantCulture, out double stubLabelNum)) continue;
            if (!int.TryParse(parts[9], out int year)) continue;
            if (!double.TryParse(parts[12], NumberStyles.Any, CultureInfo.InvariantCulture, out double ageNum)) continue;

            double estimate = 0;
            if (!string.IsNullOrWhiteSpace(parts[13]) && parts[13] != "*" && parts[13] != "")
                double.TryParse(parts[13], NumberStyles.Any, CultureInfo.InvariantCulture, out estimate);

            var features = new double[]
            {
                    panelNum,
                    unitNum,
                    stubNameNum,
                    stubLabelNum,
                    ageNum,
                    estimate
            };

            string label = year <= 2010 ? "0" : "1";

            data.Add((features, label));
        }

        string[] featureNames = { "PANEL_NUM", "UNIT_NUM", "STUB_NAME_NUM", "STUB_LABEL_NUM", "AGE_NUM", "ESTIMATE" };
        return (data, featureNames);
    }
}