// Title: Generate QR Code with Custom Colors and Save as PNG
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set the foreground to blue and background to white, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.QR, configure visual parameters such as BarColor and BackColor, and export the result in common image formats like PNG. Developers working on QR code creation, branding, or custom‑styled barcodes can use these patterns to produce images for web, print, or mobile applications.
// Prompt: Generate a QR Code barcode with foreground color blue and background color white, saved as PNG.
// Tags: qr code, barcode generation, png, color, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Provides an entry point that generates a QR Code with custom colors and saves it as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Creates a QR Code barcode, applies blue foreground and white background colors, and writes the image to disk.
        /// </summary>
        static void Main()
        {
            // Define the full path for the output PNG file.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_blue_white.png");

            // Initialize the barcode generator for QR Code with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
            {
                // Set the barcode (foreground) color to blue.
                generator.Parameters.Barcode.BarColor = Color.Blue;

                // Set the background color to white.
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode as a PNG image to the specified path.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Output the location of the saved QR code image.
            Console.WriteLine($"QR code saved to: {outputPath}");
        }
    }
}