// Title: Generate QR Code and Rotate 180 Degrees for Upside-Down Display
// Description: This example creates a QR Code barcode, rotates it 180 degrees, and saves it as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using BarcodeGenerator with QR Code symbology. Shows how to configure QR error correction, apply image rotation, and export to PNG. Useful for developers needing custom orientation of barcodes for labels, packaging, or UI elements where upside‑down display is required.
// Prompt: Generate QR Code barcode and rotate barcode 180 degrees for upside‑down display.
// Tags: qr code, rotation, barcode generation, png, aspose.barcode, encode types, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code, rotates it 180° for upside‑down display,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_upside_down.png");

        // Initialize a QR Code generator with the desired text/content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Rotate the generated barcode image 180 degrees (upside‑down).
            generator.Parameters.RotationAngle = 180f;

            // Set a high error correction level to improve readability after rotation.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the rotated QR Code as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the PNG file has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}