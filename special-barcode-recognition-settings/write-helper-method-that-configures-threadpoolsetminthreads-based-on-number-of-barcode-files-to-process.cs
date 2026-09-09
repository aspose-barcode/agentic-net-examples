// Title: Configure ThreadPool Minimum Threads for Barcode Processing
// Description: Demonstrates generating barcode images, configuring the .NET ThreadPool based on the number of barcode files, and reading them using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode .NET library category of barcode generation and recognition. It showcases the use of BarcodeGenerator for creating Code128 barcodes, BarCodeReader for decoding them, and ThreadPool configuration to optimize parallel processing. Developers working with bulk barcode operations often need to adjust thread pool settings to improve performance when handling many files.
// Prompt: Write a helper method that configures ThreadPool.SetMinThreads based on the number of barcode files to process.
// Tags: barcode symbology, generation, recognition, threadpool, code128, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates barcode images, configures the ThreadPool based on file count,
/// reads the barcodes, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode files
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Configure ThreadPool based on number of files
        ConfigureThreadPoolMinThreads(barcodeFiles.Count);

        // Read each barcode file (demonstration)
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Perform barcode recognition
                    reader.ReadBarCodes();
                    foreach (BarCodeResult result in reader.FoundBarCodes)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{file}': {ex.Message}");
            }
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Configures the minimum number of worker threads in the ThreadPool based on the number of barcode files to process.
    /// </summary>
    /// <param name="fileCount">The total number of barcode files that will be processed.</param>
    static void ConfigureThreadPoolMinThreads(int fileCount)
    {
        if (fileCount <= 0)
        {
            Console.WriteLine("No barcode files to process; ThreadPool configuration skipped.");
            return;
        }

        // Retrieve the maximum number of threads allowed by the ThreadPool
        int maxWorkerThreads, maxCompletionPortThreads;
        ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);

        // Determine desired minimum: at least 1, not exceeding the maximum, and proportional to file count
        int desiredMin = Math.Min(Math.Max(1, fileCount), maxWorkerThreads);

        // Apply the new minimum thread settings
        bool result = ThreadPool.SetMinThreads(desiredMin, maxCompletionPortThreads);
        Console.WriteLine($"ThreadPool minimum worker threads set to {desiredMin} (success: {result})");
    }
}