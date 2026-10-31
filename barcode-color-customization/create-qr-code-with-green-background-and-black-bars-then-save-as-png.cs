// Title: Generate QR code with green background and black bars
// Description: Demonstrates creating a QR code, customizing its background to green and bar color to black, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize QR code appearance. Typical use cases include branding QR codes with corporate colors or creating visually distinct codes for marketing materials. Developers often need to adjust colors, sizes, and output formats when integrating barcode generation into .NET applications.
// Prompt: Create a QR code with a green background and black bars, then save as PNG.
// Tags: qr code, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code with a green background and black bars,
/// then saves the image as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR code, applies color customizations,
    /// saves the result, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_green_background_black_bars.png");

        // Initialize the barcode generator for QR code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the background color of the QR code to green
            generator.Parameters.BackColor = Color.Green;

            // Set the color of the QR code bars (modules) to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated QR code as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}