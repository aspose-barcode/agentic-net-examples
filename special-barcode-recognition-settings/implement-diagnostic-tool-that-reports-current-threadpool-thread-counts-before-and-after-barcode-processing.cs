// Title: ThreadPool Diagnostic for Barcode Generation and Recognition
// Description: Demonstrates how to capture ThreadPool thread counts before and after creating and reading a barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing barcode generation (BarcodeGenerator) and recognition (BarCodeReader). It illustrates typical use cases such as creating a Code128 barcode, saving it as PNG, and decoding it, while monitoring ThreadPool resources—a common requirement for performance‑sensitive applications. Developers often need to assess thread usage when integrating barcode operations into multithreaded services.
// Prompt: Implement a diagnostic tool that reports current ThreadPool thread counts before and after barcode processing.
// Tags: barcode, code128, generation, recognition, threadpool, diagnostics, aspose.barcode, png

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition while reporting ThreadPool statistics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it back, and logs ThreadPool info before and after processing.
    /// </summary>
    static void Main()
    {
        // Report ThreadPool counts before any barcode work
        GetThreadPoolInfo("Before processing");

        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "DiagBarcode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read and decode the generated barcode
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.ReadBarCodes();
            Console.WriteLine($"Found {reader.FoundCount} barcode(s).");
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Report ThreadPool counts after barcode work
        GetThreadPoolInfo("After processing");

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }

    /// <summary>
    /// Writes the current ThreadPool maximum and available thread counts to the console.
    /// </summary>
    /// <param name="label">Label indicating the point in execution (e.g., "Before processing").</param>
    static void GetThreadPoolInfo(string label)
    {
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxIO);
        ThreadPool.GetAvailableThreads(out int availWorker, out int availIO);
        Console.WriteLine($"{label} - ThreadPool MaxWorkerThreads: {maxWorker}, AvailableWorkerThreads: {availWorker}");
        Console.WriteLine($"{label} - ThreadPool MaxIOThreads: {maxIO}, AvailableIOThreads: {availIO}");
    }
}