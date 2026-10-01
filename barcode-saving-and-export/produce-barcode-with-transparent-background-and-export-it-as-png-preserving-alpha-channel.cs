// Title: Generate QR barcode with transparent background saved as PNG
// Description: Demonstrates creating a QR code barcode with a transparent background and exporting it to a PNG file that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class, set background colors, and save images in formats that support transparency such as PNG. Developers often need to embed barcodes into UI designs or documents where the background must blend with surrounding content, so controlling the alpha channel is essential.
// Prompt: Produce a barcode with transparent background and export it as PNG preserving the alpha channel.
// Tags: qr, barcode, transparent background, png, alpha channel, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR code with a transparent background
/// and saves it as a PNG image preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures colors, and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the QR code.
        string codeText = "https://example.com";

        // Build a temporary file path for the output PNG.
        string outputPath = Path.Combine(Path.GetTempPath(), "transparent_barcode.png");

        // Ensure the target directory exists before saving.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator with QR symbology and the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the background color to transparent so the PNG retains alpha.
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the barcode (foreground) color to black for contrast.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file; PNG supports alpha transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}