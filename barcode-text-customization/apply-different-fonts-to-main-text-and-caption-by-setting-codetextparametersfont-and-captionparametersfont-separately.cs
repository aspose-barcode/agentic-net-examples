// Title: Apply Different Fonts to Barcode Code Text and Caption
// Description: Demonstrates how to assign separate fonts to the main barcode text and an above caption using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and customization category. It showcases the use of BarcodeGenerator along with CodeTextParameters and CaptionParameters to style barcode elements. Developers often need to adjust fonts for readability or branding when creating barcodes for labels, packaging, or documentation.
// Prompt: Apply different fonts to main text and caption by setting CodetextParameters.Font and CaptionParameters.Font separately.
// Tags: code128, font, caption, png, barcodegenerator, codetextparameters, captionparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with custom fonts for the code text and an above caption,
/// then saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode appearance,
    /// and writes the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "BarcodeWithFonts.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the main code text (barcode value) to use a manual Helvetica font, size 12pt
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Enable a caption above the barcode, set its text, and apply a Courier font, size 14pt
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Courier";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;

            // Save the configured barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}