// Title: Parallel barcode reading from multiple images
// Description: Demonstrates generating several barcode images, then using parallel processing to read them concurrently and aggregate the decoded values.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the BarCodeReader class with multi‑core processing. It illustrates typical use cases such as batch scanning of images, high‑throughput barcode extraction, and result aggregation using thread‑safe collections. Developers looking for parallel barcode recognition patterns can reference this snippet.
// Prompt: Use parallel processing to read barcodes from multiple images concurrently and aggregate results.
// Tags: barcode symbology, parallel processing, aggregation, barcodereader, aspose.barcode, code128, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates parallel barcode reading from multiple generated images and aggregates the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, reads them in parallel, and prints aggregated results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeParallel_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = $"CODE{i + 1}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            imageFiles.Add(filePath);
        }

        // Configure processor to use all CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Thread‑safe collection for aggregated results
        var aggregatedResults = new ConcurrentBag<string>();

        // Parallel reading of barcodes
        Parallel.ForEach(imageFiles, file =>
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // In evaluation mode, CodeText may contain a watermark;
                        // we consider any non‑empty result as a successful detection.
                        if (!string.IsNullOrEmpty(result.CodeText))
                        {
                            aggregatedResults.Add(result.CodeText);
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{file}': {ex.Message}");
            }
        });

        // Output aggregated results
        Console.WriteLine("Aggregated barcode results:");
        foreach (var txt in aggregatedResults)
        {
            Console.WriteLine(txt);
        }

        // Clean up temporary files
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