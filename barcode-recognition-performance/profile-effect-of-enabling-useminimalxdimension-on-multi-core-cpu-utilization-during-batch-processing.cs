// Title: Demonstrate batch barcode recognition with and without UseMinimalXDimension
// Description: This example generates a set of Code128 barcode images, then processes them in a batch using Aspose.BarCode's multithreaded reader, comparing normal XDimension mode to the UseMinimalXDimension setting.
// Category-Description: Shows how to use Aspose.BarCode's BarCodeReader with multithreaded processor settings for high‑performance batch decoding. It covers configuring processor cores, adjusting XDimension quality settings, and measuring recognition time—common tasks for developers building bulk barcode scanning solutions. Suitable for searches about Aspose.BarCode batch processing, multithreading, and XDimension optimization.
// Prompt: Profile the effect of enabling UseMinimalXDimension on multi‑core CPU utilization during batch processing.
// Tags: barcode, code128, batch processing, multithreading, xdimension, useminimalxdimension, aspose.barcode, performance

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch barcode generation and recognition, comparing normal XDimension mode with UseMinimalXDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, configures multithreaded processing, runs two recognition batches, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images
        List<string> files = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = "Sample" + i.ToString("D2");
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            GenerateBarcode(filePath, codeText);
            files.Add(filePath);
        }

        // Configure multithreaded processor settings (use all cores)
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Run batch without UseMinimalXDimension (default Normal mode)
        Console.WriteLine("Batch processing with Normal XDimension:");
        RunBatch(files, useMinimal: false);

        // Run batch with UseMinimalXDimension enabled
        Console.WriteLine();
        Console.WriteLine("Batch processing with UseMinimalXDimension:");
        RunBatch(files, useMinimal: true);

        // Cleanup temporary folder
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates a Code128 barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="path">File path where the barcode image will be saved.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    static void GenerateBarcode(string path, string codeText)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Processes a list of barcode image files, optionally using the minimal XDimension mode, and reports timing.
    /// </summary>
    /// <param name="files">Collection of image file paths to decode.</param>
    /// <param name="useMinimal">If true, enables UseMinimalXDimension; otherwise uses Normal mode.</param>
    static void RunBatch(List<string> files, bool useMinimal)
    {
        Stopwatch watch = Stopwatch.StartNew();

        int totalFound = 0;
        foreach (string file in files)
        {
            // Verify the file exists before attempting to read
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
                    // Apply XDimension quality settings based on the flag
                    if (useMinimal)
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
                    totalFound += results.Length;

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
        Console.WriteLine($"Total barcodes read: {totalFound}");
        Console.WriteLine($"Recognition time: {watch.ElapsedMilliseconds} ms");
    }
}