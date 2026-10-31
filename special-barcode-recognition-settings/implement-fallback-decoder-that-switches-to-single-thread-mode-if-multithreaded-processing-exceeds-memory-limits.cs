// Title: Fallback Decoder with Memory-Aware Thread Switching
// Description: Demonstrates generating a QR code, reading it using multithreaded processing, and falling back to single‑thread mode when memory usage exceeds a defined limit.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. It illustrates typical use cases such as multithreaded decoding for performance and handling memory constraints by switching to single‑thread processing. Developers working with high‑throughput barcode scanning often need to balance speed and resource usage, making this pattern valuable for robust applications.
// Prompt: Implement a fallback decoder that switches to single‑thread mode if multithreaded processing exceeds memory limits.
// Tags: barcode, qr, generation, recognition, multithreading, memory, fallback, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, multithreaded decoding, and a memory‑aware fallback to single‑thread decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, attempts multithreaded decoding,
    /// and switches to single‑thread mode if memory consumption exceeds the defined limit.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 5;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        const long memoryLimitBytes = 50L * 1024 * 1024; // 50 MB memory threshold

        // Enable multithreaded processing (default: use all CPU cores)
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        Console.WriteLine("Multithreaded read attempt:");
        long before = GC.GetTotalMemory(true);

        // Perform barcode reading using multithreaded mode
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.ReadBarCodes();
            foreach (var result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        long after = GC.GetTotalMemory(true);
        long used = after - before;
        Console.WriteLine($"Memory used: {used / 1024} KB");

        // If memory usage exceeds the limit, fall back to single‑thread decoding
        if (used > memoryLimitBytes)
        {
            Console.WriteLine("Memory limit exceeded, switching to single‑thread mode.");

            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

            // Perform barcode reading using single‑thread mode
            using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
            {
                reader.ReadBarCodes();
                foreach (var result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"[SingleThread] {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored: cleanup failures are non‑critical for this demo
        }
    }
}