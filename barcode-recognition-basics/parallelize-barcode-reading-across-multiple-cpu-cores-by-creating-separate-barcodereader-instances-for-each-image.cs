// Title: Parallel Barcode Generation and Multi‑Core Recognition Example
// Description: This example creates barcode images of various symbologies, then reads them concurrently across all CPU cores to showcase high‑performance scanning.
// Category-Description: Part of the Aspose.BarCode suite, this sample illustrates combined use of BarcodeGenerator for image creation and BarCodeReader for recognition. It focuses on multithreaded processing, configuring ProcessorSettings and ThreadPool to maximize throughput—common when handling large batches of images in enterprise scanning or inventory systems.
// Prompt: Parallelize barcode reading across multiple CPU cores by creating separate BarCodeReader instances for each image.
// Tags: barcode, symbology, generation, recognition, parallel, multithreading, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates parallel barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, configures multithreaded processing, reads barcodes in parallel, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data: a list of (symbology, text) tuples
        var samples = new List<(BaseEncodeType encode, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Pdf417, "PDF417 Sample"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Aztec, "AztecCode")
        };

        var generatedFiles = new List<string>();

        // Generate barcode images and store their file paths
        foreach (var (encode, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encode.TypeName}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Configure Aspose.BarCode to use all available CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Optionally adjust the .NET ThreadPool to provide enough worker threads
        ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
        ThreadPool.SetMaxThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);

        var stopwatch = Stopwatch.StartNew();

        // Set up parallel options to limit degree of parallelism to the number of processors
        ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Perform barcode reading in parallel, creating a separate BarCodeReader for each image
        Parallel.ForEach(generatedFiles, options, filePath =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image '{filePath}': {ex.Message}");
            }
        });

        stopwatch.Stop();
        Console.WriteLine($"Total recognition time: {stopwatch.ElapsedMilliseconds} ms");

        // Clean up generated barcode image files
        foreach (var file in generatedFiles)
        {
            try { File.Delete(file); } catch { }
        }

        // Remove the temporary folder
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}