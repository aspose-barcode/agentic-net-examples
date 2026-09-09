// Title: Generate Code128 barcode with Verdana font and left-aligned text
// Description: Demonstrates how to create a Code128 barcode, set a custom Verdana 10 pt font for the human‑readable text, and align the text to the left of the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include branding, labeling, and creating readable barcodes with specific font styles. Developers often need to adjust font, size, and alignment to meet design requirements.
// Prompt: Create a barcode with custom text font Verdana, size 10 pt, and align text left of the bars.
// Tags: code128, barcode generation, font customization, text alignment, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a Code128 barcode with custom font settings and left-aligned text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Determine output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable manual font mode to allow custom font settings
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
            // Set the font family to Verdana
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            // Set the font size to 10 points
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            // Align the human‑readable text to the left of the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}