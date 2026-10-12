// Title: Multi‑threaded Mailmark barcode decoding using BarCodeReader
// Description: Demonstrates generating a batch of Mailmark barcode images and decoding them with BarCodeReader configured for multi‑threaded processing to speed up large‑scale image analysis.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases key API classes like ComplexBarcodeGenerator, MailmarkCodetext, BarCodeReader, and ProcessorSettings, illustrating typical use cases where developers need to process many barcode images efficiently, often leveraging multi‑core CPUs for faster decoding.
// Prompt: Configure BarCodeReader for multi‑threaded processing to accelerate decoding of large Mailmark image batches.
// Tags: mailmark, decoding, multithreading, console, aspose.barcode, aspose.barcode.recognition, aspose.barcode.generation, aspose.barcode.complexbarcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates multi‑threaded decoding of Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample Mailmark images, configures the reader for multi‑threaded processing,
    /// decodes each image, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate a small set of Mailmark barcode images
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

            string filePath = Path.Combine(batchFolder, $"mailmark_{i}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Configure multithreaded processing for BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Process each generated image
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.Mailmark))
            {
                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                Console.WriteLine($"File: {Path.GetFileName(file)} - Detected: {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }

                // Note: Mailmark 4‑state recognition is not supported; zero results are expected.
            }
        }

        // Clean up temporary files (optional)
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
            // Ignore any cleanup errors
        }
    }
}