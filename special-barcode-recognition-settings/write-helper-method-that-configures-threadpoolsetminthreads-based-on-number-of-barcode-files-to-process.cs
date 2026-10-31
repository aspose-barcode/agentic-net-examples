// Title: Configure ThreadPool Minimum Threads for Barcode Processing
// Description: Demonstrates generating multiple barcode images, configuring ThreadPool minimum threads based on file count, and reading the barcodes.
// Category-Description: This example belongs to the Aspose.BarCode operations category covering barcode generation and recognition. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and ThreadPool configuration, typical for batch processing scenarios where developers need to optimize parallel execution of barcode tasks.
// Prompt: Write a helper method that configures ThreadPool.SetMinThreads based on the number of barcode files to process.
// Tags: barcode symbology, generation, recognition, threadpool, csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, ThreadPool configuration, and barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode files, configures ThreadPool, and reads the barcodes.
    /// </summary>
    static void Main()
    {
        // Sample number of barcode files to process
        int numberOfFiles = 5;

        // Create a dedicated temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < numberOfFiles; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        Console.WriteLine($"Generated {barcodeFiles.Count} barcode files in: {tempFolder}");

        // Configure ThreadPool minimum threads based on the number of generated files
        ConfigureThreadPoolMinThreads(barcodeFiles.Count);

        // Read each generated barcode using BarCodeReader (optional demonstration)
        Stopwatch sw = Stopwatch.StartNew();
        foreach (string file in barcodeFiles)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                reader.ReadBarCodes();
                foreach (var result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeText}");
                }
            }
        }
        sw.Stop();
        Console.WriteLine($"Reading completed in {sw.ElapsedMilliseconds} ms");
    }

    /// <summary>
    /// Configures the ThreadPool's minimum worker threads based on the number of barcode files to process.
    /// </summary>
    /// <param name="fileCount">The number of barcode files that will be processed.</param>
    static void ConfigureThreadPoolMinThreads(int fileCount)
    {
        if (fileCount < 0)
            throw new ArgumentOutOfRangeException(nameof(fileCount), "File count cannot be negative.");

        // Retrieve current maximum thread settings
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);

        // Desired minimum threads: one per file, but not exceeding the max worker threads
        int desiredMin = Math.Max(1, Math.Min(fileCount, maxWorkerThreads));

        // Apply the new minimum thread settings
        bool success = ThreadPool.SetMinThreads(desiredMin, maxCompletionPortThreads);
        if (!success)
            Console.WriteLine("Failed to set ThreadPool minimum threads.");
        else
            Console.WriteLine($"ThreadPool minimum worker threads set to {desiredMin}.");
    }
}