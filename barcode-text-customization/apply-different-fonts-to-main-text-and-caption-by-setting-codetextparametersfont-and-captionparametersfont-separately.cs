// Title: Apply distinct fonts to barcode text and top caption
// Description: Demonstrates how to set separate fonts for the main barcode text and a caption using Aspose.BarCode's CodeTextParameters and CaptionParameters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating font customization for barcode elements. It showcases the use of BarcodeGenerator, CodeTextParameters, and CaptionParameters to control typography, a common requirement when integrating barcodes into printed materials or UI designs. Developers often need to differentiate barcode data from accompanying labels, and this snippet provides a clear pattern for doing so.
// Prompt: Apply different fonts to main text and caption by setting CodetextParameters.Font and CaptionParameters.Font separately.
// Tags: barcode, code128, font, caption, generation, png, aspose.barcode, codetextparameters, captionparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting distinct fonts for barcode code text and a top caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with custom fonts for the code text and caption, then saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeWithFonts.png");

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a barcode generator for Code128 symbology with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable manual font mode for the barcode's code text
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set a custom font for the main barcode text (different from the caption)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Configure the top caption appearance
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Courier";
            generator.Parameters.CaptionAbove.Font.Size.Point = 10f;
            generator.Parameters.CaptionAbove.Font.Style = FontStyle.Bold;

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}