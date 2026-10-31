// Title: Impact of Image Resolution on Barcode Detection Latency
// Description: Demonstrates how changing the DPI of a generated barcode image from 72 to 300 affects the time required to read the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating Code128 barcodes, setting image resolution via Parameters.Resolution, and BarCodeReader for decoding. Developers often need to balance image quality against processing speed when optimizing barcode scanning workflows in desktop or web applications.
// Prompt: Measure the impact of increasing image resolution from 72 DPI to 300 DPI on detection latency.
// Tags: code128, barcode, resolution, detection-latency, generation, recognition, aspose.barcode, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Executes a performance comparison of barcode detection at two different image resolutions (72 DPI vs 300 DPI).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes at two resolutions, measures read times, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeResolutionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the 72 DPI and 300 DPI barcode images.
        string path72 = Path.Combine(tempDir, "barcode_72.png");
        string path300 = Path.Combine(tempDir, "barcode_300.png");

        // Generate barcode images at the specified resolutions.
        GenerateBarcode(path72, 72f);
        GenerateBarcode(path300, 300f);

        // Measure the time required to read each barcode image.
        long time72 = MeasureReadTime(path72);
        long time300 = MeasureReadTime(path300);

        // Output the measured read times.
        Console.WriteLine($"Read time at 72 DPI: {time72} ms");
        Console.WriteLine($"Read time at 300 DPI: {time300} ms");

        // Cleanup temporary files and directory.
        try { File.Delete(path72); } catch { }
        try { File.Delete(path300); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }

    /// <summary>
    /// Generates a Code128 barcode image with the specified resolution and saves it to the given file path.
    /// </summary>
    /// <param name="filePath">Full path where the barcode image will be saved.</param>
    /// <param name="resolution">Image resolution in DPI (dots per inch).</param>
    static void GenerateBarcode(string filePath, float resolution)
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the desired image resolution.
            generator.Parameters.Resolution = resolution;

            // Optional: keep module size consistent across resolutions.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time taken to read a barcode from an image file.
    /// </summary>
    /// <param name="filePath">Full path to the barcode image file.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasureReadTime(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Barcode image not found.", filePath);

        // Initialize the barcode reader for Code128 symbology.
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Start timing the read operation.
            Stopwatch sw = Stopwatch.StartNew();

            // Perform the barcode detection.
            var results = reader.ReadBarCodes();

            // Stop timing.
            sw.Stop();

            // Output decoded text for verification (optional).
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded: {result.CodeText}");
            }

            // Return the elapsed time in milliseconds.
            return sw.ElapsedMilliseconds;
        }
    }
}