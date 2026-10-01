// Title: Parallel Barcode Reading Using Multiple CPU Cores
// Description: Demonstrates how to generate sample barcode images and read them concurrently across all processor cores using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch processing collection, showcasing barcode generation (BarcodeGenerator) and recognition (BarCodeReader) with parallel execution. Developers often need to process large sets of images quickly; using ProcessorSettings with Parallel.ForEach enables high‑throughput scanning on multi‑core machines.
// Prompt: Parallelize barcode reading across multiple CPU cores by creating separate BarCodeReader instances for each image.
// Tags: barcode, parallel, multithreading, code128, generation, recognition, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a set of Code128 barcode images and reads them in parallel
/// using separate <see cref="BarCodeReader"/> instances for each file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for sample barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeParallel_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate a few sample barcode images (Code128) and collect their paths
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"sample_{i}.png");
            string codeText = $"Sample{i}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Enable parallel processing across all CPU cores
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        // Optionally limit to a specific core count:
        // BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // Read barcodes in parallel; each image gets its own BarCodeReader instance
        // --------------------------------------------------------------------
        Parallel.ForEach(imageFiles, file =>
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                return;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Evaluation version may modify CodeText, so just check for non‑empty result
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
        });

        // --------------------------------------------------------------------
        // Cleanup temporary files (optional)
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually reclaim the temp folder
        }
    }
}