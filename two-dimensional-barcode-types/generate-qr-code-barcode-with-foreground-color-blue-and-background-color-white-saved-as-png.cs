// Title: Generate QR Code with Custom Colors and Save as PNG
// Description: Demonstrates how to create a QR Code barcode, set its foreground to blue and background to white, and save the image as a PNG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce QR Code images with custom colors. Developers commonly need to customize barcode appearance for branding or UI integration, and this snippet shows the typical steps for setting colors and exporting to PNG.
// Prompt: Generate a QR Code barcode with foreground color blue and background color white, saved as PNG.
// Tags: qr code, barcode generation, png, aspose.barcode, aspose.drawing, color customization

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with custom colors and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code, applies color settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = "qr_blue_white.png";

        // Initialize the QR Code generator within a using block to ensure proper disposal
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text that will be encoded into the QR Code
            generator.CodeText = "Sample QR Code";

            // Configure the barcode's foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Configure the barcode's background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}