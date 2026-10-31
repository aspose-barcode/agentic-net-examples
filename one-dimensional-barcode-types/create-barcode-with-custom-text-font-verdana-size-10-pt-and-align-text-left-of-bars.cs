// Title: Generate Code128 Barcode with Verdana Font and Left Text Alignment
// Description: Demonstrates creating a Code128 barcode, applying a custom Verdana 10‑pt font to the human‑readable text, and aligning the text to the left of the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode text appearance using the BarcodeGenerator class and its CodeTextParameters. Typical use cases include branding, readability enhancements, and meeting design guidelines where specific fonts and text positioning are required. Developers often need to adjust font family, size, and alignment to match corporate style guides.
// Prompt: Create a barcode with custom text font Verdana, size 10 pt, and align text left of the bars.
// Tags: code128, barcode generation, custom font, text alignment, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with custom font settings and left‑aligned text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_font_barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable manual font mode to allow custom font configuration.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Verdana and size to 10 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Align the human‑readable text to the left side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}