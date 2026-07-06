// Title: Generate barcode with hidden main text and visible top caption
// Description: This example creates a Code128 barcode where the main human‑readable text is hidden and a caption is displayed above the barcode to convey supplemental information.
// Category-Description: Demonstrates Aspose.BarCode generation features for customizing human‑readable text and adding captions. It uses BarcodeGenerator, EncodeTypes, and the Parameters hierarchy (CodeTextParameters, CaptionAbove) to control visibility, positioning, and styling of text elements. Ideal for developers needing to embed supplemental data without cluttering the barcode itself, such as product codes with additional notes.
// Prompt: Generate barcodes with hidden main text and visible top caption to display supplemental information only.
// Tags: code128, barcode, caption, hiddentext, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode with the main text hidden and a visible caption above it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates and saves a barcode image with customized text settings.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode_with_caption.png";

        // Initialize a BarcodeGenerator for Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Hide the default human‑readable text that would appear below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "Supplemental Information";
            generator.Parameters.CaptionAbove.Visible = true; // Ensure the caption is displayed.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Color.Black;

            // Optional: set the barcode's foreground and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the image has been saved.
        Console.WriteLine($"Barcode image saved to {outputPath}");
    }
}