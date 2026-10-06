// Title: Generate QR Code with High Error Correction and Save as PNG
// Description: This example creates a QR Code barcode with high error correction level (Level H) containing the text "Hello World" and saves it as a PNG image file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR Code symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel to configure error correction, a common requirement for robust QR codes in marketing, inventory, and authentication scenarios. Developers often need to generate QR codes with specific error levels and export them to image formats like PNG for web or print use.
// Prompt: Generate a QR Code barcode with error correction level high and save as PNG.
// Tags: qr code, barcode generation, error correction, png, aspose.barcode, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with high error correction and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_high.png");

        // Initialize the barcode generator for QR Code symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the QR Code error correction level to high (Level H) for maximum data recovery.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}