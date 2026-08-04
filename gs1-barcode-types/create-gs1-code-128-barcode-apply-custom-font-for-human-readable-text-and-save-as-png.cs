// Title: Generate GS1 Code 128 barcode with custom font and save as PNG
// Description: Demonstrates creating a GS1 Code 128 barcode, applying a custom Helvetica font to the human‑readable text, and exporting the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as CodeText location, font, and alignment using the BarcodeGenerator class. Typical use cases include producing GS1‑compliant barcodes for retail and logistics, where custom text styling is required. Developers often need to customize human‑readable text appearance while generating barcodes programmatically.
// Prompt: Create a GS1 Code 128 barcode, apply a custom font for human‑readable text, and save as PNG.
// Tags: gs1, code128, barcode, generation, png, font

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with custom human‑readable text styling and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // GS1 Code 128 requires AI (01) with a 14‑digit GTIN.
        const string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 Code 128 with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Display human‑readable text below the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Set a custom font (Helvetica, 12pt) for the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Center the human‑readable text horizontally under the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode image as a PNG file.
            generator.Save("gs1code128.png");
        }

        // Output a simple confirmation to the console.
        Console.WriteLine("Barcode saved as gs1code128.png");
    }
}