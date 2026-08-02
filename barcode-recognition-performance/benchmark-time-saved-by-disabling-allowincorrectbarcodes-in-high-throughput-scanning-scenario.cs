// Title: Benchmark effect of AllowIncorrectBarcodes on barcode scanning performance
// Description: Demonstrates how disabling AllowIncorrectBarcodes can reduce processing time when scanning many Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode scanning performance category, illustrating the use of BarCodeReader, QualitySettings, and DecodeType classes to measure recognition speed. Developers often need to optimize high‑throughput barcode scanning by toggling AllowIncorrectBarcodes, which controls validation of barcode integrity. The snippet shows typical setup, generation, and timing of barcode reads for benchmarking purposes.
// Prompt: Benchmark the time saved by disabling AllowIncorrectBarcodes in a high‑throughput scanning scenario.
// Tags: code128, scanning, performance, allowincorrectbarcodes, benchmark, aspose.barcode, generation, recognition

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a set of Code128 barcode images and benchmarks the impact of the
/// <c>AllowIncorrectBarcodes</c> setting on recognition speed using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample barcodes, runs two benchmarks
    /// (with and without <c>AllowIncorrectBarcodes</c>), and prints the timing results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a folder for sample barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Generate a small set of barcode images (5 items)
        // --------------------------------------------------------------------
        const int sampleCount = 5;
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"123456789{i}";
            string filePath = Path.Combine(folderPath, $"code{i}.png");

            // Create a Code128 barcode and save it as PNG
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Benchmark reading with AllowIncorrectBarcodes = false
        // --------------------------------------------------------------------
        TimeSpan timeWithoutAllowIncorrect = BenchmarkReading(folderPath, allowIncorrect: false);

        // --------------------------------------------------------------------
        // Benchmark reading with AllowIncorrectBarcodes = true
        // --------------------------------------------------------------------
        TimeSpan timeWithAllowIncorrect = BenchmarkReading(folderPath, allowIncorrect: true);

        // --------------------------------------------------------------------
        // Output the results
        // --------------------------------------------------------------------
        Console.WriteLine($"Reading time without AllowIncorrectBarcodes: {timeWithoutAllowIncorrect.TotalMilliseconds} ms");
        Console.WriteLine($"Reading time with AllowIncorrectBarcodes: {timeWithAllowIncorrect.TotalMilliseconds} ms");
        Console.WriteLine($"Time saved by disabling AllowIncorrectBarcodes: {(timeWithAllowIncorrect - timeWithoutAllowIncorrect).TotalMilliseconds} ms");
    }

    /// <summary>
    /// Reads all PNG files in the specified folder using the given <c>AllowIncorrectBarcodes</c> setting
    /// and returns the elapsed time.
    /// </summary>
    /// <param name="folderPath">Path to the folder containing barcode images.</param>
    /// <param name="allowIncorrect">Whether to allow incorrect barcodes during recognition.</param>
    /// <returns>Time taken to read all barcodes.</returns>
    static TimeSpan BenchmarkReading(string folderPath, bool allowIncorrect)
    {
        // Get all PNG files in the folder
        string[] files = Directory.GetFiles(folderPath, "*.png");
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Process each file with BarCodeReader
        foreach (string file in files)
        {
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Apply the requested AllowIncorrectBarcodes setting
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                // Perform recognition and access the result to ensure processing
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    string _ = result.CodeText;
                }
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}