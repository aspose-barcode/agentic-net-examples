// Title: Generate Code128 barcode with custom Verdana font and left-aligned text
// Description: This example creates a Code128 barcode, sets the human‑readable text to Verdana 10 pt, and aligns the text to the left of the bars.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on customizing the barcode's code text appearance. It uses the BarcodeGenerator, EncodeTypes, and CodeTextParameters classes to modify font family, size, and alignment—common tasks when branding or meeting layout requirements. Ideal for developers needing to produce barcodes with tailored text styling in PNG, JPEG, or other image formats.
// Prompt: Create a barcode with custom text font Verdana, size 10 pt, and align text left of the bars.
// Tags: barcode, code128, textfont, alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with custom text font settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies Verdana 10 pt font, left-aligns the text, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable manual font mode to allow custom font settings.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Verdana.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";

            // Set the font size to 10 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Align the human‑readable text to the left side of the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}