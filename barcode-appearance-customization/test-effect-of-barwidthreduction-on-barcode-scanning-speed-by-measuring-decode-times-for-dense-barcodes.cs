// Title: Effect of BarWidthReduction on Code128 Decode Speed
// Description: Demonstrates how changing the BarWidthReduction property influences the time required to decode a dense Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to generate high‑density barcodes with the BarcodeGenerator class, adjust the BarWidthReduction setting, and measure decoding performance using BarCodeReader with QualitySettings.HighPerformance. Developers working with barcode scanning optimization often need to tune these parameters to balance image size and scan speed.
// Prompt: Test the effect of BarWidthReduction on barcode scanning speed by measuring decode times for dense barcodes.
// Tags: code128, barwidthreduction, decode-time, performance, barcode-generation, barcode-recognition, aspnet, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the impact of the BarWidthReduction setting on barcode decoding speed for dense Code128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcodes with different BarWidthReduction values,
    /// measures their decode times, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarWidthReductionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Dense alphanumeric string to increase barcode complexity
        string codeText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        // Define file paths for the two test images
        string pathNoReduction = Path.Combine(tempFolder, "barcode_no_reduction.png");
        string pathWithReduction = Path.Combine(tempFolder, "barcode_with_reduction.png");

        // Generate barcodes: one without reduction and one with a reduction of 4 pixels
        GenerateBarcode(codeText, 0f, pathNoReduction);
        GenerateBarcode(codeText, 4f, pathWithReduction);

        // Measure and compare decode times for both images
        long timeNoReduction = MeasureDecodeTime(pathNoReduction);
        long timeWithReduction = MeasureDecodeTime(pathWithReduction);

        // Output the measured decode times
        Console.WriteLine($"Decode time (BarWidthReduction = 0): {timeNoReduction} ms");
        Console.WriteLine($"Decode time (BarWidthReduction = 4): {timeWithReduction} ms");

        // Clean up temporary files and folder
        try { File.Delete(pathNoReduction); } catch { }
        try { File.Delete(pathWithReduction); } catch { }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Generates a Code128 barcode image with the specified BarWidthReduction value.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="barWidthReductionPixels">Bar width reduction in pixels.</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    static void GenerateBarcode(string text, float barWidthReductionPixels, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            // Use a small XDimension to increase barcode density
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply the specified Bar Width Reduction
            generator.Parameters.Barcode.BarWidthReduction.Pixels = barWidthReductionPixels;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time required to decode a barcode image using the HighPerformance quality preset.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <returns>Elapsed time in milliseconds, or -1 if the file does not exist.</returns>
    static long MeasureDecodeTime(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return -1;
        }

        // Specify the expected barcode type for faster decoding
        BaseDecodeType decodeType = DecodeType.Code128;
        Stopwatch sw = new Stopwatch();

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Configure the reader for maximum performance
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Start timing, read barcodes, then stop timing
            sw.Start();
            var results = reader.ReadBarCodes();
            sw.Stop();

            // Output decoded values for verification (optional)
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded: {result.CodeText} (Type: {result.CodeTypeName})");
            }
        }

        return sw.ElapsedMilliseconds;
    }
}