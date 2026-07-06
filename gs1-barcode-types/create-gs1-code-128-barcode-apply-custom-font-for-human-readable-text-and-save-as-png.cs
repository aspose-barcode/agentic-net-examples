// Title: Generate GS1 Code 128 Barcode with Custom Font
// Description: Demonstrates creating a GS1 Code 128 barcode, customizing the human‑readable text font, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical use cases include adding GS1-compliant barcodes to product packaging, applying branding through custom fonts for human‑readable text, and exporting the barcode to common image formats. Developers often need to adjust text appearance, alignment, and output settings when integrating barcodes into UI or print workflows.
// Prompt: Create a GS1 Code 128 barcode, apply a custom font for human‑readable text, and save as PNG.
// Tags: gs1,code128,barcode,generation,font,png,aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// applies a custom font to the human‑readable text, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with GS1 Code 128 symbology and sample GS1 data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Set a custom font (Arial, 12pt) for the human‑readable text displayed below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Align the human‑readable text to the center of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode image to a PNG file.
            generator.Save("gs1code128.png");
        }

        // Inform the user that the barcode has been created successfully.
        Console.WriteLine("GS1 Code 128 barcode generated and saved as gs1code128.png");
    }
}