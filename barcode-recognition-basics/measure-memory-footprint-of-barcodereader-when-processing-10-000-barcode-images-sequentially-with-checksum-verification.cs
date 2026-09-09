// Title: Measure BarCodeReader memory usage with checksum verification
// Description: Demonstrates how to generate barcode images, read them with checksum validation enabled, and measure the average memory footprint of BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode processing category, focusing on memory profiling during sequential barcode reading. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings such as ChecksumValidation. Developers often need to assess resource consumption when handling large batches of barcodes in high‑throughput applications.
// Prompt: Measure memory footprint of BarCodeReader when processing 10,000 barcode images sequentially with checksum verification enabled.
// Tags: code128, memory, checksum, reading, png, barcodegenerator, barcodereader, decodetype, checksumvalidation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a set of barcode images, reads them with checksum verification,
/// and calculates the average memory consumption of the BarCodeReader instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, reading, memory measurement,
    /// and cleanup steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a dedicated temporary folder for barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate a small set of barcode images (10 samples for safe demo)
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 10; i++)
        {
            string filePath = Path.Combine(tempFolder, $"code{i:D4}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i:D4}"))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        long totalMemoryDiff = 0;
        int processedCount = 0;

        // --------------------------------------------------------------------
        // Process each generated image, measuring memory before and after reading
        // --------------------------------------------------------------------
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Measure memory before creating the reader
            long before = GC.GetTotalMemory(true);

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Enable checksum verification
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Perform reading
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing
                    Console.WriteLine($"Read {result.CodeTypeName}: {result.CodeText}");
                }
            }

            // Measure memory after disposing the reader
            long after = GC.GetTotalMemory(true);
            long diff = after - before;
            totalMemoryDiff += diff;
            processedCount++;
        }

        // --------------------------------------------------------------------
        // Output average memory footprint information
        // --------------------------------------------------------------------
        if (processedCount > 0)
        {
            double averageMemory = totalMemoryDiff / (double)processedCount;
            Console.WriteLine($"Processed {processedCount} barcodes.");
            Console.WriteLine($"Average memory footprint of BarCodeReader (bytes): {averageMemory:F0}");
        }
        else
        {
            Console.WriteLine("No barcodes were processed.");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in imageFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}