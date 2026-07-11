// Title: Demonstrates barcode generation, reading, and ThreadPool configuration
// Description: Generates sample Code128 barcodes, reads them, and adjusts ThreadPool minimum threads based on file count.
// Category-Description: This example belongs to Aspose.BarCode usage for barcode generation and recognition, showcasing how to work with BarcodeGenerator, BarCodeReader, and ThreadPool settings. Developers often need to process multiple barcode images efficiently, requiring proper thread pool tuning to improve throughput in batch operations.
// Prompt: Write a helper method that configures ThreadPool.SetMinThreads based on the number of barcode files to process.
// Tags: barcode symbology, generation, recognition, threadpool, multithreading, aspose.barcode, code128, png

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program demonstrating barcode generation, reading, and ThreadPool configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes if needed, configures ThreadPool, and reads each barcode.
    /// </summary>
    static void Main()
    {
        // Directory to hold sample barcode images
        const string barcodeDir = "Barcodes";

        // Ensure the directory exists and contains a few sample files
        if (!Directory.Exists(barcodeDir))
        {
            Directory.CreateDirectory(barcodeDir);
            GenerateSampleBarcodes(barcodeDir, 5);
        }

        // Retrieve all PNG files in the directory
        string[] barcodeFiles = Directory.GetFiles(barcodeDir, "*.png");
        Console.WriteLine($"Found {barcodeFiles.Length} barcode file(s) to process.");

        // Configure ThreadPool based on the number of files to process
        ConfigureThreadPool(barcodeFiles.Length);

        // Process each barcode file: read and output its type and text
        foreach (string filePath in barcodeFiles)
        {
            using (var reader = new BarCodeReader(filePath))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // Program ends here; no waiting for user input.
    }

    // Generates a given number of sample Code128 barcode images.
    private static void GenerateSampleBarcodes(string directory, int count)
    {
        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

        for (int i = 1; i <= count; i++)
        {
            string fileName = Path.Combine(directory, $"sample_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Optional: set a modest image size
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 100f;
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
        }
    }

    // Adjusts the ThreadPool's minimum worker threads based on workload size.
    private static void ConfigureThreadPool(int fileCount)
    {
        if (fileCount < 0) throw new ArgumentOutOfRangeException(nameof(fileCount));

        // Retrieve current minimum thread settings.
        ThreadPool.GetMinThreads(out int currentWorkerMin, out int currentCompletionPortMin);

        // Determine a reasonable minimum: at least the current setting,
        // the number of files, and twice the processor count.
        int desiredWorkerMin = Math.Max(currentWorkerMin,
                                Math.Max(fileCount, Environment.ProcessorCount * 2));

        // Apply the new minimum; keep the completion port minimum unchanged.
        bool success = ThreadPool.SetMinThreads(desiredWorkerMin, currentCompletionPortMin);
        if (!success)
        {
            Console.WriteLine("Warning: Unable to set the desired minimum thread count.");
        }
        else
        {
            Console.WriteLine($"ThreadPool minimum worker threads set to {desiredWorkerMin}.");
        }
    }
}