// Title: Benchmarking UseMinimalXDimension Impact on Multi‑Core Barcode Decoding
// Description: Demonstrates how enabling the UseMinimalXDimension setting affects batch barcode decoding performance on multi‑core CPUs.
// Category-Description: This example belongs to the Aspose.BarCode performance profiling category, illustrating the use of BarCodeReader.ProcessorSettings and QualitySettings to control CPU utilization and decoding accuracy. Developers often need to benchmark different decoding configurations, such as X‑dimension modes, to optimize throughput in high‑volume scanning scenarios. The snippet shows generating sample Code128 barcodes, configuring multi‑core processing, and measuring elapsed time for batch reads.
// Prompt: Profile the effect of enabling UseMinimalXDimension on multi‑core CPU utilization during batch processing.
// Tags: code128, performance, batch-processing, multithreading, useminimalxdimension, barcodegenerator, barcodereader, qualitysettings, processorsettings

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates profiling the effect of UseMinimalXDimension on multi‑core CPU utilization during batch barcode processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes, configures processor settings, runs benchmarks, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (5 PNG files)
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder, 5);

        // Ensure the processor uses all available cores for the benchmark
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Run batch processing without UseMinimalXDimension and record elapsed time
        long elapsedDefault = ProcessBatch(barcodeFiles, useMinimalXDimension: false);
        Console.WriteLine($"Batch processing without UseMinimalXDimension: {elapsedDefault} ms");

        // Run batch processing with UseMinimalXDimension enabled and record elapsed time
        long elapsedMinimal = ProcessBatch(barcodeFiles, useMinimalXDimension: true);
        Console.WriteLine($"Batch processing with UseMinimalXDimension: {elapsedMinimal} ms");

        // Clean up temporary files
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { /* ignore */ }
        }
        try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }
    }

    // Generates a specified number of barcode PNG files and returns their file paths
    private static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string codeText = $"Sample{i + 1}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");

            // Create a Code128 barcode with a modest XDimension for consistency
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.XDimension.Point = 0.5f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }
        return files;
    }

    // Processes a batch of barcode images and returns the elapsed time in milliseconds
    private static long ProcessBatch(List<string> files, bool useMinimalXDimension)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Use Code128 as the decode type for this example
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Apply the XDimension mode if requested
                if (useMinimalXDimension)
                {
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                }

                // Read all barcodes in the image (there will be one per file)
                try
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Demonstrate access to decoded data; output can be suppressed in real benchmarks
                        Console.WriteLine($"Decoded: {result.CodeText} ({result.CodeTypeName})");
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    Console.WriteLine($"Skipping unreadable file: {file}");
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}