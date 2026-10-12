// Title: Barcode Generation, Recognition, and ThreadPool Configuration Example
// Description: Demonstrates creating barcode images, reading them back, and adjusting the .NET ThreadPool minimum threads based on the number of files to process.
// Category-Description: This example belongs to the Aspose.BarCode .NET library category covering barcode generation and recognition workflows. It showcases the use of BarcodeGenerator for encoding, BarCodeReader for decoding, and ThreadPool management to optimize parallel processing of multiple barcode files. Developers often need to generate batches of barcodes and efficiently read them, adjusting thread pool settings to match workload size.
// Prompt: Write a helper method that configures ThreadPool.SetMinThreads based on the number of barcode files to process.
// Tags: barcode generation, barcode recognition, threadpool, code128, png, aspose.barcode, multithreading

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and dynamic ThreadPool configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, configures the ThreadPool,
    /// reads each barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode files
        int sampleCount = 5;
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"Sample{i + 1}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Configure ThreadPool based on the number of files to be processed
        ConfigureThreadPoolMinThreads(barcodeFiles.Count);

        // Read each barcode file and output the result
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Perform barcode detection
                reader.ReadBarCodes();
                Console.WriteLine($"File: {Path.GetFileName(file)} - Barcodes found: {reader.FoundCount}");

                // Iterate over all detected barcodes
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Adjusts the minimum number of worker threads in the ThreadPool based on the number of barcode files.
    /// Ensures the minimum is at least the file count (or processor count) without exceeding the maximum allowed.
    /// </summary>
    /// <param name="fileCount">The total number of barcode files to process.</param>
    static void ConfigureThreadPoolMinThreads(int fileCount)
    {
        // Retrieve current ThreadPool settings
        ThreadPool.GetMinThreads(out int currentWorker, out int currentCompletion);
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxCompletion);

        // Determine desired minimum worker threads (at least the number of files, but not exceeding max)
        int desiredMin = Math.Min(Math.Max(fileCount, Environment.ProcessorCount), maxWorker);

        // Apply the new minimum worker thread count while keeping completion port threads unchanged
        bool result = ThreadPool.SetMinThreads(desiredMin, currentCompletion);
        Console.WriteLine($"ThreadPool MinThreads set to {desiredMin}: {(result ? "Success" : "Failed")}");
    }
}