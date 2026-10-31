// Title: Generate Code128 barcode with custom Calibri font and right-aligned text
// Description: This example creates a Code128 barcode, sets the human‑readable text to use the Calibri font at 13 pt, and aligns the text to the right of the barcode.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on customizing barcode text appearance. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a PNG image. Typical scenarios include branding, label design, and compliance where specific fonts and text alignment are required. Developers often need to adjust font properties, alignment, and output formats when integrating barcodes into documents or packaging.
// Prompt: Create a barcode with custom text font Calibri, size 13 pt, and align text right of the barcode.
// Tags: barcode, code128, textfont, alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with custom text styling and alignment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting barcode image.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the text font to be manually defined.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Specify the font family (Calibri) and size (13 points).
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Calibri";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 13f;

            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}