// Title: Barcode processing timing measurement example
// Description: Demonstrates how to generate a barcode, load it, and measure the time taken for loading, preprocessing, detection, and decoding using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for reading them. Developers often need to profile performance of each stage—loading, preprocessing, detection, and decoding—to optimize applications that process large volumes of barcodes. The example provides a template for timing these stages in .NET projects.
/// Prompt: Log detailed timing for each processing stage—loading, preprocessing, detection, and decoding—to identify bottlenecks.
/// Tags: barcode, code128, timing, performance, generation, recognition, aspose.barcode, qualitysettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reading, and detailed timing of each processing stage using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, measures loading, preprocessing, detection, and decoding times, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcode
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTiming_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode image to be read later
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Prepare a stopwatch for timing
        var stopwatch = new Stopwatch();

        // ---------- Loading ----------
        stopwatch.Start();

        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        using (FileStream fileStream = new FileStream(barcodePath, FileMode.Open, FileAccess.Read))
        {
            stopwatch.Stop();
            long loadingTimeMs = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Loading time: {loadingTimeMs} ms");

            // ---------- Preprocessing ----------
            stopwatch.Restart();

            // Create the reader with all supported decode types
            using (var reader = new BarCodeReader(fileStream, DecodeType.AllSupportedTypes))
            {
                // Example preprocessing: set a quality preset for high performance
                reader.QualitySettings = QualitySettings.HighPerformance;

                stopwatch.Stop();
                long preprocessingTimeMs = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"Preprocessing time: {preprocessingTimeMs} ms");

                // ---------- Detection ----------
                stopwatch.Restart();

                BarCodeResult[] results = reader.ReadBarCodes();

                stopwatch.Stop();
                long detectionTimeMs = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"Detection time: {detectionTimeMs} ms");

                // ---------- Decoding ----------
                stopwatch.Restart();

                foreach (var result in results)
                {
                    // Accessing CodeText performs the decoding step
                    string codeText = result.CodeText;
                    Console.WriteLine($"Detected [{result.CodeTypeName}]: {codeText}");
                }

                stopwatch.Stop();
                long decodingTimeMs = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"Decoding time: {decodingTimeMs} ms");
            }
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect the demo
        }
    }
}