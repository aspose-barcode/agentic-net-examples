// Title: Set barcode text font to Times New Roman italic 16 pt
// Description: Demonstrates how to generate a Code128 barcode with custom text font using Aspose.BarCode. The example sets the barcode's human‑readable text to Times New Roman, italic style, 16 pt for emphasis.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance through the BarcodeGenerator and its Parameters. It shows usage of EncodeTypes, CodeTextParameters, FontMode, and image saving. Developers often need to adjust font family, style, and size to match branding or design guidelines when embedding barcodes in documents or UI.
// Prompt: Set barcode text font to Times New Roman, italic style, size 16 pt for emphasis.
// Tags: code128, barcode generation, font customization, png output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting barcode text font to Times New Roman italic 16 pt and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with custom font and saves it to the Output folder.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the font for the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;               // Use manual font settings
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";    // Set font family
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Italic;          // Apply italic style
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 16f;                  // Set size to 16 pt

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}