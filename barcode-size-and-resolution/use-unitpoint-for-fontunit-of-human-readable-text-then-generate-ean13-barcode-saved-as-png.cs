// Title: Generate EAN13 barcode with point‑size font and save as PNG
// Description: Demonstrates creating an EAN‑13 barcode, configuring the human‑readable text font using Unit.Point, and exporting the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and CodeTextParameters to produce barcodes for product labeling, inventory, and retail applications. Developers often need to customize font size, placement, and output format when integrating barcodes into documents or printing workflows.
// Prompt: Use Unit.Point for FontUnit of human‑readable text, then generate EAN13 barcode saved as PNG.
// Tags: ean13, barcode, generation, png, fontunit, point, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates an EAN‑13 barcode, sets the human‑readable text font size using Unit.Point,
/// and saves the barcode image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file name.
        string outputPath = "ean13.png";

        // Create a BarcodeGenerator for EAN13 with the specified numeric value.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Set the human‑readable text font size in points (Unit.Point).
            gen.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Position the human‑readable text below the barcode.
            gen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode as a PNG image.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"EAN13 barcode saved to {outputPath}");
    }
}