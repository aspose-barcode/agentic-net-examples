// Title: Generate and rotate QR Code barcode
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and rotating it 180 degrees for upside‑down display.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. It shows setting barcode parameters such as RotationAngle to modify orientation, a common requirement when displaying barcodes in non‑standard layouts or on rotated media. Developers often need to generate barcodes programmatically and adjust their visual appearance for printing or UI purposes.
// Prompt: Generate QR Code barcode and rotate barcode 180 degrees for upside‑down display.
// Tags: qr code, rotation, barcode generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and rotating it 180 degrees for upside‑down display.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, applies rotation, saves the image, and outputs the file path.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "QRCodeRotated180.png");

        // Initialize the barcode generator with QR encoding and the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, World!"))
        {
            // Rotate the barcode 180 degrees for upside‑down display
            generator.Parameters.RotationAngle = 180f;
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}