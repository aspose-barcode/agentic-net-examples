// Title: Create barcode with custom colors in a single step
// Description: Demonstrates how to generate a Code128 barcode image with custom bar, background, text, and caption colors using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and its Parameters property to customize visual aspects such as bar color, background, code text color, and captions. Developers often need to tailor barcode appearance for branding or UI integration, and this snippet shows the typical API calls for setting colors and saving to PNG.
// Prompt: Create a barcode with custom bar, background, text, and caption colors in a single generation step.
// Tags: code128, barcode, color customization, png, aspose.barcode, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors and captions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as PNG.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Prepare a temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "custom_colors.png");

        // Initialize the barcode generator for Code128 with the desired value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set background color
            generator.Parameters.BackColor = Color.LightGray;

            // Set bar (foreground) color
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Set barcode text (code text) color
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.DarkRed;

            // Configure top caption
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Green;

            // Configure bottom caption
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Brown;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}