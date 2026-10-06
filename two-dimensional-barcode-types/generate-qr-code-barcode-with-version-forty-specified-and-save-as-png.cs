// Title: Generate QR Code with Version 40 and Save as PNG
// Description: This example creates a QR Code barcode using Aspose.BarCode, sets the QR version to 40, and saves the image as a PNG file.
// Category-Description: Demonstrates QR Code generation using Aspose.BarCode's BarcodeGenerator. Shows how to configure QR version, adjust module size, and export to PNG. Useful for developers needing high-capacity QR codes in .NET applications, covering key classes like BarcodeGenerator, EncodeTypes, QRVersion, and BarCodeImageFormat.
// Prompt: Generate a QR Code barcode with version forty specified and save as PNG.
// Tags: qr code, barcode generation, png output, aspose.barcode, qrversion, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with version 40 and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the QR Code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "QRCodeVersion40.png");

        // Initialize the barcode generator for QR Code symbology with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose"))
        {
            // Set the size of a single QR module (pixel dimension) to 4.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Specify the QR Code version to 40 (maximum data capacity).
            generator.Parameters.Barcode.QR.Version = QRVersion.Version40;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}