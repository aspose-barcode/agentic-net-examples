// Title: Demonstrate barcode batch processing with and without UseMinimalXDimension
// Description: This example generates a set of Code128 barcode images, then reads them in a multithreaded batch, comparing normal X‑dimension mode to the minimal X‑dimension mode.
// Category-Description: Shows Aspose.BarCode batch recognition techniques, focusing on ProcessorSettings for multi‑core utilization and QualitySettings.XDimension options. Developers working with high‑volume barcode scanning can learn how to configure threading and X‑dimension modes to optimize performance and accuracy.
// Prompt: Profile the effect of enabling UseMinimalXDimension on multi‑core CPU utilization during batch processing.
// Tags: barcode, code128, batch processing, multithreading, useminimalxdimension, qualitysettings, processorsettings, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point and helper methods for demonstrating batch barcode recognition
/// with different X‑dimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcode images, configures multithreaded processing, and runs
    /// recognition twice—once with normal X‑dimension mode and once with minimal X‑dimension mode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            string codeText = $"Test{i}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save each barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Ensure multithreaded processing is enabled
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Process batch without UseMinimalXDimension (Normal mode)
        Console.WriteLine("Processing batch with XDimensionMode.Normal");
        ProcessBatch(barcodeFiles, useMinimalXDimension: false);

        // Process batch with UseMinimalXDimension
        Console.WriteLine("Processing batch with XDimensionMode.UseMinimalXDimension");
        ProcessBatch(barcodeFiles, useMinimalXDimension: true);

        // Cleanup temporary folder
        try
        {
            foreach (var file in barcodeFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Reads a collection of barcode image files using the specified X‑dimension mode
    /// and reports recognition results and timing.
    /// </summary>
    /// <param name="files">List of image file paths to process.</param>
    /// <param name="useMinimalXDimension">If true, enables XDimensionMode.UseMinimalXDimension; otherwise uses Normal mode.</param>
    static void ProcessBatch(List<string> files, bool useMinimalXDimension)
    {
        int totalBarcodes = 0;
        Stopwatch watch = Stopwatch.StartNew();

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize reader for Code128 barcodes
                using (var reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Configure X‑dimension mode based on the flag
                    if (useMinimalXDimension)
                    {
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = 1f;
                    }
                    else
                    {
                        reader.QualitySettings.XDimension = XDimensionMode.Normal;
                    }

                    // Perform recognition
                    BarCodeResult[] results = reader.ReadBarCodes();
                    totalBarcodes += results.Length;

                    // Output each recognized barcode
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle files that cannot be loaded as barcodes
                Console.WriteLine($"Skipping file due to load error: {ex.Message}");
            }
        }

        watch.Stop();
        Console.WriteLine($"Total barcodes read: {totalBarcodes}");
        Console.WriteLine($"Recognition time: {watch.ElapsedMilliseconds} ms");
        Console.WriteLine();
    }
}