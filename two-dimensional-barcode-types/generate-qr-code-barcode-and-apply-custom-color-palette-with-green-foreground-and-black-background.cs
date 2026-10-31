// Title: Generate QR Code with Custom Green Foreground and Black Background
// Description: This example creates a QR Code barcode, applies a green foreground and black background color palette, and saves the result as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with visual customization. It uses the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and color properties to produce branded QR codes. Typical scenarios include creating QR codes that match corporate colors or UI themes, a common need for developers integrating barcodes into web or desktop applications.
// Prompt: Generate QR Code barcode and apply custom color palette with green foreground and black background.
// Tags: qr, barcode, color, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with a custom green foreground and black background, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code, applies colors, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path where the PNG image will be saved
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_green.png");

        // Initialize the barcode generator for a QR Code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Apply custom colors: green bars (foreground) and black background
            generator.Parameters.Barcode.BarColor = Color.Green;
            generator.Parameters.BackColor = Color.Black;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}