// Title: Multi‑threaded Mailmark barcode decoding example
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader for multi‑threaded processing to speed up decoding of a batch of Mailmark images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on performance optimization using the BarCodeReader processor settings and .NET thread pool. It shows how to generate Mailmark barcodes, adjust processor cores, and decode them concurrently, a common requirement for high‑volume mail processing systems.
// Prompt: Configure BarCodeReader for multi‑threaded processing to accelerate decoding of large Mailmark image batches.
// Tags: mailmark, barcode, multithreading, performance, aspnet, aspose.barcode, decoding, threadpool

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a set of Mailmark barcodes, configuring the
/// <see cref="BarCodeReader"/> for multi‑threaded processing, and decoding the
/// images while measuring performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary Mailmark images,
    /// configures processor settings, decodes each image, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary folder for the batch of images.
        // --------------------------------------------------------------------
        string batchFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // --------------------------------------------------------------------
        // 2. Generate sample Mailmark images and collect their file paths.
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(batchFolder, $"mailmark_{i}.png");
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "1",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save each barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 3. Configure BarCodeReader for multi‑threaded processing.
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // --------------------------------------------------------------------
        // 4. Optionally increase ThreadPool limits to provide more worker threads.
        // --------------------------------------------------------------------
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxIO);
        ThreadPool.SetMaxThreads(Math.Max(Environment.ProcessorCount * 4, maxWorker), maxIO);
        ThreadPool.GetMinThreads(out int minWorker, out int minIO);
        ThreadPool.SetMinThreads(Math.Max(Environment.ProcessorCount * 4, minWorker), minIO);

        // --------------------------------------------------------------------
        // 5. Decode each image using BarCodeReader and report results.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Mailmark;
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, decodeType))
                {
                    Stopwatch sw = Stopwatch.StartNew();          // Start timing
                    var results = reader.ReadBarCodes();          // Perform decoding
                    sw.Stop();                                    // Stop timing

                    Console.WriteLine($"File: {Path.GetFileName(file)} - Detected: {results.Length} barcodes in {sw.ElapsedMilliseconds} ms");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image '{file}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // 6. Cleanup temporary files and folder (optional).
        // --------------------------------------------------------------------
        try
        {
            foreach (var file in imageFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(batchFolder);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}