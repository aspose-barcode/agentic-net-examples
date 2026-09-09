// Title: Generate QR Code with Version 40 and Save as PNG
// Description: This example creates a QR Code barcode using Aspose.BarCode, sets the QR version to 40 (maximum data capacity), and saves the image as a PNG file.
// Category-Description: Demonstrates barcode generation with Aspose.BarCode focusing on QR Code creation. It showcases the use of BarcodeGenerator, EncodeTypes, QRVersion, and BarCodeImageFormat to produce high‑capacity QR codes, a common requirement for marketing, product tracking, and data‑rich applications. Developers looking for QR Code generation patterns can reference this example as part of a collection of barcode generation tutorials.
// Prompt: Generate a QR Code barcode with version forty specified and save as PNG.
// Tags: qr code, barcode generation, png, aspose.barcode, qrcode version, image output

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
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "QRCodeVersion40.png");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code version to 40 (maximum size and data capacity).
            gen.Parameters.Barcode.QR.Version = QRVersion.Version40;

            // Save the generated QR Code image as a PNG file.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}