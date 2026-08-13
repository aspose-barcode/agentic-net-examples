// Title: Generate QR Code with Custom Green Foreground and Black Background
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set a green bar color and a black background, and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and color customization via Parameters.Barcode.BarColor and Parameters.BackColor. Typical use cases include branding QR codes with corporate colors or improving visual contrast. Developers often need to adjust foreground and background colors when integrating barcodes into UI designs or printed materials.
// Prompt: Generate QR Code barcode and apply custom color palette with green foreground and black background.
// Tags: qr code, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with a green foreground
/// and a black background, then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument for the QR code text;
    /// otherwise defaults to "Hello World".
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the text to encode: use first argument if provided, else default.
        string codeText = args.Length > 0 ? args[0] : "Hello World";

        // Build the full path for the output PNG file in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_green.png");

        // Initialize the QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Apply a green color to the barcode's bars (foreground).
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Set the image background to black.
            generator.Parameters.BackColor = Color.Black;

            // Render and save the barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the generated QR code image was saved.
        Console.WriteLine($"QR code generated and saved to: {outputPath}");
    }
}