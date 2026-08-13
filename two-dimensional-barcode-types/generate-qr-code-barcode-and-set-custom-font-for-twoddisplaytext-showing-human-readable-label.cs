// Title: Generate QR Code with Custom Human‑Readable Font
// Description: Demonstrates how to create a QR Code barcode, set a custom human‑readable label, and apply a specific font to the TwoDDisplayText.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating QR Code creation using BarcodeGenerator. It shows how to configure human‑readable text (TwoDDisplayText) and customize its font, a common requirement when developers need printable barcodes with clear labels. Typical use cases include marketing materials, product packaging, and documentation where QR codes are paired with descriptive text.
// Prompt: Generate QR Code barcode and set custom font for TwoDDisplayText showing human readable label.
// Tags: qr code, barcode generation, custom font, twoddisplaytext, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code image, adds a human‑readable label,
/// and applies a custom font to that label using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the QR Code, configures display text and font,
    /// saves the image as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Initialize the QR Code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data that the QR Code will encode.
            generator.CodeText = "https://example.com";

            // Define the human‑readable text that appears below the QR Code.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "My QR Code";

            // Apply a custom font (Helvetica, 14pt) to the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Optionally increase error correction to the highest level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image in PNG format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR code image saved to: {outputPath}");
    }
}