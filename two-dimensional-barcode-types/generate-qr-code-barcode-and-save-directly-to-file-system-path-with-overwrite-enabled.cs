// Title: Generate QR Code and Save as PNG with Overwrite
// Description: This example creates a QR Code barcode encoding a URL and saves it directly to a PNG file, overwriting any existing file.
// Category-Description: Demonstrates basic Aspose.BarCode generation for QR Code symbology. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure barcode parameters such as X‑Dimension and error correction level, then saves the image to the file system. Developers often need quick generation of QR codes for web links, product IDs, or authentication, and this pattern shows the typical steps for creating and perserving a barcode image.
// Prompt: Generate a QR Code barcode and save directly to file system path with overwrite enabled.
// Tags: qr code, barcode generation, png, overwrite, aspose.barcode, encode types, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Create a BarcodeGenerator for QR Code symbology with the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the X dimension (module size) of the QR Code in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Configure the error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code directly to the specified file path as PNG.
            // The Save method overwrites the file if it already exists.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}