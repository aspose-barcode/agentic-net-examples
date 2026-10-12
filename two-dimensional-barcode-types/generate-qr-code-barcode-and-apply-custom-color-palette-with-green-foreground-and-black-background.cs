// Title: Generate QR Code with custom green foreground and black background
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, setting a green foreground color and a black background, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on visual customization of generated barcodes. It showcases the use of BarcodeGenerator, EncodeTypes, and color properties (BarColor, BackColor) to apply custom color palettes. Developers often need to match branding or UI themes by adjusting foreground and background colors when generating QR codes or other symbologies, and this snippet illustrates the typical steps.
// Prompt: Generate QR Code barcode and apply custom color palette with green foreground and black background.
// Tags: qr code, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode with a green foreground and black background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates the QR Code with custom colors, saves it as PNG, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "qr_green_foreground_black_background.png");

        // Initialize the barcode generator for QR encoding with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the barcode (foreground) color to green
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Set the background color to black
            generator.Parameters.BackColor = Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}