// Title: Profile MinimalXDimension Impact on Barcode Batch Processing Throughput
// Description: Demonstrates how varying the MinimalXDimension setting affects the time required to read a batch of Code128 barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch processing and performance profiling category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader with QualitySettings for decoding, and Stopwatch for measuring throughput. Developers often need to evaluate how quality parameters like MinimalXDimension influence processing speed in large‑scale scanning scenarios.
// Prompt: Profile the impact of increasing MinimalXDimension on overall batch processing throughput in tests.
// Tags: barcode, code128, performance, batch, minimalxdimension, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that profiles how different MinimalXDimension values affect
/// the throughput of reading a batch of Code128 barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them with
    /// varying MinimalXDimension settings, measures processing time, and reports throughput.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = "Sample" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Define MinimalXDimension values to test
        float[] minimalValues = new float[] { 1f, 2f, 3f, 4f, 5f };

        Console.WriteLine("Profiling MinimalXDimension impact on batch processing throughput:");
        foreach (float minimal in minimalValues)
        {
            // Start timing for the current MinimalXDimension setting
            Stopwatch sw = Stopwatch.StartNew();
            int totalRead = 0;

            // Process each generated barcode file
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                try
                {
                    using (var reader = new BarCodeReader(file, DecodeType.Code128))
                    {
                        // Apply quality settings: use MinimalXDimension mode and set the current value
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = minimal;

                        // Read all barcodes in the image
                        BarCodeResult[] results = reader.ReadBarCodes();
                        totalRead += results.Length;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
                }
            }

            // Stop timing and calculate throughput
            sw.Stop();
            double seconds = sw.Elapsed.TotalSeconds;
            double throughput = barcodeFiles.Count / seconds;
            Console.WriteLine($"MinimalXDimension={minimal} => Time={seconds:F3}s, Throughput={throughput:F2} images/sec, TotalRead={totalRead}");
        }

        // Clean up temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}