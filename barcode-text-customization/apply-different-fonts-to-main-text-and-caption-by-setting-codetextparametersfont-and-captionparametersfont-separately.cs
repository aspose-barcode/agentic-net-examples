// Title: Apply Different Fonts to Barcode Text and Caption
// Description: Demonstrates creating a Code128 barcode, assigning a custom font to the barcode's code text and a separate font to an above caption, then saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance using the BarcodeGenerator, CodeTextParameters, and CaptionParameters classes. Typical use cases include branding, readability enhancements, and meeting design guidelines for printed or digital barcodes. Developers often need to set distinct fonts for the barcode data and accompanying captions to match corporate style guides.
// Prompt: Apply different fonts to main text and caption by setting CodetextParameters.Font and CaptionParameters.Font separately.
// Tags: code128, font, caption, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying separate fonts to barcode code text and caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with custom fonts and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.png");
        string dir = Path.GetDirectoryName(outputPath);

        // Ensure the output directory exists
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Create a barcode generator for Code128 with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the main barcode text font
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Enable and configure the caption above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}