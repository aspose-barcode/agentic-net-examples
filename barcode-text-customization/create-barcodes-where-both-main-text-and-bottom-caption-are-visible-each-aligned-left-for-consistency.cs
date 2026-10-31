// Title: Generate Code128 barcode with left-aligned main text and bottom caption
// Description: Demonstrates how to create a Code128 barcode where both the primary code text and a custom bottom caption are visible and left‑aligned.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionBelow settings. Developers often need to customize barcode text placement and alignment for branding or labeling requirements. The snippet shows typical steps for configuring text visibility, alignment, and saving the image.
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
    /// Entry point. Generates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodesDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "BarcodeWithCaption.png");

        // Create a barcode generator for Code128 with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Ensure main text is displayed below the barcode and aligned left
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Configure bottom caption: make it visible and left aligned
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Left;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}