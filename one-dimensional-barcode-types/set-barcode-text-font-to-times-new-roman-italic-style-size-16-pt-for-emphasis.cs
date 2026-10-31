// Title: Set barcode text font to Times New Roman italic 16pt
// Description: Demonstrates how to configure the barcode text font using Aspose.BarCode to Times New Roman, italic style, 16 pt, and save as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize code text appearance. It uses BarcodeGenerator, EncodeTypes, and CodeTextParameters classes to modify font properties such as family, style, and size. Typical use cases include branding, emphasizing barcode data, or matching corporate style guidelines. Developers often need to adjust font settings when generating barcodes for printed materials, reports, or UI displays.
// Prompt: Set barcode text font to Times New Roman, italic style, size 16 pt for emphasis.
// Tags: barcode, code128, font, times new roman, italic, size, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with custom text font settings and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures font properties for the barcode text and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 symbology with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample Text"))
        {
            // Enable manual control over the code text font.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Times New Roman.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";

            // Apply italic style to the font.
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Italic;

            // Set the font size to 16 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 16f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}