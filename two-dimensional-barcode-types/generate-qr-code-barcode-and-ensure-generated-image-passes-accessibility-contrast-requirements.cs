// Title: Generate QR Code with High Contrast for Accessibility
// Description: Creates a QR Code barcode image with black foreground on white background to meet accessibility contrast guidelines.
// Category-Description: This example demonstrates Aspose.BarCode's barcode generation capabilities, focusing on QR Code creation using the BarcodeGenerator class. It shows how to configure encoding mode, error correction level, module size, and foreground/background colors to satisfy accessibility contrast requirements. Developers commonly use these APIs to embed scannable QR codes in applications, websites, or printed media while ensuring readability for users with visual impairments.
/// Prompt: Generate QR Code barcode and ensure generated image passes accessibility contrast requirements.
/// Tags: qr code, barcode generation, accessibility, contrast, aspose.barcode, png, encode types, high error correction

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a QR Code barcode image with high contrast suitable for accessibility compliance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code PNG file with black on white colors and high error correction.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string outputPath = Path.Combine(outputDir, "QrCode.png");

        // --------------------------------------------------------------------
        // Define the data to encode in the QR Code
        // --------------------------------------------------------------------
        string codeText = "https://example.com";

        // --------------------------------------------------------------------
        // Create and configure the QR Code generator
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // QR-specific settings: use ECI encoding (UTF-8) and highest error correction level
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Set colors to ensure maximum contrast (black foreground, white background)
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"QR Code generated at: {outputPath}");
    }
}