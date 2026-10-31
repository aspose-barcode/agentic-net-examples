// Title: Generate QR Code with Transparent Background
// Description: Demonstrates creating a QR Code barcode and saving it as a PNG with a fully transparent background, suitable for overlay on colored images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image formatting and visual customization. It showcases the use of BarcodeGenerator, EncodeTypes, and image format classes to produce QR Code barcodes with transparent backgrounds, a common requirement when integrating barcodes into UI designs or marketing materials. Developers often need to adjust colors, dimensions, and transparency to blend barcodes seamlessly with existing graphics.
// Prompt: Generate QR Code barcode and set background transparency to allow overlay on colored backgrounds.
// Tags: qr code, barcode generation, transparent background, png, aspose.barcode, encoding, image formatting

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode with a transparent background
/// and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Set the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "qr_transparent.png");
        string codeText = "Hello, World!";

        // Initialize the barcode generator for QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set a fully transparent background (alpha = 0)
            generator.Parameters.BackColor = Color.FromArgb(0, 255, 255, 255);
            // Optional: set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;
            // Optional: adjust the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image with transparency preserved
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}