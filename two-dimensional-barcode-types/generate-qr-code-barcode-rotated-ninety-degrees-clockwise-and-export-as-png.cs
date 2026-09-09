// Title: Generate and Save a Rotated QR Code as PNG
// Description: Demonstrates creating a QR Code barcode, rotating it 90 degrees clockwise, and exporting it to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. It shows setting rotation via Parameters.RotationAngle and saving the image with BarCodeImageFormat. Developers often need to customize barcode orientation and export formats for integration into reports, labels, or UI assets.
// Prompt: Generate a QR Code barcode rotated ninety degrees clockwise and export as PNG.
// Tags: qr code, rotation, png, aspose.barcode, barcodegenerator, encode types

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode, rotating it 90 degrees clockwise, and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the barcode, applies rotation, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG image
        string outputPath = Path.Combine(outputDir, "QrRotated90.png");

        // Initialize BarcodeGenerator with QR encoding and the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose QR"))
        {
            // Set rotation angle to 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user of the saved file location
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}