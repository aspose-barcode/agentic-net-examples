// Title: Generate QR Code and Log Generation Parameters
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, configuring its properties, and outputting detailed generation settings for debugging.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. It covers setting QR‑specific options (error correction level, encode mode), general barcode appearance (module size, padding, colors), and resolution. Developers often need such patterns when integrating barcode creation into web services, reporting tools, or automated testing pipelines, and require detailed logs to troubleshoot rendering issues.
// Prompt: Generate QR Code barcode and log detailed generation parameters for debugging purposes.
// Tags: qr, barcode, generation, debugging, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDemo
{
    /// <summary>
    /// Provides a simple console demonstration of QR Code generation using Aspose.BarCode.
    /// The program configures QR‑specific settings, logs all relevant parameters, and saves the image as PNG.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates a QR Code, logs its configuration, and writes the image to disk.
        /// </summary>
        static void Main()
        {
            // Determine the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

            // Initialize a QR Code generator with the QR symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data that the QR Code will encode.
                generator.CodeText = "https://example.com";

                // Configure QR‑specific options.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH; // Highest error correction.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;   // Automatic mode selection.

                // Configure general barcode appearance.
                generator.Parameters.Barcode.XDimension.Point = 2f; // Module size in points.
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
                generator.Parameters.Resolution = 300; // DPI for the output image.
                generator.Parameters.Barcode.BarColor = Color.Black; // Bar (module) color.

                // Log detailed generation parameters for debugging.
                Console.WriteLine("QR Code Generation Parameters:");
                Console.WriteLine($"CodeText: {generator.CodeText}");
                Console.WriteLine($"ErrorLevel: {generator.Parameters.Barcode.QR.ErrorLevel}");
                Console.WriteLine($"EncodeMode: {generator.Parameters.Barcode.QR.EncodeMode}");
                Console.WriteLine($"XDimension (points): {generator.Parameters.Barcode.XDimension.Point}");
                Console.WriteLine($"Padding (L,T,R,B) points: {generator.Parameters.Barcode.Padding.Left.Point}, {generator.Parameters.Barcode.Padding.Top.Point}, {generator.Parameters.Barcode.Padding.Right.Point}, {generator.Parameters.Barcode.Padding.Bottom.Point}");
                Console.WriteLine($"Resolution DPI: {generator.Parameters.Resolution}");
                Console.WriteLine($"BarColor: {generator.Parameters.Barcode.BarColor}");

                // Save the generated QR Code as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR code saved to: {outputPath}");
            }
        }
    }
}