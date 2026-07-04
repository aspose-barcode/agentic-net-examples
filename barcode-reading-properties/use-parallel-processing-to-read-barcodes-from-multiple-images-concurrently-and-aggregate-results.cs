// Title: Parallel Barcode Reading and Aggregation
// Description: Demonstrates generating multiple barcode images, reading them concurrently using parallel processing, and aggregating the recognition results.
// Prompt: Use parallel processing to read barcodes from multiple images concurrently and aggregate results.
// Tags: barcode, parallel, aggregation, code128, aspose.barcode

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that creates several Code128 barcode images,
/// reads them in parallel, and aggregates the recognition results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, processes them concurrently,
    /// and displays aggregated results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary directory for sample barcode images.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodesSample");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // --------------------------------------------------------------------
        // Define sample texts to encode into barcodes.
        // --------------------------------------------------------------------
        string[] sampleTexts = new string[] { "ABC123", "XYZ789", "1234567890", "HELLO", "WORLD" };
        List<string> imagePaths = new List<string>();

        // --------------------------------------------------------------------
        // Generate barcode images (Code128) and collect their file paths.
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                generator.Save(filePath);
            }
            imagePaths.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Thread‑safe collection to aggregate recognition results.
        // --------------------------------------------------------------------
        var aggregatedResults = new ConcurrentBag<string>();

        // --------------------------------------------------------------------
        // Read barcodes from all images concurrently using Parallel.ForEach.
        // --------------------------------------------------------------------
        Parallel.ForEach(imagePaths, filePath =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Warning: File not found - {filePath}");
                return;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    string line = $"File: {Path.GetFileName(filePath)}, Type: {result.CodeTypeName}, Text: {result.CodeText}";
                    aggregatedResults.Add(line);
                }
            }
        });

        // --------------------------------------------------------------------
        // Output aggregated results to the console.
        // --------------------------------------------------------------------
        Console.WriteLine("Aggregated Barcode Recognition Results:");
        foreach (var line in aggregatedResults)
        {
            Console.WriteLine(line);
        }

        // --------------------------------------------------------------------
        // Clean up generated files (optional).
        // --------------------------------------------------------------------
        foreach (var path in imagePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignore any deletion errors.
            }
        }

        // --------------------------------------------------------------------
        // Remove temporary directory if it is empty.
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore if directory not empty or deletion fails.
        }
    }
}