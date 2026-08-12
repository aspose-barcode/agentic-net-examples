// Title: Generate QR Code with custom quiet zone using Aspose.BarCode
// Description: Demonstrates creating a QR Code barcode, setting module size, and configuring an eight‑module quiet zone for improved scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension and Padding. Typical use cases include producing QR Code images for marketing, product labeling, or mobile app interactions where precise quiet zone control is required. Developers often need to adjust module size and padding to meet scanner specifications.
// Prompt: Generate QR Code barcode and configure quiet zone size to eight modules for scanner tolerance.
// Tags: qr code, barcode generation, quiet zone, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code image with a custom quiet zone using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the QR Code, applies padding, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Initialize the QR Code generator with the desired text payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the size of a single QR module (XDimension) to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate quiet zone size: eight modules on each side.
            float quietZone = 8 * generator.Parameters.Barcode.XDimension.Point;

            // Apply the calculated quiet zone to all four padding sides.
            generator.Parameters.Barcode.Padding.Left.Point = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}