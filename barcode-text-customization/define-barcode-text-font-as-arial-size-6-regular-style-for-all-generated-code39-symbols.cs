// Title: Generate Code39 Barcode with Custom Arial Font
// Description: Demonstrates how to generate a Code39 barcode and set the barcode text font to Arial, size 6, regular style.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include creating printable labels, inventory tags, and product identifiers where specific font styling is required. Developers often need to control font family, size, and style to meet branding or regulatory guidelines.
// Prompt: Define barcode text font as Arial, size 6, regular style for all generated Code39 symbols.
// Tags: code39, barcode, font, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code39 barcode image with custom font settings and saves it to a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies Arial font (size 6, regular), and writes the PNG file path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "code39.png");

        // Initialize the barcode generator for Code39 symbology with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Enable manual font mode to allow custom font specifications
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family, style, and size (Arial, regular, 6 points)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Regular;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 6f;

            // Save the barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Code39 barcode saved to: {outputPath}");
    }
}