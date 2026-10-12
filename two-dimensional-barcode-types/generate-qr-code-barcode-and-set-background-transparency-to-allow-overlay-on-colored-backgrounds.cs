// Title: Generate QR Code with Transparent Background
// Description: Demonstrates creating a QR Code barcode and setting its background to fully transparent so it can be overlaid on colored images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. It covers configuring barcode colors, applying a transparent background, and saving the result in PNG format to preserve alpha channel data. Developers often need these steps when integrating barcodes into UI designs, reports, or marketing materials that require seamless blending with existing graphics.
// Prompt: Generate QR Code barcode and set background transparency to allow overlay on colored backgrounds.
// Tags: qr code, barcode generation, transparent background, png, aspose.barcode, encoding, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode with a fully transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "qr_transparent.png");
        // Text to encode in the QR Code.
        string codeText = "Transparent QR Example";

        // Initialize the barcode generator for QR Code symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set the barcode's foreground (bars) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color to fully transparent (alpha = 0).
            generator.Parameters.BackColor = Color.FromArgb(0, 255, 255, 255);

            // Save the barcode as a PNG image to retain the transparency information.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"QR code with transparent background saved to: {outputPath}");
    }
}