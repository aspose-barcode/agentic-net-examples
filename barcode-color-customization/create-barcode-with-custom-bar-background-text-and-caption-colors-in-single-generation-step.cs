// Title: Generate a Code128 barcode with custom colors and caption
// Description: This example shows how to create a Code128 barcode while customizing the bar, background, text, and caption colors in a single generation step.
// Category-Description: Aspose.BarCode generation examples demonstrate how to use the BarcodeGenerator class along with EncodeTypes, BarCodeImageFormat, and related parameter objects to produce barcodes with tailored appearance. Typical scenarios include branding, UI integration, and printed media where color and caption styling are required. Developers often need to adjust bar colors, background, text, and caption properties to match design guidelines.
/// Prompt: Create a barcode with custom bar, background, text, and caption colors in a single generation step.
/// Tags: code128, barcode, generation, color, caption, png, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with custom colors and a caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_barcode.png");

        // Initialize the barcode generator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the bar (foreground) color.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the barcode image.
            generator.Parameters.BackColor = Color.White;

            // Set the color of the human‑readable text (code text).
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Red;

            // Configure the caption displayed above the barcode.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Green;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated at: {outputPath}");
    }
}