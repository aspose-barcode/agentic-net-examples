// Title: Measure memory usage of BarCodeReader processing multiple images with checksum validation
// Description: Demonstrates generating sample barcode images, reading them sequentially with checksum verification enabled, and measuring the memory footprint of the BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode reading and performance measurement category. It showcases the BarCodeReader class, checksum validation via BarcodeSettings, and memory profiling using GC.GetTotalMemory. Developers often need to assess resource consumption when processing large batches of barcodes, especially in high‑throughput or memory‑constrained environments.
// Prompt: Measure memory footprint of BarCodeReader when processing 10,000 barcode images sequentially with checksum verification enabled.
// Tags: barcode, symbology, reading, checksum, memory, performance, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a sample that generates barcode images, reads them with checksum validation,
/// and measures the memory consumption of the BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates sample barcodes, processes them while measuring memory usage,
    /// and outputs the results to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of sample images to generate (use a small safe number for demo)
        // In a real benchmark replace with 10000
        int sampleCount = 10;

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = GenerateSampleBarcodes(sampleCount, tempFolder);

        // Record memory usage before processing the images
        long memoryBefore = GC.GetTotalMemory(true);

        // Process each barcode image sequentially with checksum validation enabled
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the reader for all supported decode types
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Enable checksum validation for the current read operation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes present in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output result information (optional, useful for verification)
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | CodeText: {result.CodeText} | Type: {result.CodeTypeName}");
                }
            }
        }

        // Record memory usage after processing the images
        long memoryAfter = GC.GetTotalMemory(true);
        long memoryUsed = memoryAfter - memoryBefore;

        Console.WriteLine();
        Console.WriteLine($"Memory used for processing {sampleCount} images: {memoryUsed} bytes");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors in demo scenarios
        }
    }

    /// <summary>
    /// Generates a specified number of barcode PNG files in the given folder.
    /// </summary>
    /// <param name="count">Number of barcode images to create.</param>
    /// <param name="folder">Destination folder for the generated images.</param>
    /// <returns>List of file paths for the created barcode images.</returns>
    private static List<string> GenerateSampleBarcodes(int count, string folder)
    {
        List<string> files = new List<string>();

        for (int i = 0; i < count; i++)
        {
            string codeText = $"CODE{i:D5}";
            string filePath = Path.Combine(folder, $"barcode_{i}.png");

            // Create a barcode generator for Code128 symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode image as PNG; default size adapts to the CodeText length
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }

        return files;
    }
}