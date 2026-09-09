// Title: Impact of Large Image Dimensions on Barcode Recognition with HighPerformance Settings
// Description: Demonstrates generating a 4000x4000 PNG barcode and measuring the recognition time using the HighPerformance quality preset.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating high‑resolution barcodes and BarCodeReader with QualitySettings to profile performance. Developers often need to evaluate how image size affects decoding speed, especially when processing large scans in high‑throughput scenarios.
// Prompt: Profile the impact of large image dimensions (e.g., 4000x4000) on recognition time with HighPerformance preset.
// Tags: code128, performance, highperformance, generation, recognition, png, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a large barcode image and measures the time required to recognize it
/// using the HighPerformance quality preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a 4000x4000 barcode, reads it, and outputs timing information.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure image size, resolution, and colors
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 4000f;
            generator.Parameters.ImageHeight.Pixels = 4000f;
            generator.Parameters.Resolution = 300f;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode to a memory stream in PNG format
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading

                // Initialize the barcode reader with all supported decode types
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Apply the HighPerformance preset to prioritize speed
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Measure the time taken to read barcodes
                    var stopwatch = Stopwatch.StartNew();
                    BarCodeResult[] results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    // Output performance metrics and detected barcode information
                    Console.WriteLine($"Recognition time (HighPerformance): {stopwatch.ElapsedMilliseconds} ms");
                    Console.WriteLine($"Barcodes detected: {results.Length}");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}