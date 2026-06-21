using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, embedding it in a large image,
/// and measuring recognition performance using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, draws it onto a large bitmap,
    /// then runs multiple recognition iterations while timing them.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Store the generated barcode image in a memory stream.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Load the barcode image from the stream into a bitmap.
                using (var barcodeBitmap = new Bitmap(barcodeStream))
                {
                    // Define size for a large blank canvas (4000x4000 pixels).
                    const int largeSize = 4000;

                    // Create the large bitmap that will hold the barcode.
                    using (var largeBitmap = new Bitmap(largeSize, largeSize))
                    {
                        // Prepare graphics object to draw on the large bitmap.
                        using (var graphics = Graphics.FromImage(largeBitmap))
                        {
                            // Fill the background with white color.
                            graphics.Clear(Color.White);

                            // Calculate coordinates to center the barcode.
                            int x = (largeSize - barcodeBitmap.Width) / 2;
                            int y = (largeSize - barcodeBitmap.Height) / 2;

                            // Draw the barcode onto the large bitmap at the calculated position.
                            graphics.DrawImage(barcodeBitmap, x, y, barcodeBitmap.Width, barcodeBitmap.Height);
                        }

                        // Number of recognition iterations for timing.
                        const int iterations = 5;
                        double totalMs = 0;

                        // Perform recognition repeatedly to gather timing data.
                        for (int i = 0; i < iterations; i++)
                        {
                            // Start a high‑resolution stopwatch.
                            var sw = Stopwatch.StartNew();

                            // Initialize the barcode reader with the large bitmap.
                            using (var reader = new BarCodeReader(largeBitmap))
                            {
                                // Use the HighPerformance preset to speed up recognition.
                                reader.QualitySettings = QualitySettings.HighPerformance;

                                // Read all barcodes in the image (results are printed, not used for timing).
                                foreach (var result in reader.ReadBarCodes())
                                {
                                    Console.WriteLine($"Iteration {i + 1}: Detected {result.CodeTypeName} - {result.CodeText}");
                                }
                            }

                            // Stop the stopwatch and accumulate elapsed time.
                            sw.Stop();
                            totalMs += sw.Elapsed.TotalMilliseconds;

                            // Output elapsed time for the current iteration.
                            Console.WriteLine($"Iteration {i + 1} elapsed: {sw.Elapsed.TotalMilliseconds:F2} ms");
                        }

                        // Compute and display the average recognition time.
                        double averageMs = totalMs / iterations;
                        Console.WriteLine($"Average recognition time (HighPerformance) on {largeSize}x{largeSize} image: {averageMs:F2} ms");
                    }
                }
            }
        }
    }
}