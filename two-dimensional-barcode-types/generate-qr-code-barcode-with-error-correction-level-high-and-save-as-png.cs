// Title: Generate QR Code with High Error Correction and Save as PNG
// Description: Demonstrates creating a QR Code barcode with high error correction level (Level H) using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure QR Code parameters such as error correction level. It showcases the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel classes to produce high‑reliability QR codes, a common requirement for applications needing robust data encoding. Developers often refer to these patterns when generating QR codes for URLs, contact info, or product data.
// Prompt: Generate a QR Code barcode with error correction level high and save as PNG.
// Tags: qr code, error correction, png, barcode generation, aspose.barcode, encode types, qrcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code with high error correction level and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        string outputPath = "qr_high_error.png";

        // Initialize the QR code generator with the desired text (e.g., a URL).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the QR code to use the highest error correction level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Render and save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the generated QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}