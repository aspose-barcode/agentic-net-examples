using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how XDimension affects barcode recognition time and visualizes the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes with varying XDimension values, measures recognition time,
    /// and creates a performance graph saved as a PNG image.
    /// </summary>
    static void Main()
    {
        // Define a set of XDimension values (in points) to test.
        float[] xDimensions = new float[] { 0.5f, 1f, 1.5f, 2f, 2.5f };
        double[] recognitionTimes = new double[xDimensions.Length];

        // Loop through each XDimension, generate a barcode, recognize it, and record the time.
        for (int i = 0; i < xDimensions.Length; i++)
        {
            float xDim = xDimensions[i];

            // Create a barcode generator for Code128 with a sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure barcode appearance.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.XDimension.Point = xDim;
                generator.Parameters.Barcode.BarHeight.Point = 40f;
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode to a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Recognize the barcode and measure the elapsed time.
                    using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                    {
                        var sw = Stopwatch.StartNew();
                        var results = reader.ReadBarCodes();
                        sw.Stop();

                        recognitionTimes[i] = sw.Elapsed.TotalMilliseconds;
                        Console.WriteLine($"XDimension: {xDim} pt, Recognition Time: {recognitionTimes[i]:F2} ms, Detected: {results.Length}");
                    }
                }
            }
        }

        // Settings for the performance graph image.
        int width = 500;
        int height = 400;
        int marginLeft = 60;
        int marginBottom = 40;
        int marginTop = 20;
        int marginRight = 20;

        // Create a bitmap to draw the graph.
        using (var bitmap = new Bitmap(width, height))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Fill background.
                graphics.Clear(Color.White);

                // Prepare drawing tools.
                var axisPen = new Pen(Color.Black, 2f);
                var gridPen = new Pen(Color.LightGray, 1f);
                var pointBrush = new SolidBrush(Color.Blue);
                var linePen = new Pen(Color.Red, 2f);
                var font = new Font("Arial", 10f);

                // Draw X and Y axes.
                graphics.DrawLine(axisPen, marginLeft, marginTop, marginLeft, height - marginBottom);
                graphics.DrawLine(axisPen, marginLeft, height - marginBottom, width - marginRight, height - marginBottom);

                // Determine scaling factors for the plot.
                float maxX = xDimensions[xDimensions.Length - 1];
                float maxY = (float)recognitionTimes[0];
                for (int i = 1; i < recognitionTimes.Length; i++)
                {
                    if ((float)recognitionTimes[i] > maxY)
                        maxY = (float)recognitionTimes[i];
                }
                // Round up Y max to the nearest 10 for a cleaner grid.
                maxY = (float)Math.Ceiling(maxY / 10.0) * 10f;

                float plotWidth = width - marginLeft - marginRight;
                float plotHeight = height - marginTop - marginBottom;

                // Draw horizontal grid lines and Y-axis labels.
                for (int i = 0; i <= 5; i++)
                {
                    float y = marginTop + i * (plotHeight / 5f);
                    graphics.DrawLine(gridPen, marginLeft, y, width - marginRight, y);
                    float yValue = maxY - i * (maxY / 5f);
                    graphics.DrawString(yValue.ToString("0"), font, Brushes.Black, 5, y - 7);
                }

                // Draw vertical grid lines and X-axis labels.
                for (int i = 0; i <= xDimensions.Length - 1; i++)
                {
                    float x = marginLeft + i * (plotWidth / (xDimensions.Length - 1));
                    graphics.DrawLine(gridPen, x, marginTop, x, height - marginBottom);
                    graphics.DrawString(xDimensions[i].ToString("0.0"), font, Brushes.Black, x - 10, height - marginBottom + 5);
                }

                // Plot data points and connect them with lines.
                PointF? prevPoint = null;
                for (int i = 0; i < xDimensions.Length; i++)
                {
                    float xPos = marginLeft + (xDimensions[i] / maxX) * plotWidth;
                    float yPos = marginTop + ((maxY - (float)recognitionTimes[i]) / maxY) * plotHeight;
                    var pt = new PointF(xPos, yPos);

                    // Draw the data point.
                    graphics.FillEllipse(pointBrush, xPos - 3f, yPos - 3f, 6f, 6f);

                    // Draw a line from the previous point to the current one.
                    if (prevPoint.HasValue)
                    {
                        graphics.DrawLine(linePen, prevPoint.Value, pt);
                    }
                    prevPoint = pt;
                }

                // Add axis titles.
                graphics.DrawString("XDimension (pt)", font, Brushes.Black, width / 2f - 40, height - 15);
                graphics.DrawString("Recognition Time (ms)", font, Brushes.Black, 5, marginTop - 10);
            }

            // Save the completed chart to a PNG file.
            bitmap.Save("performance.png", ImageFormat.Png);
        }

        Console.WriteLine("Performance graph saved as 'performance.png'.");
    }
}