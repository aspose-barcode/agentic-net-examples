// Title: Generate QR Code and Rotate 180 Degrees
// Description: Creates a QR Code barcode containing a sample text, rotates it 180 degrees for upside‑down display, and saves the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.QR, configure visual parameters such as X‑Dimension, apply a rotation angle, and export the barcode to common image formats. Typical use cases include creating printable QR codes, customizing orientation for specific display requirements, and integrating barcode generation into .NET applications.
// Prompt: Generate QR Code barcode and rotate barcode 180 degrees for upside‑down display.
// Tags: qr code, barcode generation, rotation, png, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode, rotating it 180 degrees, and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies rotation, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDirectory = Path.Combine(Environment.CurrentDirectory, "Barcodes");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Build the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDirectory, "QRCodeRotated180.png");

        // Initialize the barcode generator with QR encoding and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, World!"))
        {
            // Set the X‑Dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Rotate the barcode 180 degrees for upside‑down display.
            generator.Parameters.RotationAngle = 180f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}