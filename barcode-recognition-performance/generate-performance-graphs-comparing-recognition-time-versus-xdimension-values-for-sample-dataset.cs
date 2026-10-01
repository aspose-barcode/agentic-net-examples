// Title: Barcode Recognition Performance Graph by XDimension
// Description: Generates Code128 barcodes with different XDimension values, measures their recognition time, and creates a PNG performance graph.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and drawing APIs to evaluate barcode scanning performance. Developers often need to benchmark barcode parameters such as XDimension to optimize read speed for various scanners and applications.
// Prompt: Generate performance graphs comparing recognition time versus XDimension values for a sample dataset.
// Tags: barcode, code128, performance, graph, xdimension, generation, recognition, png, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes with varying XDimension values, measuring recognition time,
/// and creating a performance graph saved as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the performance measurement and graph generation.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample XDimension values (in points)
        float[] xDimensions = new float[] { 0.5f, 1f, 1.5f, 2f, 2.5f };
        // Store recognition times (in milliseconds)
        List<double> recognitionTimes = new List<double>();

        // Generate barcodes, save them, and measure recognition time
        foreach (float xDim in xDimensions)
        {
            string codeText = "Sample12345";
            string imagePath = Path.Combine(tempFolder, $"barcode_{xDim}.png");

            // Generate barcode with specific XDimension
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.XDimension.Point = xDim;
                // Save directly to file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Measure recognition time
            double elapsedMs;
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                Stopwatch sw = Stopwatch.StartNew();
                BarCodeResult[] results = reader.ReadBarCodes();
                sw.Stop();
                elapsedMs = sw.Elapsed.TotalMilliseconds;

                // Ensure at least one result was read (evaluation version adds watermark)
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected for XDimension {xDim}");
                }
            }

            recognitionTimes.Add(elapsedMs);
            Console.WriteLine($"XDimension {xDim} pt -> Recognition time: {elapsedMs:F2} ms");
        }

        // Create a performance graph
        int width = 800;
        int height = 600;
        using (var bitmap = new Bitmap(width, height))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Fill background
                graphics.Clear(Color.White);

                // Define margins
                int marginLeft = 80;
                int marginBottom = 80;
                int marginTop = 60;
                int marginRight = 40;

                // Draw axes
                Pen axisPen = new Pen(Color.Black, 2);
                graphics.DrawLine(axisPen, marginLeft, height - marginBottom, width - marginRight, height - marginBottom); // X axis
                graphics.DrawLine(axisPen, marginLeft, height - marginBottom, marginLeft, marginTop); // Y axis

                // Determine scaling factors
                float maxX = xDimensions[xDimensions.Length - 1];
                float maxY = (float)Math.Ceiling(recognitionTimes[recognitionTimes.Count - 1] / 10) * 10; // round up to nearest 10

                // Plot points and connecting lines
                Pen linePen = new Pen(Color.Blue, 2);
                Brush pointBrush = new SolidBrush(Color.Red);
                Font labelFont = new Font("Arial", 12);
                for (int i = 0; i < xDimensions.Length; i++)
                {
                    float xVal = xDimensions[i];
                    double yVal = recognitionTimes[i];

                    // Convert data values to pixel coordinates
                    float xPixel = marginLeft + (xVal / maxX) * (width - marginLeft - marginRight);
                    float yPixel = height - marginBottom - (float)(yVal / maxY) * (height - marginTop - marginBottom);

                    // Draw data point
                    float pointSize = 6f;
                    graphics.FillEllipse(pointBrush, xPixel - pointSize / 2, yPixel - pointSize / 2, pointSize, pointSize);

                    // Draw line to the next point, if any
                    if (i < xDimensions.Length - 1)
                    {
                        float nextXVal = xDimensions[i + 1];
                        double nextYVal = recognitionTimes[i + 1];
                        float nextXPixel = marginLeft + (nextXVal / maxX) * (width - marginLeft - marginRight);
                        float nextYPixel = height - marginBottom - (float)(nextYVal / maxY) * (height - marginTop - marginBottom);
                        graphics.DrawLine(linePen, xPixel, yPixel, nextXPixel, nextYPixel);
                    }

                    // X‑axis label for the current point
                    string xLabel = xVal.ToString("0.##");
                    SizeF xLabelSize = graphics.MeasureString(xLabel, labelFont);
                    graphics.DrawString(xLabel, labelFont, Brushes.Black, xPixel - xLabelSize.Width / 2, height - marginBottom + 5);
                }

                // Y‑axis labels (5 evenly spaced ticks)
                int yTicks = 5;
                for (int i = 0; i <= yTicks; i++)
                {
                    float yValue = i * maxY / yTicks;
                    float yPixel = height - marginBottom - (yValue / maxY) * (height - marginTop - marginBottom);

                    // Tick mark
                    graphics.DrawLine(Pens.Black, marginLeft - 5, yPixel, marginLeft, yPixel);

                    // Tick label
                    string yLabel = yValue.ToString("0");
                    SizeF yLabelSize = graphics.MeasureString(yLabel, labelFont);
                    graphics.DrawString(yLabel, labelFont, Brushes.Black, marginLeft - yLabelSize.Width - 8, yPixel - yLabelSize.Height / 2);
                }

                // Axis titles
                graphics.DrawString("XDimension (points)", labelFont, Brushes.Black,
                    marginLeft + (width - marginLeft - marginRight) / 2 - 60,
                    height - marginBottom + 40);

                graphics.TranslateTransform(20, marginTop + (height - marginTop - marginBottom) / 2 + 60);
                graphics.RotateTransform(-90);
                graphics.DrawString("Recognition Time (ms)", labelFont, Brushes.Black, 0, 0);
                graphics.ResetTransform();

                // Graph title
                graphics.DrawString("Barcode Recognition Time vs XDimension",
                    new Font("Arial", 16, FontStyle.Bold), Brushes.Black,
                    marginLeft + (width - marginLeft - marginRight) / 2 - 150, marginTop - 40);
            }

            // Save the graph image
            string graphPath = Path.Combine(tempFolder, "RecognitionPerformance.png");
            bitmap.Save(graphPath, Aspose.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine($"Performance graph saved to: {graphPath}");
        }

        // Cleanup: optionally delete temporary files (commented out to keep results)
        // Directory.Delete(tempFolder, true);
    }
}