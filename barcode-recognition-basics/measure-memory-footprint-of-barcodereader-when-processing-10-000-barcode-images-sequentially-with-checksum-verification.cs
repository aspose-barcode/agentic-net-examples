// Title: Measure memory usage of BarCodeReader with checksum validation
// Description: Demonstrates how to generate a set of Code11 barcode images, read them sequentially with checksum verification enabled, and measure the process's memory footprint.
// Category-Description: This example belongs to the Aspose.BarCode barcode processing category, illustrating the use of BarcodeGenerator for image creation and BarCodeReader for decoding with checksum validation. Developers often need to assess memory consumption when handling large batches of barcodes, especially in high‑throughput or resource‑constrained environments. The sample showcases key API classes such as BarcodeGenerator, BarCodeReader, and related settings, providing a template for performance testing and optimization.
// Prompt: Measure memory footprint of BarCodeReader when processing 10,000 barcode images sequentially with checksum verification enabled.
// Tags: code11, checksum, memory, performance, barcodereader, barcodegenerator, aspnet, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring the memory footprint of BarCodeReader while processing a batch of barcode images with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Code11 barcodes, reads them with checksum verification, and reports memory usage.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of sample images (use small safe count; replace with 10000 for real test)
        int sampleCount = 10;
        List<string> barcodeFiles = new List<string>();

        // Generate sample barcode images (Code11 has obligatory checksum)
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = (123456 + i).ToString(); // simple numeric text
            string filePath = Path.Combine(tempFolder, $"code_{i}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, codeText))
            {
                // Set X-dimension to control image size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save the generated barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // Measure memory before processing
        long memoryBefore = GC.GetTotalMemory(true);

        // Process each barcode image sequentially with checksum verification enabled
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code11))
            {
                // Enable checksum validation (On)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read barcodes (result is ignored; we just force the read)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing
                    string text = result.CodeText;
                    string type = result.CodeTypeName;
                }
            }
        }

        // Measure memory after processing
        long memoryAfter = GC.GetTotalMemory(true);
        long memoryDiff = memoryAfter - memoryBefore;

        // Output memory usage statistics
        Console.WriteLine($"Memory before processing: {memoryBefore:N0} bytes");
        Console.WriteLine($"Memory after processing:  {memoryAfter:N0} bytes");
        Console.WriteLine($"Memory increase:          {memoryDiff:N0} bytes");

        // Cleanup temporary files
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}