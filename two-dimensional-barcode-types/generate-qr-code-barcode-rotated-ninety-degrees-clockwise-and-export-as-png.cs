// Title: Generate rotated QR Code and save as PNG
// Description: This example creates a QR Code barcode, rotates it 90 degrees clockwise, and saves it as a PNG image.
// Category-Description: Demonstrates basic barcode generation using Aspose.BarCode, covering encoding types, image format export, and barcode parameter customization such as rotation. Developers working with barcode creation, image rendering, and visual adjustments can refer to these examples to learn how to generate QR codes, set rotation angles, and save to common image formats.
// Prompt: Generate a QR Code barcode rotated ninety degrees clockwise and export as PNG.
// Tags: qr code, rotation, png, aspose.barcode, barcode generation, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Provides an example that generates a QR Code, rotates it, and saves it as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates a QR Code, applies a 90° clockwise rotation, and writes the result to disk.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Define the full path for the output PNG file.
            string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_rotated.png");

            // Initialize the barcode generator with QR encoding and the desired text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello QR"))
            {
                // Rotate the barcode 90 degrees clockwise.
                generator.Parameters.RotationAngle = 90f;

                // Save the rotated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Output the location of the saved image to the console.
            Console.WriteLine($"QR code saved to: {outputPath}");
        }
    }
}