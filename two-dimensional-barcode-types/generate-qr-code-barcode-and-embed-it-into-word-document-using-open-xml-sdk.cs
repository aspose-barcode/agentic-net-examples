// Title: Generate QR Code and Save as PNG Image
// Description: This example creates a QR Code barcode from a URL and saves it as a PNG file.
// Category-Description: Demonstrates basic barcode generation using Aspose.BarCode. It showcases the BarcodeGenerator class with QR encoding, configuring error correction level, and exporting the result to an image format. Developers commonly use these APIs to create barcodes for marketing, product tracking, or data sharing scenarios, often integrating the generated images into documents or web pages.
// Prompt: Generate QR Code barcode and embed it into a Word document using Open XML SDK.
// Tags: qr code, barcode, generation, png, aspose.barcode, openxml

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a QR Code barcode and save it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path where the QR code image will be saved.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Create a BarcodeGenerator for QR encoding with the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the QR error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}