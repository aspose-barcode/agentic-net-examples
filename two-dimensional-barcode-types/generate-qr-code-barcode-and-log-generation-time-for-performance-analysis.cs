// Title: Generate QR Code and measure generation time
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as PNG, and logging the time taken for generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image format classes. Typical use cases include creating QR codes for URLs, product information, or authentication, where developers need to control error correction, size, and performance metrics. The snippet helps developers quickly benchmark QR code generation in .NET applications.
// Prompt: Generate a QR Code barcode and log generation time for performance analysis.
// Tags: qr code, barcode generation, performance, aspnet, aspose.barcode, png, encode types

using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDemo
{
    /// <summary>
    /// Demonstrates QR Code generation and performance logging using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a QR Code, saves it as PNG, and writes elapsed time to console.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the QR code image
            string outputPath = "qr_code.png";

            // Start measuring the time required to generate the QR code
            var stopwatch = Stopwatch.StartNew();

            // Initialize the QR code generator with the desired text (a sample URL)
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Configure a high error correction level to improve readability under damage
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Set auto‑size mode to interpolation for smoother scaling
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Define the image dimensions (width and height) in points
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the generated QR code as a PNG file at the specified path
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Stop the stopwatch now that generation is complete
            stopwatch.Stop();

            // Output the elapsed time in milliseconds to the console
            Console.WriteLine($"QR code generated and saved to '{outputPath}' in {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}