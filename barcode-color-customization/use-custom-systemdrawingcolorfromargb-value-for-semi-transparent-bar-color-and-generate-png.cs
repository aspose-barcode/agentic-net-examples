// Title: Generate semi‑transparent Code128 barcode PNG with Aspose.BarCode
// Description: Demonstrates setting a custom semi‑transparent bar color using System.Drawing.Color.FromArgb and saving the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance with the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat. Typical use cases include creating custom‑styled barcodes for product labels, packaging, or digital media where visual branding is required. Developers often need to adjust colors, sizes, and output formats, making this a reference for color customization and PNG output.
// Prompt: Use a custom System.Drawing.Color.FromArgb value for semi‑transparent bar color and generate PNG.
// Tags: barcode, code128, color, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example that generates a Code128 barcode with a semi‑transparent bar color
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies a custom semi‑transparent color,
    /// and writes the resulting PNG file to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.png");

        // Initialize a BarcodeGenerator for Code128 symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply a semi‑transparent red color to the barcode bars (alpha = 128)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 255, 0, 0);

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}