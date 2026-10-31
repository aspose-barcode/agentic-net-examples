// Title: Multithreaded Mailmark Barcode Decoding Example
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader for multithreaded processing and decoding a batch of Mailmark barcode images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on performance optimization using the BarCodeReader class. It shows how to enable all CPU cores, adjust thread‑pool limits, generate sample Mailmark barcodes, and read them efficiently. Developers working with large volumes of barcode images can use these techniques to accelerate decoding in batch processing scenarios.
// Prompt: Configure BarCodeReader for multi‑threaded processing to accelerate decoding of large Mailmark image batches.
// Tags: mailmark, barcode, multithreading, reader, aspose.barcode, image, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Sample program that configures multithreaded barcode reading,
/// generates a set of Mailmark barcode images, and decodes them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Configure BarCodeReader to use all available CPU cores.
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // ------------------------------------------------------------
        // Optionally adjust the .NET ThreadPool limits to match the core count.
        // ------------------------------------------------------------
        int workerThreads, completionPortThreads;
        ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMaxThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(Math.Max(Environment.ProcessorCount * 4, workerThreads), completionPortThreads);

        // ------------------------------------------------------------
        // Create a temporary folder to store the generated Mailmark images.
        // ------------------------------------------------------------
        string batchFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // ------------------------------------------------------------
        // Generate a small set of Mailmark barcode images.
        // ------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            string imagePath = Path.Combine(batchFolder, $"mailmark_{i}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(imagePath);
        }

        // ------------------------------------------------------------
        // Decode each generated image using BarCodeReader.
        // ------------------------------------------------------------
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.Mailmark))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)} (Mailmark reading not supported).");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image '{Path.GetFileName(file)}': {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files (optional).
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}