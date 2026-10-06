// Title: Read Batch of PNG Barcodes with HighPerformance Quality Settings
// Description: Demonstrates generating a set of PNG barcode images, then reading them using Aspose.BarCode with the HighPerformance quality preset.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to configure QualitySettings for fast processing when reading multiple images. The key API classes are BarCodeGenerator, BarCodeReader, QualitySettings, and related enums. Typical use cases include bulk scanning of barcodes in high‑throughput scenarios where performance outweighs accuracy.
// Prompt: Set QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images.
// Tags: barcode, code128, png, batch processing, highperformance, qualitysettings, generation, recognition, aspose

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading a batch of PNG barcode images using Aspose.BarCode with high‑performance settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them with HighPerformance preset, and cleans up temporary files.
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

            // Use BarcodeGenerator to create a Code128 barcode and save as PNG
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // Read the batch of PNG barcode images with HighPerformance preset
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize BarCodeReader for all supported types
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Set recognition quality preset to HighPerformance
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
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{file}': {ex.Message}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }

            if (Directory.Exists(batchFolder))
                Directory.Delete(batchFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}