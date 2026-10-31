// Title: Generate EAN13 Barcode with Point Font Size and Save as PNG
// Description: Demonstrates how to create an EAN‑13 barcode, set the human‑readable text font size using points, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes. Typical scenarios include creating product labels, inventory tags, and printable assets where precise font sizing (using FontUnit.Point) is required. Developers often need to customize human‑readable text appearance and export barcodes to common image formats.
// Prompt: Use Unit.Point for FontUnit of human‑readable text, then generate EAN13 barcode saved as PNG.
// Tags: ean13, barcode, generation, png, fontunit, point, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates an EAN‑13 barcode, sets the human‑readable text font size in points,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ean13.png");

        // Create a BarcodeGenerator for EAN13 with the specified numeric value.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Set the human‑readable text font size to 12 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"EAN13 barcode saved to: {outputPath}");
    }
}