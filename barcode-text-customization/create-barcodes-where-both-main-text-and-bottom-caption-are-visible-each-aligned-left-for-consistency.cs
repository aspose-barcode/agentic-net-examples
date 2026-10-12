// Title: Generate Code128 Barcode with Left-Aligned Text and Caption
// Description: Demonstrates how to create a Code128 barcode where the main barcode text and a custom bottom caption are both visible and left‑aligned.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, CodeTextParameters, and CaptionBelow to produce barcodes for labeling, packaging, and inventory applications. Developers often need to customize text placement, alignment, and visibility to meet branding or regulatory requirements. The snippet illustrates typical steps for configuring text and caption properties before saving the image.
// Prompt: Create barcodes where both main text and bottom caption are visible, each aligned left for consistency.
// Tags: code128, barcode generation, caption, left alignment, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with left‑aligned main text and a custom bottom caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode image and saves it to the Output folder.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the barcode image will be saved
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the barcode content and the caption text
        string codeText = "1234567890";
        string bottomCaption = "Sample Caption";
        string outputPath = Path.Combine(outputDir, "BarcodeWithCaption.png");

        // Create a BarcodeGenerator for Code128 with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure the main barcode text to appear below the bars and align it to the left
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Enable the bottom caption, set its text, and align it to the left
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = bottomCaption;
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Left;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}