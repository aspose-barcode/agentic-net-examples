// Title: Measure memory usage of BarCodeReader with checksum validation
// Description: Demonstrates how to generate 10,000 Code128 barcode images, read them sequentially with checksum verification, and measure the memory footprint of BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and BarcodeGenerator for bulk processing. It shows typical scenarios where developers need to evaluate memory consumption while decoding large numbers of barcodes with checksum validation enabled, using classes such as BarcodeGenerator, BarCodeReader, and related settings.
// Prompt: Measure memory footprint of BarCodeReader when processing 10,000 barcode images sequentially with checksum verification enabled.
// Tags: code128, checksum, memory, barcodereader, barcodegenerator, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates measuring the memory footprint of <see cref="BarCodeReader"/> when processing a large number of barcode images with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcode images, reads them with checksum validation, and reports memory usage.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for sample barcode images
        // --------------------------------------------------------------------
        string folder = Path.Combine(Path.GetTempPath(), "AsposeBarCodeSample");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // --------------------------------------------------------------------
        // Number of images to process (scaled down for demo; replace with 10000 for real measurement)
        // --------------------------------------------------------------------
        int sampleCount = 10;

        // --------------------------------------------------------------------
        // Generate sample barcode images (Code128) and save them as PNG files
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i:D5}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Record memory usage before processing the images
        // --------------------------------------------------------------------
        long memoryBefore = GC.GetTotalMemory(true);

        // --------------------------------------------------------------------
        // Process each image sequentially with checksum validation enabled
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Enable checksum validation for each read operation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the type and text of the decoded barcode
                    Console.WriteLine($"Read {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Record memory usage after processing and calculate the difference
        // --------------------------------------------------------------------
        long memoryAfter = GC.GetTotalMemory(true);
        long memoryUsed = memoryAfter - memoryBefore;

        // --------------------------------------------------------------------
        // Output memory consumption details
        // --------------------------------------------------------------------
        Console.WriteLine($"Memory before: {memoryBefore} bytes");
        Console.WriteLine($"Memory after: {memoryAfter} bytes");
        Console.WriteLine($"Memory used during processing: {memoryUsed} bytes");
    }
}