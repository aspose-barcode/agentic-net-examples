// Title: Generate QR Code with Transparent Background
// Description: Creates a QR Code barcode, sets a transparent background, and saves it as a PNG for overlay on images.
// Category-Description: This example demonstrates Aspose.BarCode's barcode generation capabilities, focusing on QR Code creation with a transparent background. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a PNG image suitable for compositing over other graphics. Developers often need such images for UI overlays, marketing materials, or embedding barcodes in photos where the background must remain visible.
// Prompt: Generate QR Code barcode and add a transparent background for overlay on images.
// Tags: qr code, barcode generation, transparent background, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a QR Code barcode with a transparent background
/// and save it as a PNG image suitable for overlaying on other images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code, applies transparency,
    /// and writes the resulting image to the Output folder.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "QrTransparent.png");

        // Initialize the barcode generator for a QR Code with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the background color to transparent so the image can be overlaid
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the QR Code modules (bars) to black (default value)
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG, which supports alpha transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR Code with transparent background saved to: {outputPath}");
    }
}