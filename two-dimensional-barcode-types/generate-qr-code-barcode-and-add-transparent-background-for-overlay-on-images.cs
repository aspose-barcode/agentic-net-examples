// Title: Generate QR Code with Transparent Background
// Description: Creates a QR Code barcode, sets a transparent background, and saves it as a PNG for overlay on images.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation with visual customization. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a QR Code with transparent background, a common requirement when overlaying barcodes on photos or UI elements. Developers working with barcode rendering, image compositing, or custom branding will find this pattern useful.
// Prompt: Generate QR Code barcode and add a transparent background for overlay on images.
// Tags: qr code, barcode generation, transparent background, png, aspose.barcode, image overlay

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare an output directory in the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the data to encode and the full path for the resulting image
        string codeText = "https://example.com";
        string outputPath = Path.Combine(outputDir, "QrTransparent.png");

        // Generate the QR Code with a transparent background
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: adjust the size of each QR module (pixel size)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Set the color of the QR modules (foreground)
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color to transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG, which supports transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}