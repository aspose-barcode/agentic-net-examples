using System;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Sample XDimension values (in points)
        float[] xDimensions = { 1f, 2f, 3f, 4f, 5f };
        // Store recognition times (in milliseconds)
        List<double> recognitionTimes = new List<double>();

        // Loop through each XDimension, generate barcode, recognize and measure time
        foreach (float xDim in xDimensions)
        {
            // Create barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Ensure size is controlled by XDimension
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Set XDimension (smallest bar width) in points
                generator.Parameters.Barcode.XDimension.Point = xDim;
                // Set a reasonable bar height
                generator.Parameters.Barcode.BarHeight.Point = 50f;

                // Generate barcode image in memory
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Measure recognition time
                    var stopwatch = Stopwatch.StartNew();
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                    {
                        // Use high performance preset for consistency
                        reader.QualitySettings = QualitySettings.HighPerformance;
                        // Perform recognition (ignore results)
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // No action needed; just force reading
                        }
                    }
                    stopwatch.Stop();
                    recognitionTimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                }
            }
        }

        // Create a simple performance chart
        const int chartWidth = 500;
        const int chartHeight = 400;
        const int margin = 50;

        using (var chartBitmap = new Bitmap(chartWidth, chartHeight))
        {
            using (var graphics = Graphics.FromImage(chartBitmap))
            {
                // White background
                graphics.Clear(Color.White);

                // Axes
                Pen axisPen = new Pen(Color.Black, 2);
                graphics.DrawLine(axisPen, margin, chartHeight - margin, chartWidth - margin, chartHeight - margin); // X axis
                graphics.DrawLine(axisPen, margin, margin, margin, chartHeight - margin); // Y axis

                // Determine scaling factors
                float maxX = xDimensions[xDimensions.Length - 1];
                double maxY = 0;
                foreach (double t in recognitionTimes)
                    if (t > maxY) maxY = t;
                maxY = Math.Max(maxY, 1); // avoid division by zero

                float xScale = (chartWidth - 2 * margin) / maxX;
                float yScale = (chartHeight - 2 * margin) / (float)maxY;

                // Plot points and lines
                Pen linePen = new Pen(Color.Blue, 2);
                Brush pointBrush = new SolidBrush(Color.Red);
                for (int i = 0; i < xDimensions.Length; i++)
                {
                    float x = margin + xDimensions[i] * xScale;
                    float y = chartHeight - margin - (float)(recognitionTimes[i] * yScale);
                    // Draw point
                    graphics.FillEllipse(pointBrush, x - 3, y - 3, 6, 6);
                    // Draw line to next point
                    if (i < xDimensions.Length - 1)
                    {
                        float nextX = margin + xDimensions[i + 1] * xScale;
                        float nextY = chartHeight - margin - (float)(recognitionTimes[i + 1] * yScale);
                        graphics.DrawLine(linePen, x, y, nextX, nextY);
                    }
                }

                // Labels
                Font labelFont = new Font("Arial", 10);
                Brush labelBrush = new SolidBrush(Color.Black);
                // X axis label
                graphics.DrawString("XDimension (pt)", labelFont, labelBrush, chartWidth / 2f - 40, chartHeight - margin + 20);
                // Y axis label
                graphics.DrawString("Recognition Time (ms)", labelFont, labelBrush, margin - 45, margin - 30);
                // Title
                graphics.DrawString("Recognition Time vs XDimension", new Font("Arial", 12, FontStyle.Bold), labelBrush, margin, margin - 30);
            }

            // Save chart image
            chartBitmap.Save("performance.png", ImageFormat.Png);
        }

        // Output results to console
        Console.WriteLine("XDimension (pt) | Recognition Time (ms)");
        for (int i = 0; i < xDimensions.Length; i++)
        {
            Console.WriteLine($"{xDimensions[i],15} | {recognitionTimes[i],22:F2}");
        }
        Console.WriteLine("Performance chart saved as 'performance.png'.");
    }
}