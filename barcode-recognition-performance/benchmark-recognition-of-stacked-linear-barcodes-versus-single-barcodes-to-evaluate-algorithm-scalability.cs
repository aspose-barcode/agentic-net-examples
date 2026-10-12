// Title: Benchmark Recognition of Single vs Stacked Linear Barcodes
// Description: Demonstrates generating Code128 and DataBarStacked barcodes, then measuring the time required to recognize each type using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator class for creating barcodes and the BarCodeReader class for decoding them. Typical use cases include performance testing, scalability evaluation, and automated quality checks where developers need to compare processing times of different barcode symbologies.
// Prompt: Benchmark recognition of stacked linear barcodes versus single barcodes to evaluate algorithm scalability.
// Tags: barcode symbology, performance, benchmark, generation, recognition, code128, databarstacked, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a simple benchmark that compares recognition times for single linear (Code128) and stacked linear (DataBarStacked) barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures recognition performance, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Prepare collections for single and stacked linear barcodes
        var singleBarcodes = new List<(string FilePath, string CodeText)>();
        var stackedBarcodes = new List<(string FilePath, string CodeText)>();

        // Generate sample single linear barcodes (Code128)
        for (int i = 0; i < 5; i++)
        {
            string codeText = $"CODE{i + 1}";
            string filePath = Path.Combine(tempDir, $"single_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            singleBarcodes.Add((filePath, codeText));
        }

        // Generate sample stacked linear barcodes (DataBarStacked) using a valid GTIN-14 payload
        string stackedPayload = "(01)01234567890128";
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempDir, $"stacked_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, stackedPayload))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            stackedBarcodes.Add((filePath, stackedPayload));
        }

        // Benchmark recognition of single linear barcodes
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (var (filePath, _) in singleBarcodes)
        {
            if (!File.Exists(filePath))
                continue;

            BaseDecodeType decodeType = DecodeType.Code128;
            using (var reader = new BarCodeReader(filePath, decodeType))
            {
                var results = reader.ReadBarCodes();
                // Iterate results to ensure the reader processes each image
                foreach (var result in results)
                {
                    Console.WriteLine($"Single: Detected {result.CodeTypeName} - {result.CodeText}");
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Single barcode recognition time for {singleBarcodes.Count} items: {stopwatch.ElapsedMilliseconds} ms");

        // Benchmark recognition of stacked linear barcodes
        stopwatch.Restart();
        foreach (var (filePath, _) in stackedBarcodes)
        {
            if (!File.Exists(filePath))
                continue;

            BaseDecodeType decodeType = DecodeType.DatabarStacked;
            using (var reader = new BarCodeReader(filePath, decodeType))
            {
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"Stacked: Detected {result.CodeTypeName} - {result.CodeText}");
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Stacked barcode recognition time for {stackedBarcodes.Count} items: {stopwatch.ElapsedMilliseconds} ms");

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}