// Title: Batch PNG Barcode Generation and High-Performance Recognition
// Description: Demonstrates generating multiple PNG barcode images and reading them using the HighPerformance quality preset for faster processing.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and QualitySettings to control recognition performance. Typical scenarios include bulk barcode processing, high‑throughput scanning, and performance‑critical applications where developers need to balance speed and accuracy.
// Prompt: Set QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images.
// Tags: barcode generation, barcode recognition, png, highperformance, qualitysettings, aspose.barcode, code128

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a temporary batch of PNG Code128 barcodes and reading them
/// with the HighPerformance quality preset using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them with high‑performance
    /// settings, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the batch of barcode images.
        // --------------------------------------------------------------------
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // --------------------------------------------------------------------
        // Generate sample PNG barcode images (Code128) and store their file paths.
        // --------------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            string codeText = $"Sample{i:D3}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Read the generated barcode images using the HighPerformance preset.
        // --------------------------------------------------------------------
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
                    // Apply the high‑performance quality setting before decoding.
                    reader.QualitySettings = QualitySettings.HighPerformance;

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
                Console.WriteLine($"Failed to load image '{file}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{file}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup: delete the temporary folder and its contents.
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}