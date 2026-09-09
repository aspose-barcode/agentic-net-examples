// Title: Parallel barcode reading from multiple images
// Description: Demonstrates generating several barcode images, then reading them concurrently using parallel processing, and aggregating the results.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category, showcasing how to use BarcodeGenerator, BarCodeReader, and related result classes. Typical use cases include batch processing of scanned documents, high‑throughput barcode scanning, and aggregating results for reporting. Developers often need to handle multiple images efficiently, leveraging .NET parallelism and thread‑safe collections.
// Prompt: Use parallel processing to read barcodes from multiple images concurrently and aggregate results.
// Tags: code128,qr,datamatrix,pdf417,aztec,barcode-generation,barcode-reading,parallel-processing,png,barcodegenerator,barcodereader,concurrentdictionary

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates parallel barcode reading from multiple generated images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, reads them in parallel, and prints aggregated results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeParallel_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type and text)
        var samples = new List<(BaseEncodeType encode, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417 Sample"),
            (EncodeTypes.Aztec, "AztecSample")
        };

        var filePaths = new List<string>();

        // Generate barcode images and collect their file paths
        foreach (var (encode, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encode.TypeName}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            filePaths.Add(filePath);
        }

        // Thread‑safe collection for aggregating read results per file
        var aggregated = new ConcurrentDictionary<string, List<BarCodeResult>>();

        // Parallel barcode reading across all generated images
        Parallel.ForEach(filePaths, filePath =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                // Initialize reader for all supported barcode types
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    var list = new List<BarCodeResult>(results);
                    // Store results in the concurrent dictionary
                    aggregated.TryAdd(filePath, list);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image {filePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        });

        // Output aggregated results to the console
        foreach (var kvp in aggregated)
        {
            Console.WriteLine($"File: {kvp.Key}");
            foreach (var result in kvp.Value)
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}, Quality: {result.ReadingQuality}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not crash the program
        }
    }
}