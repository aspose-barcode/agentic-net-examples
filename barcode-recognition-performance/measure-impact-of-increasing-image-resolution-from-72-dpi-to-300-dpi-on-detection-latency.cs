// Title: Measure barcode detection latency at different image resolutions
// Description: Demonstrates how changing the image resolution from 72 DPI to 300 DPI affects the time required to detect a barcode. The example generates two PNG images at the specified DPI values and measures detection latency.
// Category-Description: This example belongs to the Aspose.BarCode image resolution and performance category. It shows how to use BarcodeGenerator to set image resolution, BarCodeReader to detect barcodes, and Stopwatch to benchmark detection time. Developers often need to evaluate trade‑offs between image quality and processing speed when optimizing barcode scanning applications.
// Prompt: Measure the impact of increasing image resolution from 72 DPI to 300 DPI on detection latency.
// Tags: barcode, code128, resolution, performance, detection latency, aspose.barcode, png, stopwatch

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates barcodes at different resolutions and measures the detection latency for each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary images at 72 DPI and 300 DPI,
    /// measures detection latency for each, outputs the results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "ResolutionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the two resolution variants
        string path72 = Path.Combine(tempDir, "barcode_72dpi.png");
        string path300 = Path.Combine(tempDir, "barcode_300dpi.png");

        // Generate barcode images at 72 DPI and 300 DPI
        GenerateBarcode(path72, 72f);
        GenerateBarcode(path300, 300f);

        // Measure detection latency for each image
        long latency72 = MeasureDetectionLatency(path72);
        long latency300 = MeasureDetectionLatency(path300);

        // Output the latency results
        Console.WriteLine($"Detection latency at 72 DPI: {latency72} ms");
        Console.WriteLine($"Detection latency at 300 DPI: {latency300} ms");

        // Cleanup temporary files and directory
        try { File.Delete(path72); } catch { }
        try { File.Delete(path300); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }

    /// <summary>
    /// Generates a Code128 barcode image with the specified resolution and saves it to the given path.
    /// </summary>
    /// <param name="filePath">Full file path where the barcode image will be saved.</param>
    /// <param name="resolution">Image resolution in DPI (dots per inch).</param>
    static void GenerateBarcode(string filePath, float resolution)
    {
        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the desired image resolution
            generator.Parameters.Resolution = resolution;

            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time required to detect barcodes in the specified image file.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <returns>Detection latency in milliseconds, or -1 if the file does not exist.</returns>
    static long MeasureDetectionLatency(string imagePath)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return -1;
        }

        // Initialize the barcode reader for the image
        using (var reader = new BarCodeReader(imagePath))
        {
            // Start timing the detection process
            Stopwatch sw = Stopwatch.StartNew();

            // Perform barcode detection
            var results = reader.ReadBarCodes();

            // Stop timing
            sw.Stop();

            // Optionally output each detected code text
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeText}");
            }

            // Return elapsed time in milliseconds
            return sw.ElapsedMilliseconds;
        }
    }
}