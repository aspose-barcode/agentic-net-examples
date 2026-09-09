// Title: Generate Code128 barcode with left-aligned main text and bottom caption
// Description: Demonstrates how to create a Code128 barcode where both the primary code text and a custom bottom caption are visible and left‑aligned.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionBelow settings. Developers often need to customize barcode text placement, alignment, and styling for labeling and packaging applications. The snippet shows typical steps for configuring text visibility, alignment, and font before saving the image.
// Prompt: Create barcodes where both main text and bottom caption are visible, each aligned left for consistency.
// Tags: code128, barcode, caption, text alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with visible main text and a left‑aligned bottom caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures text and caption alignment, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string filePath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Make the main code text visible and position it below the barcode, left‑aligned
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Configure the bottom caption: make it visible, set its text, left‑align it, and define font size
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;

            // Save the barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {filePath}");
    }
}