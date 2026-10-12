// Title: Generate QR Code with High Contrast for Accessibility
// Description: Demonstrates creating a QR Code barcode with black on white colors to meet accessibility contrast guidelines.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure QR Code parameters such as error correction level, module size, and foreground/background colors. It uses the BarcodeGenerator class and related parameter objects to produce a PNG image. Developers often need to generate scannable barcodes with visual accessibility compliance for web and print media.
// Prompt: Generate QR Code barcode and ensure generated image passes accessibility contrast requirements.
// Tags: qr code, barcode generation, accessibility, contrast, aspnet, aspose.barcode, png, qrcode, error correction

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode with high contrast colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, configures the QR Code generator, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Determine a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the PNG image
        string outputPath = Path.Combine(outputDir, "qr_contrast.png");

        // Initialize the barcode generator for QR Code with the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Use the highest error correction level to improve scan reliability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color to white for maximum contrast
            generator.Parameters.BackColor = Color.White;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}