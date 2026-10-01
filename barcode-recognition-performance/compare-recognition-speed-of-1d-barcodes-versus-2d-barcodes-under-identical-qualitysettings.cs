// Title: Barcode Recognition Speed Comparison between 1D and 2D Symbologies
// Description: Demonstrates measuring and comparing the average read time of Code128 (1D) and QR (2D) barcodes using identical quality settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to create barcode images with BarcodeGenerator, read them with BarCodeReader, and apply QualitySettings for performance tuning. Typical use cases include bulk barcode processing, performance benchmarking, and selecting optimal symbologies for high‑throughput scanning scenarios. Developers often need to evaluate read speed across different symbologies while keeping decoding parameters consistent.
// Prompt: Compare recognition speed of 1D barcodes versus 2D barcodes under identical QualitySettings.
// Tags: barcode symbology, speed benchmark, recognition, 1d, 2d, aspose.barcode, generation, recognition, qualitysettings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates benchmarking the recognition speed of 1D versus 2D barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates barcodes, benchmarks reading speed, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data for 1D (Code128) and 2D (QR) barcodes
        var oneDTexts = new List<string> { "1234567890", "ABCDEFGHIJ", "9876543210", "CODE128TEST", "BARCODE1D" };
        var twoDTexts = new List<string> { "Hello World!", "https://example.com", "Aspose.BarCode", "QR Code Test", "12345ABCDE" };

        // Generate barcode images and collect file paths
        var oneDFiles = GenerateBarcodes(tempFolder, EncodeTypes.Code128, oneDTexts);
        var twoDFiles = GenerateBarcodes(tempFolder, EncodeTypes.QR, twoDTexts);

        // Benchmark reading speed for each set
        double oneDAvgMs = BenchmarkReading(oneDFiles);
        double twoDAvgMs = BenchmarkReading(twoDFiles);

        // Output average read times
        Console.WriteLine($"Average reading time for 1D barcodes (Code128): {oneDAvgMs:F2} ms");
        Console.WriteLine($"Average reading time for 2D barcodes (QR): {twoDAvgMs:F2} ms");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates barcode images for the specified texts and returns a list of file paths.
    /// </summary>
    /// <param name="folder">Folder where images will be saved.</param>
    /// <param name="encodeType">Symbology to use for generation.</param>
    /// <param name="texts">Collection of text strings to encode.</param>
    /// <returns>List of generated image file paths.</returns>
    static List<string> GenerateBarcodes(string folder, BaseEncodeType encodeType, List<string> texts)
    {
        var files = new List<string>();
        int index = 0;

        foreach (var text in texts)
        {
            // Build a unique file name for each barcode
            string filePath = Path.Combine(folder, $"barcode_{index}_{encodeType.TypeName}.png");

            // Create and save the barcode image
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Default settings automatically size the barcode to fit the text
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
            index++;
        }

        return files;
    }

    /// <summary>
    /// Measures the average time (in milliseconds) required to read a collection of barcode images.
    /// </summary>
    /// <param name="filePaths">List of barcode image file paths.</param>
    /// <returns>Average read time per image.</returns>
    static double BenchmarkReading(List<string> filePaths)
    {
        var totalMs = 0.0;
        int count = 0;

        foreach (var path in filePaths)
        {
            if (!File.Exists(path))
                continue;

            // Start timing the read operation
            var stopwatch = Stopwatch.StartNew();

            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Apply identical quality settings for all reads
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Perform the read
                var results = reader.ReadBarCodes();

                // Access result properties to prevent compiler optimizations from removing the call
                foreach (var result in results)
                {
                    var _ = result.CodeText;
                    var __ = result.CodeTypeName;
                }
            }

            stopwatch.Stop();
            totalMs += stopwatch.Elapsed.TotalMilliseconds;
            count++;
        }

        return count > 0 ? totalMs / count : 0.0;
    }
}