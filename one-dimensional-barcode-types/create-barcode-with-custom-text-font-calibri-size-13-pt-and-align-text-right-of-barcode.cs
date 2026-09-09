// Title: Generate Code128 barcode with right-aligned Calibri text
// Description: Demonstrates how to create a Code128 barcode, set the human‑readable text to Calibri 13 pt, and align the text to the right of the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include branding, product labeling, and inventory systems where specific font styling and text placement are required. Developers often need to adjust font properties, alignment, and output formats when integrating barcodes into applications.
// Prompt: Create a barcode with custom text font Calibri, size 13 pt, and align text right of the barcode.
// Tags: code128, barcode generation, png, font, text alignment, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with custom text styling and alignment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies font settings, aligns the text, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Switch to manual font mode so we can specify a custom font
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Calibri and size to 13 points
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Calibri";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 13f;

            // Align the human‑readable text to the right side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}