// Title: Generate GS1 Code 128 barcode with custom human‑readable font
// Description: Demonstrates creating a GS1 Code 128 barcode, applying a custom Helvetica font to the human‑readable text, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode symbology (GS1 Code 128), customize code‑text appearance, and export to common image formats. Developers working with product identification, inventory, or retail systems often need to generate GS1‑compliant barcodes with readable text, using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Create a GS1 Code 128 barcode, apply a custom font for human‑readable text, and save as PNG.
// Tags: gs1, code128, barcode generation, png, custom font, human readable text

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// customizes the human‑readable text font, and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine a temporary file path for the output PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "gs1code128.png");

        // GS1 Code 128 requires GS1‑formatted data.
        // Example: (01) followed by a 14‑digit GTIN.
        string gs1CodeText = "(01)12345678901231";

        // Initialize the barcode generator with the GS1 Code 128 symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
        {
            // Position the human‑readable text below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Apply a custom Helvetica font (12‑point) to the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Render and save the barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}