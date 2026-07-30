// Title: Generate a Code128 barcode with custom colors and caption
// Description: Demonstrates creating a barcode with custom bar, background, text, and caption colors in a single generation step.
// Category-Description: This example belongs to the Aspose.BarCode color customization category, showcasing how to use BarcodeGenerator, BarcodeParameters, and related classes to set bar, background, code text, and caption colors. Typical use cases include branding, UI integration, and printing where visual styling of barcodes is required. Developers often need to apply custom colors to match corporate design guidelines.
// Prompt: Create a barcode with custom bar, background, text, and caption colors in a single generation step.
// Tags: code128, color customization, png, barcodegenerator, parameters, caption

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors and a caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "custom_barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the color of the barcode bars to blue.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color of the image to yellow.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Set the human‑readable text (code text) color to green.
            generator.Parameters.Barcode.CodeTextParameters.Color = Aspose.Drawing.Color.Green;

            // Enable and configure a caption displayed above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Purple;

            // Save the generated barcode image as a PNG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been successfully generated.
        Console.WriteLine($"Barcode generated and saved to '{outputPath}'.");
    }
}