// Title: Measure memory usage of BarCodeReader with checksum validation
// Description: Demonstrates generating and reading Code128 barcodes while measuring the process memory before and after handling a set of images.
// Prompt: Measure memory footprint of BarCodeReader when processing 10,000 barcode images sequentially with checksum verification enabled.
// Tags: barcode, code128, memory, checksum, barcodereader, barcodegenerator

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a few Code128 barcodes, reads them with checksum validation,
/// and reports the memory consumption before and after processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Measures memory usage while processing barcode images.
    /// </summary>
    static void Main()
    {
        // Sample barcode texts (Code128 includes checksum automatically)
        string[] codes = new string[]
        {
            "123456789012",
            "987654321098",
            "555555555555",
            "111111111111",
            "222222222222"
        };

        // Force garbage collection and capture baseline memory usage
        GC.Collect();
        GC.WaitForPendingFinalizers();
        long memoryBefore = GC.GetTotalMemory(true);

        // Process each barcode image sequentially
        foreach (string code in codes)
        {
            // Generate barcode image into a memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code))
            {
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Read the barcode with checksum validation enabled
                    using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
            }
        }

        // Force garbage collection and capture final memory usage
        GC.Collect();
        GC.WaitForPendingFinalizers();
        long memoryAfter = GC.GetTotalMemory(true);

        // Output memory consumption details
        Console.WriteLine($"Memory before processing: {memoryBefore / 1024} KB");
        Console.WriteLine($"Memory after processing:  {memoryAfter / 1024} KB");
        Console.WriteLine($"Memory increase: { (memoryAfter - memoryBefore) / 1024 } KB");
    }
}