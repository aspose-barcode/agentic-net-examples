// Title: Generate Code128 Barcode with Text Above Bars
// Description: Demonstrates creating a Code128 barcode where the human‑readable text is positioned above the barcode symbols.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes, BarCodeImageFormat, and related parameter objects to customize barcode appearance. Typical use cases include adding custom text placement, adjusting font size, and setting spacing for printable barcodes. Developers often need to control these settings to meet branding or labeling requirements.
// Prompt: Create a barcode with custom text positioned above the bars (top placement).
// Tags: code128, barcode generation, text placement, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with the code text placed above the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "barcode_above.png");

        // Create a BarcodeGenerator for Code128 with the desired custom text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "CustomText"))
        {
            // Position the human‑readable text above the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Set the spacing between the text and the barcode (5 points).
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Define the font size for the code text (12 points).
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}