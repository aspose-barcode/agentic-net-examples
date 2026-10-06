// Title: Adjust Barcode Text Font and Position
// Description: This example demonstrates how to set the barcode's human‑readable text font to Arial 12 pt and place it centered below the bars using Aspose.BarCode.
// Category-Description: Aspose.BarCode generation examples illustrate how to configure barcode appearance and layout. They cover key API classes such as BarcodeGenerator, EncodeTypes, and CodeTextParameters, showing typical use cases like customizing font, alignment, and location of human‑readable text for various symbologies. Developers often need to fine‑tune these settings to match branding or printing requirements.
// Prompt: Adjust barcode text font to Arial, size 12 pt, and center the text beneath the bars.
// Tags: barcode, code128, textfont, alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates configuring barcode text font, size, and alignment using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode with customized text appearance and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outPath = Path.Combine(outputDir, "barcode.png");

        // Create a barcode generator for Code128 with the specified value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the human‑readable text font to Arial, 12 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Position the text below the bars and center it horizontally
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG image
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}