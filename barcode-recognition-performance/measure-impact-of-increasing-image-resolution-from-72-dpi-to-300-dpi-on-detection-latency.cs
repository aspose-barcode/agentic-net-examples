// Title: Impact of Image Resolution on Barcode Detection Latency
// Description: Demonstrates how changing the image DPI from 72 to 300 affects the time required to read a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to evaluate performance trade‑offs such as image resolution versus detection speed when integrating barcode scanning into applications.
// Prompt: Measure the impact of increasing image resolution from 72 DPI to 300 DPI on detection latency.
// Tags: code128, barcode, resolution, latency, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring barcode read latency at different image resolutions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes at 72 DPI and 300 DPI, measures read latency, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeResolutionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the two resolution variants
        string path72 = Path.Combine(tempDir, "barcode_72.png");
        string path300 = Path.Combine(tempDir, "barcode_300.png");

        try
        {
            // Generate barcodes at 72 DPI and 300 DPI
            GenerateBarcode(path72, 72f);
            GenerateBarcode(path300, 300f);

            // Measure and capture read latency for each image
            long latency72 = MeasureReadLatency(path72);
            long latency300 = MeasureReadLatency(path300);

            // Output the latency results
            Console.WriteLine($"Read latency at 72 DPI: {latency72} ms");
            Console.WriteLine($"Read latency at 300 DPI: {latency300} ms");
        }
        finally
        {
            // Clean up generated files and temporary directory
            if (File.Exists(path72)) File.Delete(path72);
            if (File.Exists(path300)) File.Delete(path300);
            if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
        }
    }

    /// <summary>
    /// Generates a Code128 barcode image at the specified resolution.
    /// </summary>
    /// <param name="filePath">Full path where the barcode image will be saved.</param>
    /// <param name="resolutionDpi">Image resolution in dots per inch.</param>
    static void GenerateBarcode(string filePath, float resolutionDpi)
    {
        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the desired image resolution (DPI)
            generator.Parameters.Resolution = resolutionDpi;

            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time required to read the first barcode from the specified image.
    /// </summary>
    /// <param name="filePath">Path to the barcode image file.</param>
    /// <returns>Elapsed time in milliseconds, or -1 if the file does not exist.</returns>
    static long MeasureReadLatency(string filePath)
    {
        // Verify that the image file exists before attempting to read
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return -1;
        }

        // Specify the expected barcode type for faster decoding
        BaseDecodeType decodeType = DecodeType.Code128;
        Stopwatch sw = new Stopwatch();

        // Open the barcode reader and measure the time to read the first result
        using (var reader = new BarCodeReader(filePath, decodeType))
        {
            sw.Start();
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Access the result to ensure the decoding process occurs
                string codeText = result.CodeText;
                break; // Only need the first barcode result
            }
            sw.Stop();
        }

        // Return the elapsed time in milliseconds
        return sw.ElapsedMilliseconds;
    }
}