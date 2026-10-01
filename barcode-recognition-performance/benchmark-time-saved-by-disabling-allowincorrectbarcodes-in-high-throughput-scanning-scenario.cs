// Title: Benchmarking barcode reading performance with AllowIncorrectBarcodes disabled
// Description: Demonstrates how disabling AllowIncorrectBarcodes impacts the time required to read a set of Code128 barcodes in a high‑throughput scenario.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, showcasing the use of BarcodeGenerator for image creation and BarCodeReader with QualitySettings to control validation. Developers often need to measure the effect of strict barcode validation on processing speed when handling large volumes of scans.
// Prompt: Benchmark the time saved by disabling AllowIncorrectBarcodes in a high‑throughput scanning scenario.
// Tags: barcode symbology, generation, recognition, performance, benchmark, allowincorrectbarcodes, png, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a benchmark that compares barcode reading times with and without allowing incorrect barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates sample Code128 barcodes, measures read performance, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample code texts to encode
        var codeTexts = new List<string> { "1234567890", "ABCDEFGHIJ", "9876543210", "ZXCVBNMASD", "QWERTYUIOP" };
        var barcodeFiles = new List<string>();

        // Generate PNG barcode images for each sample text
        foreach (var text in codeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Save the barcode directly as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Benchmark reading without allowing incorrect barcodes
        long timeWithoutAllow = BenchmarkReading(barcodeFiles, allowIncorrect: false);
        // Benchmark reading while allowing incorrect barcodes
        long timeWithAllow = BenchmarkReading(barcodeFiles, allowIncorrect: true);

        // Output the measured times
        Console.WriteLine($"Reading time without AllowIncorrectBarcodes: {timeWithoutAllow} ms");
        Console.WriteLine($"Reading time with AllowIncorrectBarcodes:    {timeWithAllow} ms");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors
        }
    }

    // Performs repeated reading of the provided barcode files and returns elapsed milliseconds
    static long BenchmarkReading(List<string> files, bool allowIncorrect)
    {
        const int readsPerFile = 200; // Number of reads per file to simulate high‑throughput
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (var file in files)
        {
            if (!File.Exists(file))
                continue; // Skip missing files gracefully

            for (int i = 0; i < readsPerFile; i++)
            {
                try
                {
                    using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                    {
                        // Configure whether to allow incorrect barcodes during decoding
                        reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                        // Force decoding; results are not processed further
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // No operation needed; iteration ensures decoding occurs
                        }
                    }
                }
                catch (ArgumentException)
                {
                    // Image loading failed; skip this iteration
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}