// Title: Generate a Code128 barcode with custom Comic Sans MS font and centered text
// Description: Demonstrates how to set the barcode text font to Comic Sans MS, size 11 pt, and align it centrally, then save the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include creating decorative labels, product tags, or marketing materials where font styling and text alignment are required. Developers often need to adjust font properties and alignment to match branding guidelines.
// Prompt: Set barcode text font to Comic Sans MS, size 11 pt, and align text centrally for decorative labels.
// Tags: code128, font, alignment, png, aspose.barcode, barcodegenerator, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting barcode text font and alignment using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode with custom font settings and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "decorative_label.png");

        // Initialize the barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable manual font mode to allow custom font settings.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Comic Sans MS.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Comic Sans MS";

            // Set the font size to 11 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 11f;

            // Align the barcode text to the center.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}