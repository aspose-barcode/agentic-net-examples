// Title: Barcode ThreadPool Diagnostic Example
// Description: Demonstrates how to capture ThreadPool thread counts before and after generating and reading a barcode using Aspose.BarCode. Useful for assessing resource usage in barcode processing tasks.
// Category-Description: This example belongs to the Aspose.BarCode usage category focusing on performance diagnostics and resource monitoring. It showcases the BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and .NET ThreadPool APIs to report thread pool statistics. Developers often need such patterns to ensure barcode operations do not exhaust thread resources in high‑throughput applications.
// Prompt: Implement a diagnostic tool that reports current ThreadPool thread counts before and after barcode processing.
// Tags: barcode, threadpool, diagnostics, code128, generation, recognition, aspose.barcode, performance

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reporting ThreadPool statistics surrounding barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it back, and logs ThreadPool metrics before and after processing.
    /// </summary>
    static void Main()
    {
        // Report ThreadPool counts before barcode processing
        ReportThreadPoolCounts("Before processing");

        // Prepare a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDiag_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read and decode the generated barcode image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {reader.FoundCount}");
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Report ThreadPool counts after barcode processing
        ReportThreadPoolCounts("After processing");

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves and writes the current ThreadPool maximum and available thread counts to the console.
    /// </summary>
    /// <param name="label">A descriptive label indicating the point in execution (e.g., "Before processing").</param>
    static void ReportThreadPoolCounts(string label)
    {
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxCompletion);
        ThreadPool.GetAvailableThreads(out int availWorker, out int availCompletion);
        Console.WriteLine($"{label} ThreadPool counts:");
        Console.WriteLine($"  Max Worker Threads: {maxWorker}");
        Console.WriteLine($"  Max Completion Port Threads: {maxCompletion}");
        Console.WriteLine($"  Available Worker Threads: {availWorker}");
        Console.WriteLine($"  Available Completion Port Threads: {availCompletion}");
    }
}