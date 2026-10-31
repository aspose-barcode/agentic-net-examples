// Title: Multi‑threaded vs Single‑threaded Barcode Recognition Throughput
// Description: Demonstrates how to enable multi‑threaded barcode recognition using Aspose.BarCode and compares its processing time against a single‑threaded approach on a set of generated images.
// Category-Description: This example belongs to the Aspose.BarCode performance optimization category, showcasing the use of BarCodeReader.ProcessorSettings and ThreadPool adjustments to control CPU core utilization. Typical use cases include high‑volume scanning applications where developers need to maximize throughput by leveraging multi‑core processors while maintaining accurate barcode decoding across various symbologies.
// Prompt: Enable multi‑threaded recognition and compare throughput against single‑threaded execution on a set of images.
// Tags: barcode, recognition, multithreading, performance, aspose.barcode, generation, decoding

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates sample barcode images, runs barcode recognition in both single‑threaded and multi‑threaded modes,
/// and compares the elapsed time to illustrate performance differences.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode files, performs recognition,
    /// outputs timing results, and cleans up the temporary data.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images of various symbologies
        List<string> barcodeFiles = new List<string>();
        BaseEncodeType[] encodeTypes = new BaseEncodeType[]
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.Pdf417,
            EncodeTypes.DataMatrix,
            EncodeTypes.Aztec
        };

        for (int i = 0; i < encodeTypes.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            string codeText = $"Sample{i}";
            using (var generator = new BarcodeGenerator(encodeTypes[i], codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Single‑threaded recognition
        Console.WriteLine("Single‑threaded recognition:");
        long singleThreadTime = RunRecognition(barcodeFiles, useAllCores: false, coresCount: 1, maxAdditionalThreads: 0);
        Console.WriteLine($"Total time: {singleThreadTime} ms");
        Console.WriteLine();

        // Multi‑threaded recognition
        Console.WriteLine("Multi‑threaded recognition:");
        int allowedCores = Math.Max(2, Environment.ProcessorCount);
        long multiThreadTime = RunRecognition(
            barcodeFiles,
            useAllCores: true,
            coresCount: allowedCores,
            maxAdditionalThreads: Environment.ProcessorCount * 2);
        Console.WriteLine($"Total time: {multiThreadTime} ms");
        Console.WriteLine();

        // Cleanup temporary files and folder
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
    /// Performs barcode recognition on a list of image files using the specified processor settings.
    /// Returns the total elapsed time in milliseconds.
    /// </summary>
    /// <param name="files">Paths to barcode image files.</param>
    /// <param name="useAllCores">Whether to enable usage of all CPU cores.</param>
    /// <param name="coresCount">Number of cores to restrict processing to when not using all cores.</param>
    /// <param name="maxAdditionalThreads">Maximum number of additional threads allowed for processing.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long RunRecognition(List<string> files, bool useAllCores, int coresCount, int maxAdditionalThreads)
    {
        // Configure processor settings for the BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = coresCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxAdditionalThreads;

        // Adjust ThreadPool limits when multi‑threaded mode is enabled
        if (useAllCores)
        {
            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
            ThreadPool.SetMaxThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            ThreadPool.SetMinThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);
        }

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
                    BarCodeResult[] results = reader.ReadBarCodes();
                    totalFound += results?.Length ?? 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading '{Path.GetFileName(file)}': {ex.Message}");
                }
            }
        }

        watch.Stop();
        Console.WriteLine($"Barcodes found: {totalFound}");
        return watch.ElapsedMilliseconds;
    }
}