// Title: Multi‑Threaded vs Single‑Threaded Barcode Recognition Performance Demo
// Description: Demonstrates how to enable multi‑threaded barcode recognition using Aspose.BarCode and compares its throughput against single‑threaded execution on a set of generated images.
// Category-Description: This example belongs to the Aspose.BarCode performance and threading category. It shows how to configure BarCodeReader.ProcessorSettings, adjust ThreadPool limits, and measure execution time. Developers working with bulk barcode scanning, high‑throughput applications, or needing to benchmark multi‑core utilization will find this pattern useful.
// Prompt: Enable multi‑threaded recognition and compare throughput against single‑threaded execution on a set of images.
// Tags: barcode, recognition, multithreading, performance, aspose.barcode, processorsettings, threadpool, benchmark

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates multi‑threaded and single‑threaded barcode recognition using Aspose.BarCode,
/// measuring and comparing processing times for a batch of generated barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs recognition in single‑ and multi‑threaded modes,
    /// outputs timing results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        GenerateSampleBarcodes(tempFolder, barcodeFiles);

        // Single‑threaded recognition
        ConfigureProcessorSettings(singleThread: true);
        long singleThreadTime = RecognizeBarcodes(barcodeFiles);
        Console.WriteLine($"Single‑threaded total time: {singleThreadTime} ms");

        // Multi‑threaded recognition
        ConfigureProcessorSettings(singleThread: false);
        long multiThreadTime = RecognizeBarcodes(barcodeFiles);
        Console.WriteLine($"Multi‑threaded total time: {multiThreadTime} ms");

        // Cleanup temporary folder
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
    /// Generates a set of sample barcode images of various symbologies and stores their paths.
    /// </summary>
    /// <param name="folder">Folder where images will be saved.</param>
    /// <param name="fileList">List to receive the full file paths of generated images.</param>
    static void GenerateSampleBarcodes(string folder, List<string> fileList)
    {
        // Sample data: tuple of (symbology, text, file name)
        var samples = new (BaseEncodeType encode, string text, string file)[]
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Aztec, "AztecCode", "aztec.png")
        };

        foreach (var (encode, text, file) in samples)
        {
            string path = Path.Combine(folder, file);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(path, BarCodeImageFormat.Png);
            }
            fileList.Add(path);
        }
    }

    /// <summary>
    /// Configures the global processor settings for the BarCodeReader to control threading behavior.
    /// </summary>
    /// <param name="singleThread">If true, forces single‑threaded execution; otherwise enables multi‑threading.</param>
    static void ConfigureProcessorSettings(bool singleThread)
    {
        if (singleThread)
        {
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        }
        else
        {
            BarCodeReader.ProcessorSettings.UseAllCores = true;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

            // Optionally enlarge ThreadPool limits
            int worker, completion;
            ThreadPool.GetMaxThreads(out worker, out completion);
            ThreadPool.SetMaxThreads(Math.Max(Environment.ProcessorCount * 4, worker), completion);
            ThreadPool.GetMinThreads(out worker, out completion);
            ThreadPool.SetMinThreads(Math.Max(Environment.ProcessorCount * 4, worker), completion);
        }
    }

    /// <summary>
    /// Recognizes barcodes in the provided files, measuring total elapsed time.
    /// </summary>
    /// <param name="files">List of image file paths to process.</param>
    /// <returns>Total processing time in milliseconds.</returns>
    static long RecognizeBarcodes(List<string> files)
    {
        // Start total timer for the batch
        Stopwatch totalWatch = Stopwatch.StartNew();

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Open the image with BarCodeReader and read all supported types
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Measure time for this individual file
                    Stopwatch watch = Stopwatch.StartNew();
                    reader.ReadBarCodes();
                    watch.Stop();

                    Console.WriteLine($"File: {Path.GetFileName(file)} - Barcodes read: {reader.FoundCount}, Time: {watch.ElapsedMilliseconds} ms");
                    foreach (BarCodeResult result in reader.FoundBarCodes)
                    {
                        Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported file: {file}");
            }
        }

        totalWatch.Stop();
        return totalWatch.ElapsedMilliseconds;
    }
}