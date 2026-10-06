// Title: Batch barcode generation and recognition with UseMinimalXDimension profiling
// Description: Demonstrates generating a set of Code128 barcodes, then reading them in a batch while optionally enabling UseMinimalXDimension to compare processing time.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create barcodes with BarcodeGenerator, read them with BarCodeReader, and configure multithreaded processing via ProcessorSettings. Typical use cases include high‑volume scanning, performance benchmarking, and optimizing image decoding settings for different symbologies. Developers often need to adjust X‑dimension handling and CPU core utilization to meet throughput requirements.
/// Prompt: Profile the effect of enabling UseMinimalXDimension on multi‑core CPU utilization during batch processing.
// Tags: barcode symbology, generation, recognition, batch processing, multithreading, usedimension, code128, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes and measures the impact of the
/// UseMinimalXDimension setting on multi‑core processing performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, then processes them twice:
    /// once with default X‑dimension handling and once with UseMinimalXDimension enabled.
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
            string codeText = $"CODE{i:D3}";
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set barcode color to black
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                // Save as PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Process batch without UseMinimalXDimension (default Normal mode)
        ProcessBatch(barcodeFiles, useMinimalXDimension: false);

        // Process batch with UseMinimalXDimension enabled
        ProcessBatch(barcodeFiles, useMinimalXDimension: true);
    }

    /// <summary>
    /// Reads a collection of barcode images, optionally enabling UseMinimalXDimension,
    /// and measures the elapsed time and total barcodes detected.
    /// </summary>
    /// <param name="files">List of image file paths to process.</param>
    /// <param name="useMinimalXDimension">If true, configures the reader to use minimal X‑dimension mode.</param>
    static void ProcessBatch(List<string> files, bool useMinimalXDimension)
    {
        // Configure multithreaded processor settings to use all available cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        Stopwatch stopwatch = Stopwatch.StartNew();
        int totalDetected = 0;

        // Iterate through each barcode image file
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Apply X‑dimension settings based on the flag
                if (useMinimalXDimension)
                {
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = 1f;
                }
                else
                {
                    reader.QualitySettings.XDimension = XDimensionMode.Normal;
                }

                try
                {
                    // Read all barcodes in the current image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    totalDetected += results.Length;
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    // Log a warning and continue with the next file
                    Console.WriteLine($"Warning: Skipping unreadable file '{file}'.");
                }
            }
        }

        stopwatch.Stop();
        // Output performance summary
        Console.WriteLine($"{(useMinimalXDimension ? "UseMinimalXDimension" : "Default")} - Time: {stopwatch.ElapsedMilliseconds} ms, Barcodes detected: {totalDetected}");
    }
}