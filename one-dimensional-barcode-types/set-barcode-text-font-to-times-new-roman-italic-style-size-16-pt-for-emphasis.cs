// Title: Set custom font for barcode text using Aspose.BarCode
// Description: Demonstrates how to configure the barcode text font to Times New Roman, italic style, 16 pt, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Developers often need to adjust text fonts for branding, readability, or design compliance; this snippet shows the typical steps for setting manual font mode, selecting a font family, style, and size, then rendering the barcode to a common image format.
/// Prompt: Set barcode text font to Times New Roman, italic style, size 16 pt for emphasis.
// Tags: barcode, code128, font, text, aspose.barcode, png, generation

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
    /// Generates a barcode image with Times New Roman italic 16 pt text and saves it to a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file name.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable manual font mode so custom font settings are applied.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family, style, and size for the barcode text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Italic;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 16f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image.
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}