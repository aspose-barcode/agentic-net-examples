// Title: Generate and Save a Rotated QR Code as PNG
// Description: Demonstrates creating a QR Code barcode, rotating it 90 degrees clockwise, and exporting it to a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. It shows setting barcode parameters such as rotation and saving the result in a common image format (PNG). Developers working with barcode creation for marketing, inventory, or authentication often need to customize orientation and export options, and this snippet provides a concise reference.
// Prompt: Generate a QR Code barcode rotated ninety degrees clockwise and export as PNG.
// Tags: qr code, rotation, png, aspose.barcode, barcode generation, encode types, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code, rotating it, and saving as PNG using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates QR Code, rotates, saves, and writes path to console.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full path for the PNG file
        string outputPath = Path.Combine(outputDir, "QrRotated.png");

        // Initialize the barcode generator with QR symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose!"))
        {
            // Rotate the barcode 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}