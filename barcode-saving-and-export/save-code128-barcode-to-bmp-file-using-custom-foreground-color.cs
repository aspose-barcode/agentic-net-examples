// Title: Save Code128 barcode as BMP with custom foreground color
// Description: Demonstrates generating a Code128 barcode, applying a custom dark‑green bar color, and saving it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and color customization. Typical use cases include creating printable barcodes for product labeling, inventory tracking, or shipping documentation where specific visual styling (e.g., brand colors) is required. Developers often need to adjust bar colors, output formats, and encoding types to meet branding or system integration needs.
/// Prompt: Save a Code128 barcode to a BMP file using a custom foreground color.
/// Tags: code128, barcode, generation, bmp, color, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, sets a custom foreground color,
/// and saves the result as a BMP image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code128.bmp");

        // Initialize a BarcodeGenerator for Code128 with the sample text "Sample123".
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply a custom dark‑green color to the barcode bars (foreground).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 100, 0);

            // Save the generated barcode image as a BMP file at the specified location.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}