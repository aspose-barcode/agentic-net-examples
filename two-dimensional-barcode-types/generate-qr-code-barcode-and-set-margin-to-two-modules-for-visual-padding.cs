// Title: Generate QR Code with Custom Margin
// Description: Demonstrates creating a QR Code barcode, adjusting the module size, and applying a two‑module margin to provide visual padding around the code.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and the Parameters.Barcode properties to customize QR Code appearance. Typical scenarios include generating QR codes for marketing, product labeling, or authentication where extra whitespace is required for scanning reliability. Developers often need to control module size, padding, and output format, making this pattern a common reference for barcode creation tasks.
// Prompt: Generate QR Code barcode and set margin to two modules for visual padding.
// Tags: qr code, barcode generation, margin, padding, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with a two‑module margin for visual padding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "QrCodeWithMargin.png");

        // Create a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the module size (XDimension) to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate margin as two modules (2 * XDimension).
            float margin = 2f * generator.Parameters.Barcode.XDimension.Point;

            // Apply the calculated margin to all sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = margin;
            generator.Parameters.Barcode.Padding.Top.Point = margin;
            generator.Parameters.Barcode.Padding.Right.Point = margin;
            generator.Parameters.Barcode.Padding.Bottom.Point = margin;

            // Save the generated QR Code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}