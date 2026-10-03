// Title: Generate barcode with hidden main text and visible top caption
// Description: Demonstrates how to create a Code128 barcode where the primary code text is hidden and a supplemental caption is displayed above the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionAbove settings. Typical use cases include adding descriptive labels or supplemental information without showing the encoded value directly. Developers often need to customize text visibility, positioning, and styling when generating barcodes for reports or packaging.
// Prompt: Generate barcodes with hidden main text and visible top caption to display supplemental information only.
// Tags: barcode, code128, hidden text, caption, png, aspose.barcode, generation, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with hidden main text and a visible top caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// hides the main code text, adds a top caption, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Barcode data and caption text
        string barcodeValue = "1234567890";
        string captionText = "Supplemental Information";

        // Initialize the barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeValue))
        {
            // Hide the automatically generated barcode text (code value)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the caption that appears above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = captionText;
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Color.Black;

            // Save the resulting image as PNG
            string outputPath = Path.Combine(outputDir, "BarcodeWithCaption.png");
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode image generated successfully.");
    }
}