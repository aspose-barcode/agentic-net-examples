// Title: Generate Code128 barcode with custom Calibri font and right-aligned text
// Description: Demonstrates how to create a Code128 barcode, set the code text font to Calibri 13 pt, and align the text to the right of the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include branding, labeling, and creating readable barcodes with specific font styling. Developers often need to adjust font family, size, and alignment to meet design requirements.
// Prompt: Create a barcode with custom text font Calibri, size 13 pt, and align text right of the barcode.
// Tags: code128, barcode generation, font customization, text alignment, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with custom text styling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies custom font settings, aligns the text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 with the data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable manual font mode to allow custom font size.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Calibri and size to 13 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Calibri";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 13f;

            // Align the code text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}