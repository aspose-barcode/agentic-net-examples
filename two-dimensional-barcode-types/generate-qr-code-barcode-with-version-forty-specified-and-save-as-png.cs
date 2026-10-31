// Title: Generate QR Code with Version 40 and Save as PNG
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set the QR version to 40, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QRVersion to produce high‑density QR Code images. Typical use cases include creating printable QR codes for marketing, product tracking, or data encoding where the maximum data capacity is required. Developers often need to configure QR version, module size, and output format, which this snippet shows.
// Prompt: Generate a QR Code barcode with version forty specified and save as PNG.
// Tags: qr code, barcode generation, png, aspose.barcode, version40, qr version

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a QR Code barcode with version 40 and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that performs the QR Code generation and saving process.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Build the full path for the PNG file to be saved.
        string outputPath = Path.Combine(outputDir, "QRCodeVersion40.png");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code version to 40 (maximum data capacity).
            generator.Parameters.Barcode.QR.Version = QRVersion.Version40;
            // Define the size of each module (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}