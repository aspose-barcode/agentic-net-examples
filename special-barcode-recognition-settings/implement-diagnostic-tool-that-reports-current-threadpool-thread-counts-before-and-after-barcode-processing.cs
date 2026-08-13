// Title: ThreadPool Diagnostic for Aspose.BarCode Generation and Recognition
// Description: Demonstrates how to capture ThreadPool thread counts before and after barcode generation and reading using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode processing category, illustrating the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. It shows typical workflow steps—setup, generation, decoding, and cleanup—while reporting ThreadPool metrics, a common need for developers optimizing concurrency and resource usage in barcode applications.
// Prompt: Implement a diagnostic tool that reports current ThreadPool thread counts before and after barcode processing.
// Tags: barcode, threadpool, diagnostics, generation, recognition, code128, png, aspose.barcode

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates ThreadPool diagnostics around barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Retrieves current ThreadPool information.
    /// </summary>
    /// <returns>A formatted string containing available and maximum worker and I/O threads.</returns>
    static string GetThreadPoolInfo()
    {
        ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxCompletion);
        return $"Available Worker Threads: {workerThreads}/{maxWorker}, Available IO Threads: {completionPortThreads}/{maxCompletion}";
    }

    /// <summary>
    /// Entry point. Reports ThreadPool status, generates a Code128 barcode, reads it back, and reports ThreadPool status again.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Display ThreadPool info before any barcode work.
        Console.WriteLine("ThreadPool info before barcode processing:");
        Console.WriteLine(GetThreadPoolInfo());

        // Prepare output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        string barcodePath = Path.Combine(outputDir, "sample.png");

        // Generate a barcode image using Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath);
        }

        // Read the generated barcode to simulate processing work.
        if (File.Exists(barcodePath))
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Read barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
        }

        // Display ThreadPool info after barcode work.
        Console.WriteLine("ThreadPool info after barcode processing:");
        Console.WriteLine(GetThreadPoolInfo());
    }
}