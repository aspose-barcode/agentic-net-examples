// Title: Generate barcode with hidden main text and visible top caption
// Description: Demonstrates creating a Code128 barcode where the encoded value is hidden and a supplemental caption is displayed above the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using BarcodeGenerator, CodeTextParameters, and Caption settings. Typical scenarios include adding descriptive text, branding, or additional information without showing the raw barcode data. Developers often need to hide the main code text while providing a readable caption for end‑users.
// Prompt: Generate barcodes with hidden main text and visible top caption to display supplemental information only.
// Tags: code128, caption, hidden text, png, aspose.barcode, generation, barcodeimageformat, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with hidden main text and a visible top caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, configures the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine output directory path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting image file
        string outputPath = Path.Combine(outputDir, "BarcodeWithCaption.png");

        // Initialize barcode generator with Code128 symbology and data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the main barcode text (code value)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the caption that appears above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Supplemental Information";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Color.Black;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}