// Title: Generate QR Code and Rotate 180 Degrees
// Description: This example creates a QR Code barcode containing the text "Hello World", rotates it 180 degrees, and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode generation of QR Code symbology using the BarcodeGenerator class. Shows how to configure barcode parameters such as rotation angle and export the result to a PNG file. Ideal for developers needing to produce rotated barcodes for upside‑down displays, packaging, or custom UI layouts.
// Prompt: Generate QR Code barcode and rotate barcode 180 degrees for upside‑down display.
// Tags: qr, barcode, rotation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code, rotates it 180 degrees, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_rotated.png");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Rotate the generated barcode 180 degrees for upside‑down display.
            generator.Parameters.RotationAngle = 180f;

            // Save the rotated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}