// Title: Generate QR Code and Measure Generation Time
// Description: Creates a QR Code barcode, saves it as a PNG file, and logs the time taken to generate the image.
// Category-Description: This example demonstrates basic barcode generation using Aspose.BarCode. It showcases the BarcodeGenerator class with EncodeTypes.QR, configuring QR error correction, and measuring performance via System.Diagnostics.Stopwatch. Typical scenarios include creating QR codes for URLs, product information, or authentication tokens where developers need quick generation and timing metrics.
// Prompt: Generate a QR Code barcode and log generation time for performance analysis.
// Tags: qr code, barcode generation, performance analysis, png, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a QR Code barcode, save it as PNG, and log the generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, measures the time taken, and writes the results to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_" + Guid.NewGuid().ToString("N") + ".png");

        // Initialize a QR Code generator using the BarcodeGenerator class.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data that the QR Code will encode.
            generator.CodeText = "https://example.com";

            // Optional: configure a high error‑correction level for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Start measuring the time required to generate and save the barcode.
            Stopwatch sw = Stopwatch.StartNew();

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Stop the timer once the save operation completes.
            sw.Stop();

            // Output the location of the saved file and the elapsed generation time.
            Console.WriteLine($"QR Code saved to: {outputPath}");
            Console.WriteLine($"Generation time: {sw.ElapsedMilliseconds} ms");
        }
    }
}