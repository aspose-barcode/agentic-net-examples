// Title: Generate Code128 barcode with caption and 600 dpi PNG output
// Description: This example creates a Code128 barcode, adds a caption using Document font units, and saves a high‑resolution 600 dpi PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with advanced rendering options. Shows how to configure resolution, image size, and caption properties using the BarcodeGenerator, Parameters, and related classes. Ideal for developers needing high‑quality barcode images for print or digital media.
// Prompt: Use Unit.Document for FontUnit of barcode caption, then produce high‑resolution 600 dpi PNG output.
// Tags: code128, caption, resolution, png, aspose.barcode, barcodegenerator, parameters, highresolution

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a caption and saves it as a 600 dpi PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the output resolution to 600 dpi for high‑quality rendering
            generator.Parameters.Resolution = 600f;

            // Enable interpolation mode for smoother scaling and define image dimensions in points
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Configure a caption to appear above the barcode
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            // Use Document units for the font size (12 points in Document units)
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode as a high‑resolution PNG file
            generator.Save("barcode.png");
        }
    }
}