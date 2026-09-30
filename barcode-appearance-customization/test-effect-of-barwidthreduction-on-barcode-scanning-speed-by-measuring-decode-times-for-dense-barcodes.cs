// Title: BarWidthReduction Impact on Barcode Decode Speed
// Description: Demonstrates how adjusting BarWidthReduction influences the time required to decode a dense Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on the BarWidthReduction property. Developers often need to fine‑tune barcode dimensions to balance readability and scanning performance, especially for high‑density data.
// Prompt: Test the effect of BarWidthReduction on barcode scanning speed by measuring decode times for dense barcodes.
// Tags: barcode, code128, barwidthreduction, decode-time, performance, generation, recognition, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates the effect of BarWidthReduction on barcode decoding speed for dense Code128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcodes (with and without BarWidthReduction),
    /// measures their decode times, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Prepare a dense Code128 barcode text consisting of 100 numeric characters.
        string codeText = new string('1', 100);

        // Create a unique temporary folder for generated images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarWidthReductionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the two test images.
        string pathNoReduction = Path.Combine(tempFolder, "barcode_no_reduction.png");
        string pathWithReduction = Path.Combine(tempFolder, "barcode_with_reduction.png");

        // Generate barcode without BarWidthReduction (default value 0).
        GenerateBarcode(pathNoReduction, codeText, 0f);

        // Generate barcode with a small BarWidthReduction (e.g., 0.5 points).
        GenerateBarcode(pathWithReduction, codeText, 0.5f);

        // Measure decode times for both images.
        double timeNoReduction = MeasureDecodeTime(pathNoReduction);
        double timeWithReduction = MeasureDecodeTime(pathWithReduction);

        // Output the measured decode times.
        Console.WriteLine($"Decode time without BarWidthReduction: {timeNoReduction:F2} ms");
        Console.WriteLine($"Decode time with BarWidthReduction (0.5 pt): {timeWithReduction:F2} ms");

        // Clean up temporary files and folder (best‑effort, ignore any errors).
        try
        {
            if (File.Exists(pathNoReduction)) File.Delete(pathNoReduction);
            if (File.Exists(pathWithReduction)) File.Delete(pathWithReduction);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup is best‑effort.
        }
    }

    /// <summary>
    /// Generates a barcode image with the specified BarWidthReduction.
    /// </summary>
    /// <param name="filePath">Full path where the barcode image will be saved.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="reductionPoints">BarWidthReduction value in points.</param>
    static void GenerateBarcode(string filePath, string codeText, float reductionPoints)
    {
        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the BarWidthReduction using the Point unit.
            generator.Parameters.Barcode.BarWidthReduction.Point = reductionPoints;

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time taken to decode a barcode image.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <returns>Elapsed time in milliseconds, or -1 if the file is missing.</returns>
    static double MeasureDecodeTime(string imagePath)
    {
        // Verify that the image file exists before attempting to decode.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return -1;
        }

        var stopwatch = new Stopwatch();

        // Initialize the barcode reader for all supported types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Start timing, read barcodes, then stop timing.
            stopwatch.Start();
            BarCodeResult[] results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Report if no barcode was detected.
            if (results.Length == 0)
            {
                Console.WriteLine($"No barcode detected in {imagePath}");
            }
        }

        // Return the elapsed time in milliseconds.
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}