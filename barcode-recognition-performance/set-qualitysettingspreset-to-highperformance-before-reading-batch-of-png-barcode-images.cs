// Title: Read PNG Barcodes with HighPerformance Quality Settings
// Description: Demonstrates generating a set of PNG barcode images and then reading them using Aspose.BarCode with the HighPerformance quality preset to maximize processing speed.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and QualitySettings for tuning performance. Typical scenarios include batch processing of scanned images, high‑throughput barcode scanning, and automated verification pipelines where speed is critical. Developers often need to adjust quality presets to balance accuracy and performance.
// Prompt: Set QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images.
// Tags: barcode symbology, generation, recognition, png, highperformance, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample PNG barcodes, then reads them using a high‑performance quality preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates barcodes, reads them, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // List to hold generated PNG file paths
        List<string> barcodeFiles = new List<string>();

        try
        {
            // Generate sample barcode images and collect their file paths
            GenerateSampleBarcodes(batchFolder, barcodeFiles);

            // Read the generated barcodes with HighPerformance quality preset
            ReadBarcodes(batchFolder, barcodeFiles);
        }
        finally
        {
            // Clean up: delete generated files and folder
            foreach (string file in barcodeFiles)
            {
                try { File.Delete(file); } catch { /* ignore */ }
            }
            try { Directory.Delete(batchFolder, true); } catch { /* ignore */ }
        }
    }

    // Generates a few sample PNG barcodes and records their file paths
    private static void GenerateSampleBarcodes(string folder, List<string> fileList)
    {
        // Sample data: tuple of (symbology, code text, file name)
        var samples = new (BaseEncodeType encodeType, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.text))
            {
                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            fileList.Add(filePath);
        }
    }

    // Reads each PNG barcode file using HighPerformance quality settings
    private static void ReadBarcodes(string folder, List<string> files)
    {
        foreach (string file in files)
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
                    // Apply the HighPerformance preset before decoding
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Iterate over all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText} | Type: {result.CodeTypeName}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
            }
        }
    }
}