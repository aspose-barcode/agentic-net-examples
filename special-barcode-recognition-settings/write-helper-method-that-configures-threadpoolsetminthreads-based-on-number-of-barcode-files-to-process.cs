// Title: Generate Sample Barcodes and Configure ThreadPool Minimum Threads
// Description: This example creates a set of Code128 barcode PNG images using Aspose.BarCode and then adjusts the .NET ThreadPool minimum worker threads based on the number of generated files.
// Category-Description: Demonstrates basic Aspose.BarCode generation combined with .NET ThreadPool tuning. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and common file I/O patterns. Developers working on bulk barcode creation or processing pipelines often need to balance thread resources; this snippet illustrates how to calculate and set appropriate minimum threads for improved concurrency.
// Prompt: Write a helper method that configures ThreadPool.SetMinThreads based on the number of barcode files to process.
// Tags: barcode symbology, generation, threadpool, multithreading, aspose.barcode, png, code128

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating sample barcode images and configuring the ThreadPool minimum worker threads based on the file count.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode PNG files, counts them, and configures the ThreadPool.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Prepare a folder for sample barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folder);

        // Generate a few sample barcode files (default 5)
        int sampleCount = 5;
        for (int i = 1; i <= sampleCount; i++)
        {
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath);
            }
        }

        // Count the generated barcode files
        string[] files = Directory.GetFiles(folder, "*.png");
        int barcodeFileCount = files.Length;
        Console.WriteLine($"Found {barcodeFileCount} barcode files in '{folder}'.");

        // Configure ThreadPool based on the number of files
        ConfigureThreadPool(barcodeFileCount);
    }

    // Helper method that sets ThreadPool minimum worker threads
    static void ConfigureThreadPool(int barcodeFileCount)
    {
        // Retrieve current minimum thread settings
        ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);

        // Desired worker threads: at least the number of files and at least 2 * processor count
        int desiredWorkerThreads = Math.Max(workerThreads, Math.Max(barcodeFileCount, Environment.ProcessorCount * 2));

        // Apply the new minimum thread settings
        bool success = ThreadPool.SetMinThreads(desiredWorkerThreads, completionPortThreads);

        Console.WriteLine($"ThreadPool minimum worker threads set to {desiredWorkerThreads} (success: {success}).");
    }
}