// Title: Generate Code39 Barcode with Custom Arial Font
// Description: Demonstrates how to generate a Code39 barcode and set the barcode text font to Arial, 6 pt, regular style.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include creating printable labels, inventory tags, or any application where precise font control of barcode text is required. Developers often need to adjust font family, size, and style to match branding or regulatory specifications.
/// Prompt: Define barcode text font as Arial, size 6, regular style for all generated Code39 symbols.
/// Tags: code39, barcode, font, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code39 barcode image with a custom Arial font for the barcode text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, applies font settings, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the PNG image to be saved
        string outputPath = Path.Combine(outputDir, "code39.png");

        // Initialize the barcode generator for Code39 symbology with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Enable manual font mode so custom font settings are applied
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the barcode text font to Arial, regular style, 6 points
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Regular;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 6f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}