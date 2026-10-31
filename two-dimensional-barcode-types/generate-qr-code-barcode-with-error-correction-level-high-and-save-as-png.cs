// Title: Generate QR Code with High Error Correction and Save as PNG
// Description: Demonstrates creating a QR Code barcode with high error correction level using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure QR Code parameters such as error correction level. It uses the BarcodeGenerator class together with EncodeTypes and QRErrorLevel enums to produce high‑reliability QR codes. Developers often need to generate QR codes for URLs or data payloads and control error correction to ensure readability under adverse conditions.
// Prompt: Generate a QR Code barcode with error correction level high and save as PNG.
// Tags: qr code, error correction, png, generation, aspose.barcode, barcodegenerator, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code with high error correction level and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code for a URL, sets high error correction, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_high.png");

        // Initialize the barcode generator with QR symbology and the data to encode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the QR Code error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}