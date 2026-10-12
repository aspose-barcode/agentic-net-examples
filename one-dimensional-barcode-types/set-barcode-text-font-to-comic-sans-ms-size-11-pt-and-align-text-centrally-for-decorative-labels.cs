// Title: Generate Code128 Barcode with Custom Font and Centered Text
// Description: Demonstrates how to create a Code128 barcode, set the human‑readable text to Comic Sans MS 11 pt, and align it centrally for decorative label use.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class together with EncodeTypes and CodeTextParameters. Typical scenarios include creating custom labels, receipts, or product tags where specific font styling and text alignment are required. Developers often need to control font appearance, size, and positioning of the readable text to match branding or design guidelines.
/// Prompt: Set barcode text font to Comic Sans MS, size 11 pt, and align text centrally for decorative labels.
/// Tags: code128, barcode generation, text font, alignment, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with custom text font and alignment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image with Comic Sans MS font, 11 pt size, centered text, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the barcode content and output file path
        string codeText = "DecorativeLabel";
        string outputPath = Path.Combine(outputDir, "Label.png");

        // Create a BarcodeGenerator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable manual font configuration
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Comic Sans MS and size to 11 points
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Comic Sans MS";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 11f;

            // Align the human‑readable text to the center of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}