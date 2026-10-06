// Title: Custom Foreground and Background Colors for QR Code Barcode
// Description: Demonstrates how to set custom foreground (barcode) and background colors when generating a QR code image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize the visual appearance of barcodes. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a QR code with specific colors. Developers often need to match branding guidelines or improve scan reliability by adjusting colors, and this snippet shows the typical steps for such customizations.
// Prompt: Apply custom foreground and background colors to the barcode image using generator settings.
// Tags: qr, barcode, color, foreground, background, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a QR code barcode with custom foreground and background colors and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures barcode colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output directory path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full file path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "custom_color_barcode.png");

        // Text to encode in the QR code.
        string codeText = "Aspose";

        // Initialize the barcode generator for a QR code with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to yellow.
            generator.Parameters.BackColor = Color.Yellow;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}