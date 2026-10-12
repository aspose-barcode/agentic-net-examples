// Title: Batch Process BMP Barcodes with Normal Quality and Measure Execution Time
// Description: Demonstrates generating sample BMP barcode images, reading them using the NormalQuality preset, and recording the total processing duration.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create barcode images, configure quality settings for reading, and measure performance. It uses BarcodeGenerator, BarCodeReader, and QualitySettings classes, which are commonly employed for batch processing and high‑throughput scanning scenarios. Developers often need such patterns to evaluate throughput, optimize settings, and automate barcode handling in file‑system workflows.
// Prompt: Process a directory of BMP files using NormalQuality preset and record total processing time.
// Tags: barcode, bmp, batch processing, normalquality, performance, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the BMP barcode batch processing example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample BMP barcodes, reads them with NormalQuality settings, and reports total processing time.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample BMP files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BmpBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample BMP barcode images
        int sampleCount = 5;
        for (int i = 1; i <= sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"sample{i}.bmp");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Bmp);
            }
        }

        // Verify the directory exists and retrieve BMP files
        if (!Directory.Exists(tempFolder))
        {
            Console.WriteLine("Input directory does not exist.");
            return;
        }

        string[] bmpFiles = Directory.GetFiles(tempFolder, "*.bmp", SearchOption.TopDirectoryOnly);
        if (bmpFiles.Length == 0)
        {
            Console.WriteLine("No BMP files found to process.");
            return;
        }

        // Start timing the batch processing
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Process each BMP file using NormalQuality preset
        foreach (string file in bmpFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Apply NormalQuality settings for reading
                    reader.QualitySettings = QualitySettings.NormalQuality;
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output results for the current file
                    Console.WriteLine($"File: {Path.GetFileName(file)} - Barcodes read: {results.Length}");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{file}': {ex.Message}");
            }
        }

        // Stop timing and display total elapsed time
        stopwatch.Stop();
        Console.WriteLine($"Total processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");

        // Cleanup temporary files and folder
        try
        {
            foreach (string file in bmpFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}