// Title: Generate QR Code with two-module margin padding
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set the module size, and add a visual margin equal to two modules.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and layout customization. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and barcode parameter settings such as XDimension and Padding. Developers use these APIs to produce QR codes with precise sizing and visual padding for scanning reliability and aesthetic integration.
// Prompt: Generate QR Code barcode and set margin to two modules for visual padding.
// Tags: qr code, barcode generation, margin, padding, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a two‑module margin using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, applies padding, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_margin.png");

        // Ensure the target directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the QR Code generator with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the size of a single QR module (XDimension) to 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Compute padding equal to two modules (2 * XDimension)
            float padding = generator.Parameters.Barcode.XDimension.Point * 2f;

            // Apply the calculated padding to all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Point = padding;
            generator.Parameters.Barcode.Padding.Top.Point = padding;
            generator.Parameters.Barcode.Padding.Right.Point = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;

            // Save the generated QR Code image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code with two-module margin saved to: {outputPath}");
    }
}