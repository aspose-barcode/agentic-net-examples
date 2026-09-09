// Title: Multi‑Threaded vs Single‑Threaded Barcode Recognition Throughput
// Description: Demonstrates how to enable multi‑threaded barcode recognition using Aspose.BarCode and compares its performance against single‑threaded execution on a set of generated images.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader, ProcessorSettings, and threading configuration to optimize scanning speed. Developers often need to process large batches of barcode images efficiently; this snippet illustrates typical use cases such as adjusting thread pool limits, toggling core usage, and measuring throughput for performance tuning.
// Prompt: Enable multi‑threaded recognition and compare throughput against single‑threaded execution on a set of images.
// Tags: barcode, recognition, multithreading, performance, aspose.barcode, processorsettings, threadpool

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates single‑ and multi‑threaded barcode recognition using Aspose.BarCode and measures performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs recognition in single‑ and multi‑threaded modes, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode images
        List<string> imageFiles = GenerateSampleBarcodes(tempFolder, 5);

        // -------------------------------------------------
        // Single‑threaded recognition
        // -------------------------------------------------
        Console.WriteLine("Single‑threaded recognition:");
        // Configure processor to use only one core and no additional threads
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Perform recognition and capture results
        var singleResult = RecognizeBarcodes(imageFiles);
        Console.WriteLine($"Total barcodes found: {singleResult.TotalFound}");
        Console.WriteLine($"Elapsed time: {singleResult.ElapsedMilliseconds} ms");
        Console.WriteLine();

        // -------------------------------------------------
        // Multi‑threaded recognition (all available cores)
        // -------------------------------------------------
        Console.WriteLine("Multi‑threaded recognition:");
        // Optionally adjust ThreadPool limits to provide more worker threads
        int workerThreads, completionPortThreads;
        ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMaxThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);

        // Enable use of all cores and allow additional threads for processing
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Perform recognition and capture results
        var multiResult = RecognizeBarcodes(imageFiles);
        Console.WriteLine($"Total barcodes found: {multiResult.TotalFound}");
        Console.WriteLine($"Elapsed time: {multiResult.ElapsedMilliseconds} ms");
        Console.WriteLine();

        // -------------------------------------------------
        // Cleanup temporary files
        // -------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use)
        }
    }

    /// <summary>
    /// Generates a specified number of PDF417 barcode images and returns their file paths.
    /// </summary>
    /// <param name="folder">Folder where images will be saved.</param>
    /// <param name="count">Number of barcode images to generate.</param>
    /// <returns>List of generated image file paths.</returns>
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string text = $"Sample{i + 1}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, text))
            {
                // Default parameters are sufficient for this demo
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }
        return files;
    }

    /// <summary>
    /// Holds aggregated recognition results.
    /// </summary>
    struct RecognitionResult
    {
        public int TotalFound;
        public long ElapsedMilliseconds;
    }

    /// <summary>
    /// Recognizes barcodes in the provided list of image files and returns total count and elapsed time.
    /// </summary>
    /// <param name="files">List of image file paths to process.</param>
    /// <returns>RecognitionResult containing total barcodes found and processing time.</returns>
    static RecognitionResult RecognizeBarcodes(List<string> files)
    {
        int totalFound = 0;
        Stopwatch watch = Stopwatch.StartNew();

        // Iterate through each file and read barcodes
        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                try
                {
                    // Perform barcode detection
                    reader.ReadBarCodes();
                    totalFound += reader.FoundCount;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading '{Path.GetFileName(file)}': {ex.Message}");
                }
            }
        }

        watch.Stop();
        return new RecognitionResult { TotalFound = totalFound, ElapsedMilliseconds = watch.ElapsedMilliseconds };
    }
}