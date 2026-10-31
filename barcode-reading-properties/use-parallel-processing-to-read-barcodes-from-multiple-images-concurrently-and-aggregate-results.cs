// Title: Parallel barcode reading from multiple images
// Description: Demonstrates generating several barcode images, then using Aspose.BarCode's multithreaded recognition to read them concurrently and collect the results.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure the BarCodeReader processor for parallel execution, generate sample barcodes, and aggregate detection results. It highlights key API classes such as BarcodeGenerator, BarCodeReader, ProcessorSettings, and uses .NET Parallel.ForEach for high‑throughput scenarios common in batch processing or automated scanning pipelines.
// Prompt: Use parallel processing to read barcodes from multiple images concurrently and aggregate results.
// Tags: barcode, parallel processing, multithreaded recognition, aspose.barcode, generation, recognition, concurrentbag, .net

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates parallel barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcode images, reads them in parallel, and outputs aggregated results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, file name)
        var samples = new List<(BaseEncodeType encode, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample", "pdf417.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        var filePaths = new List<string>();

        // Generate barcode images and collect their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            filePaths.Add(filePath);
        }

        // Enable multithreaded recognition settings
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        var resultsBag = new ConcurrentBag<string>();
        var stopwatch = Stopwatch.StartNew();

        // Read barcodes in parallel using Parallel.ForEach
        Parallel.ForEach(filePaths, filePath =>
        {
            if (!File.Exists(filePath))
            {
                resultsBag.Add($"File not found: {filePath}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        resultsBag.Add($"{Path.GetFileName(filePath)}: No barcode detected.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            resultsBag.Add($"{Path.GetFileName(filePath)}: {result.CodeTypeName} => {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                resultsBag.Add($"{Path.GetFileName(filePath)}: Image loading failed ({ex.Message})");
            }
        });

        stopwatch.Stop();

        // Output processing time and aggregated results
        Console.WriteLine($"Processed {filePaths.Count} images in {stopwatch.ElapsedMilliseconds} ms.");
        foreach (var line in resultsBag)
        {
            Console.WriteLine(line);
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }
}