// Title: Generate QR Code PNG with 300 DPI
// Description: This example creates a QR code containing sample text and saves it as a PNG image with a resolution of 300 DPI.
// Category-Description: Demonstrates Aspose.BarCode generation of 2‑D barcodes. The example uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce a QR code image. Typical use cases include creating printable QR codes for marketing, product tracking, or authentication. Developers often need to set image resolution, format, and content when integrating barcode generation into .NET applications.
// Prompt: Generate a QR code and save it as a PNG file with 300 DPI resolution.
// Tags: qr code, generation, png, resolution, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR code and saves it as a PNG file with 300 DPI resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Initialize the barcode generator with QR encoding and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure the image resolution to 300 DPI for high‑quality output.
            generator.Parameters.Resolution = 300;

            // Save the generated QR code as a PNG file at the specified location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the QR code has been successfully saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}