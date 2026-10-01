// Title: QR Code Detection Speed Comparison with UseMinimalXDimension
// Description: Demonstrates measuring QR code recognition time with and without the UseMinimalXDimension setting.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to create a QR code image using BarcodeGenerator, then evaluate detection performance using BarCodeReader with different XDimension quality settings. Developers often need to benchmark recognition speed for various configurations to optimize scanning applications.
// Prompt: Evaluate QR code detection speed when UseMinimalXDimension is disabled versus enabled.
// Tags: qr code, detection speed, useminimalxdimension, barcode generation, barcode recognition, aspose.barcode, png, performance

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code and measures detection time
/// with default XDimension settings versus the UseMinimalXDimension mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, then measures and prints average
    /// detection times for two XDimension configurations.
    /// </summary>
    static void Main()
    {
        // QR code content to encode
        string codeText = "https://example.com";

        // Create a QR code generator and write the image to a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the stream to a byte array for repeated reads
                byte[] imageData = ms.ToArray();

                // Measure detection time with default XDimension settings
                double defaultAvgMs = MeasureReadTime(imageData, useMinimalXDimension: false);

                // Measure detection time with UseMinimalXDimension enabled
                double minimalAvgMs = MeasureReadTime(imageData, useMinimalXDimension: true);

                // Output the average times
                Console.WriteLine($"Default XDimension detection time (average over 5 runs): {defaultAvgMs:F3} ms");
                Console.WriteLine($"UseMinimalXDimension detection time (average over 5 runs): {minimalAvgMs:F3} ms");
            }
        }
    }

    // Measures average detection time over a small number of iterations
    private static double MeasureReadTime(byte[] imageData, bool useMinimalXDimension)
    {
        const int iterations = 5;
        long totalTicks = 0;

        for (int i = 0; i < iterations; i++)
        {
            // Load the image data into a new memory stream for each iteration
            using (var ms = new MemoryStream(imageData))
            {
                // Initialize the QR code reader
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    if (useMinimalXDimension)
                    {
                        // Enable UseMinimalXDimension mode for recognition
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    }

                    // Start timing the read operation
                    Stopwatch sw = Stopwatch.StartNew();
                    BarCodeResult[] results = reader.ReadBarCodes();
                    sw.Stop();

                    totalTicks += sw.ElapsedTicks;

                    // Access results to prevent compiler optimizations from removing the read
                    foreach (var result in results)
                    {
                        string txt = result.CodeText;
                    }
                }
            }
        }

        // Calculate average time in milliseconds
        double avgMs = (totalTicks * 1000.0) / Stopwatch.Frequency / iterations;
        return avgMs;
    }
}