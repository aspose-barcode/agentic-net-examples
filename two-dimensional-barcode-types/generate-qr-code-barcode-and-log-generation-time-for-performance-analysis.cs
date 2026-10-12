// Title: Generate QR Code and Measure Generation Time
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG file, and logging the time taken for generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and QR-specific parameters such as XDimension and ErrorLevel. Typical scenarios include generating barcodes for web/mobile applications, batch processing, and performance benchmarking where developers need to measure generation speed.
// Prompt: Generate a QR Code barcode and log generation time for performance analysis.
// Tags: qr, barcode, generation, performance, png, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and logs the generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code, saves it as PNG, and outputs the elapsed time.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary output directory and ensure it exists.
        string outputDirectory = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDirectory);

        // Build the full path for the generated QR Code image.
        string outputPath = Path.Combine(outputDirectory, "qr.png");

        // Text to encode in the QR Code.
        string codeText = "Performance test QR code";

        // Start measuring the generation time.
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Create and configure the QR Code generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of a single QR module (X dimension) in points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Choose the error correction level (Level M provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop the timer after generation completes.
        stopwatch.Stop();

        // Output the location of the saved image and the elapsed time.
        Console.WriteLine($"QR Code saved to: {outputPath}");
        Console.WriteLine($"Generation time: {stopwatch.ElapsedMilliseconds} ms");
    }
}