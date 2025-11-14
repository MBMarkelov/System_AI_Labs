using System;
using System.Linq;

namespace ML_Labs
{
    public class KernelDensity2D
    {
        private readonly List<double[]> points;
        private readonly double bandwidth;

        public KernelDensity2D(List<double[]> points, double bandwidth = 0.5)
        {
            this.points = points;
            this.bandwidth = bandwidth;
        }

        private double GaussianKernel(double dx, double dy)
        {
            double u = (dx * dx + dy * dy) / (2 * bandwidth * bandwidth);
            return Math.Exp(-u) / (2 * Math.PI * bandwidth * bandwidth);
        }

        public double DensityAt(double x, double y)
        {
            return points
                .Select(p => GaussianKernel(x - p[0], y - p[1]))
                .Sum() / points.Count;
        }

        public (double[,] Density, double MinX, double MaxX, double MinY, double MaxY) GetHeatmap(
            int width = 200, int height = 200,
            double padding = 0.1)
        {
            double minX = points.Min(p => p[0]);
            double maxX = points.Max(p => p[0]);
            double minY = points.Min(p => p[1]);
            double maxY = points.Max(p => p[1]);

            double padX = (maxX - minX) * padding;
            double padY = (maxY - minY) * padding;

            minX -= padX; maxX += padX;
            minY -= padY; maxY += padY;

            var density = new double[height, width];

            for (int iy = 0; iy < height; iy++)
            {
                double y = minY + (maxY - minY) * iy / (height - 1);
                for (int ix = 0; ix < width; ix++)
                {
                    double x = minX + (maxX - minX) * ix / (width - 1);
                    density[iy, ix] = DensityAt(x, y);
                }
            }

            return (density, minX, maxX, minY, maxY);
        }
    }
}