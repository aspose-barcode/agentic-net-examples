// Title: High-Performance barcode reading from PNG images
// Description: Demonstrates how to set QualitySettings.Preset to HighPerformance when reading a batch of PNG barcode images, improving processing speed.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use BarCodeReader with QualitySettings to optimize performance for bulk image processing. Developers commonly need to read many barcodes quickly, and this snippet illustrates configuring the HighPerformance preset, generating sample barcodes, and cleaning up resources.
// Prompt: Set QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images.
// Tags: barcode symbology, performance, png, reading, qualitysettings, aspose.barcode, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting QualitySettings to HighPerformance before reading a batch of PNG barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample PNG barcodes, reads them with high‑performance settings, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample PNG barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"Sample{i:D3}";
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save each barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Read the generated barcodes with HighPerformance quality preset
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply the high‑performance quality setting before reading
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Perform barcode detection
                    BarCodeResult[] results = reader.ReadBarCodes();
                    Console.WriteLine($"File: {Path.GetFileName(file)} - Barcodes found: {results.Length}");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
            catch (Exception ex)
            {
                // Log any other processing errors
                Console.WriteLine($"Error processing file {file}: {ex.Message}");
            }
        }

        // Clean up temporary folder
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}