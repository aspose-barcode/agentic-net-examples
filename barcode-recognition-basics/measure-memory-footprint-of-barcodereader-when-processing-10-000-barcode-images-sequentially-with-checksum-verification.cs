// Title: Measure memory usage of BarCodeReader processing multiple images with checksum validation
// Description: Demonstrates how to generate Code128 barcode images, read them sequentially with checksum verification enabled, and measure the memory footprint of the BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing typical use cases such as creating barcode images, configuring reader settings (e.g., checksum validation), and profiling memory consumption. Developers working with bulk barcode processing, performance tuning, or resource‑constrained environments often need to understand the memory impact of the BarCodeReader API.
// Prompt: Measure memory footprint of BarCodeReader when processing 10,000 barcode images sequentially with checksum verification enabled.
// Tags: barcode, code128, memory, checksum, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates barcode images, reads them with checksum validation,
/// and measures the memory used by the BarCodeReader during processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeReaderMemoryTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of sample images to generate (adjustable for real‑world testing)
        int sampleCount = 10;

        // Generate sample barcode images (Code128) and collect their file paths
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            string codeText = $"CODE{i:D4}";
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Force garbage collection before measuring memory usage
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Record memory usage prior to processing
        long memoryBefore = GC.GetTotalMemory(true);

        // Process each barcode image sequentially with checksum verification enabled
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Enable checksum validation for the reader
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read barcodes; results may be empty if checksum validation fails
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    // Output minimal information to ensure the read operation occurs
                    Console.WriteLine($"Read: {result.CodeTypeName} - {result.CodeText}");
                }
            }
        }

        // Force garbage collection after processing to obtain a clean memory measurement
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Record memory usage after processing and calculate the difference
        long memoryAfter = GC.GetTotalMemory(true);
        long memoryUsed = memoryAfter - memoryBefore;

        Console.WriteLine($"Memory used for processing {sampleCount} barcodes: {memoryUsed} bytes");

        // Clean up temporary files and directory
        try
        {
            foreach (string file in imageFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}