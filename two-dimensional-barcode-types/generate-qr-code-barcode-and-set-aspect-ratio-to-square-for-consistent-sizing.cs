// Title: Generate QR Code with Square Aspect Ratio
// Description: Creates a QR Code barcode and forces a square aspect ratio to ensure consistent sizing across outputs.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. It shows setting barcode parameters such as XDimension and QR aspect ratio, a common requirement when developers need uniformly sized QR codes for UI layouts, printing, or scanning reliability. Typical use cases include generating QR codes for product labels, marketing materials, or authentication tokens.
// Prompt: Generate QR Code barcode and set aspect ratio to square for consistent sizing.
// Tags: qr, barcode, generation, aspectratio, square, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a square aspect ratio using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, forces a square aspect ratio, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "QrSquare.png");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the module size (X dimension) in pixels.
            gen.Parameters.Barcode.XDimension.Pixels = 4;

            // Force the QR Code to have a square aspect ratio (1:1).
            gen.Parameters.Barcode.QR.AspectRatio = 1;

            // Save the generated barcode image to the specified path in PNG format.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}