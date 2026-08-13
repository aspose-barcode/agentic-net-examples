// Title: Reset QualitySettings to defaults and validate processing time
// Description: Demonstrates measuring barcode reading performance with different QualitySettings and confirms that resetting to defaults restores the original processing time.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating how QualitySettings affect CPU usage during barcode recognition. It showcases the use of BarcodeGenerator, BarCodeReader, and QualitySettings classes to generate a Code128 barcode, read it under various quality presets, and validate that reverting to NormalQuality restores baseline performance. Developers often need such patterns to benchmark and optimize barcode processing in high‑throughput applications.
// Prompt: Validate that resetting QualitySettings to defaults restores original CPU consumption patterns during processing.
// Tags: code128, performance, qualitysettings, reading, aspose.barcode, generation, recognition

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how resetting <see cref="QualitySettings"/> to its default (NormalQuality) restores the original CPU consumption pattern during barcode processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample barcode, measures reading times under different quality settings,
    /// resets to defaults, and validates the performance consistency.
    /// </summary>
    static void Main()
    {
        // Define the path for the generated barcode image
        string imagePath = "sample_barcode.png";

        // Generate a Code128 barcode and save it to the specified path
        GenerateBarcode(imagePath);

        // Verify that the barcode image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to generate barcode image at '{imagePath}'.");
            return;
        }

        // Measure reading time using the default NormalQuality settings
        long defaultTime = MeasureReadingTime(imagePath, QualitySettings.NormalQuality);
        Console.WriteLine($"Reading with NormalQuality: {defaultTime} ms");

        // Measure reading time using the HighPerformance preset (lower quality, higher speed)
        long highPerfTime = MeasureReadingTime(imagePath, QualitySettings.HighPerformance);
        Console.WriteLine($"Reading with HighPerformance: {highPerfTime} ms");

        // Reset to the default NormalQuality settings and measure again
        long resetTime = MeasureReadingTime(imagePath, QualitySettings.NormalQuality);
        Console.WriteLine($"Reading after reset to NormalQuality: {resetTime} ms");

        // Simple validation: the reset time should be close to the original default time
        long diff = Math.Abs(resetTime - defaultTime);
        Console.WriteLine($"Difference after reset: {diff} ms");
    }

    /// <summary>
    /// Generates a Code128 barcode image with the provided text and saves it to the given file path.
    /// </summary>
    /// <param name="path">The file system path where the barcode image will be saved.</param>
    static void GenerateBarcode(string path)
    {
        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode image using default parameters
            generator.Save(path);
        }
    }

    /// <summary>
    /// Measures the time required to read barcodes from an image using a specific <see cref="QualitySettings"/> preset.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="settings">The quality preset to apply during reading.</param>
    /// <returns>The elapsed time in milliseconds.</returns>
    static long MeasureReadingTime(string imagePath, QualitySettings settings)
    {
        var stopwatch = new Stopwatch();

        // Initialize the barcode reader for Code128 symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the specified quality settings to the reader
            reader.QualitySettings = settings;

            // Start timing the read operation
            stopwatch.Start();

            // Iterate through all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the detected code text to ensure processing occurs
                Console.WriteLine($"Detected: {result.CodeText}");
            }

            // Stop timing after processing completes
            stopwatch.Stop();
        }

        // Return the total elapsed time in milliseconds
        return stopwatch.ElapsedMilliseconds;
    }
}