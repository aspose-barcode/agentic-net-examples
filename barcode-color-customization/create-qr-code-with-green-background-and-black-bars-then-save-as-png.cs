// Title: Generate QR Code with Green Background and Black Bars
// Description: Demonstrates creating a QR code using Aspose.BarCode, customizing its background to green and foreground to black, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure visual properties such as background and bar colors for 2D barcodes. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common tasks for developers needing customized barcode images for web or print media.
// Prompt: Create a QR code with a green background and black bars, then save as PNG.
// Tags: qr code, barcode generation, png, aspose.barcode, color customization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating QR code generation with custom colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code with a green background and black bars, then saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_green_background.png");

        // Initialize barcode generator for QR code with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set background color to green
            generator.Parameters.BackColor = Color.Green;

            // Set bar (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}