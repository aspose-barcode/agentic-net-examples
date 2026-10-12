// Title: Batch barcode reading performance profiling with varying MinimalXDimension
// Description: Demonstrates how to generate a set of Code128 barcode images, then measures read throughput while adjusting the MinimalXDimension setting.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning collection, illustrating the use of BarCodeReader's QualitySettings (XDimensionMode and MinimalXDimension) to optimize batch processing. It shows typical scenarios where developers need to balance read speed and accuracy for large image sets, using classes like BarcodeGenerator, BarCodeReader, and related enums. Ideal for performance testing and CI pipelines.
// Prompt: Profile the impact of increasing MinimalXDimension on overall batch processing throughput in tests.
// Tags: code128, barcode reading, performance profiling, png, barcodereader, barcodegenerator, xdimensionmode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and reading of Code128 barcodes while profiling the effect of MinimalXDimension on throughput.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them with varying MinimalXDimension values, and reports processing time and throughput.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images (Code128, PNG format)
        List<string> files = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = "Test" + i;
            string filePath = Path.Combine(batchFolder, $"code{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // Define MinimalXDimension values to test (in points)
        float[] minimalValues = new float[] { 1f, 2f, 3f, 4f, 5f };

        // Iterate over each MinimalXDimension setting and measure read performance
        foreach (float minimal in minimalValues)
        {
            Stopwatch sw = Stopwatch.StartNew(); // Start timing for this setting
            int totalRead = 0; // Counter for total barcodes successfully read

            // Read each generated image with the current MinimalXDimension
            foreach (string file in files)
            {
                if (!File.Exists(file))
                    continue; // Skip missing files (should not happen)

                using (var reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Configure quality settings to use MinimalXDimension
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = minimal;

                    try
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        totalRead += results.Length;
                    }
                    catch (ArgumentException ex)
                    {
                        // Log any read errors without stopping the batch
                        Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
                    }
                }
            }

            sw.Stop(); // Stop timing for this setting
            double seconds = sw.Elapsed.TotalSeconds;
            double throughput = files.Count / seconds; // Images processed per second

            // Output performance metrics for the current MinimalXDimension
            Console.WriteLine($"MinimalXDimension={minimal} => Time={seconds:F3}s, Throughput={throughput:F2} images/sec, TotalRead={totalRead}");
        }

        // Cleanup temporary folder and generated files
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use)
        }
    }
}