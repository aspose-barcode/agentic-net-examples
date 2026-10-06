// Title: Generate QR Code PNG with 300 DPI
// Description: This example creates a QR code containing the text "Hello World" and saves it as a PNG image with a resolution of 300 DPI.
// Category-Description: Demonstrates basic usage of Aspose.BarCode for QR code generation. It shows how to configure the barcode generator, set image resolution, and export to PNG using the BarcodeGenerator class. Useful for developers needing quick QR code creation for web, print, or mobile applications.
// Prompt: Generate a QR code and save it as a PNG file with 300 DPI resolution.
// Tags: qr, barcode, generation, png, resolution, aspose.barcode, encode-types.qr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR code and saves it as a PNG file with 300 DPI resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program.
    /// Generates the QR code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Initialize the barcode generator with QR encoding and the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR code image
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}