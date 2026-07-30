// Title: Generate Code128 barcode with custom colors and bottom caption
// Description: Demonstrates creating a Code128 barcode with black bars, white background, and a green caption placed below the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using BarcodeGenerator, EncodeTypes, and rendering parameters such as BarColor, BackColor, and CaptionBelow. Typical use cases include branding, product labeling, and adding descriptive text to barcodes. Developers often need to adjust colors, fonts, and caption placement to match visual design requirements.
// Prompt: Generate a barcode with black bars, white background, and green caption positioned at the bottom.
// Tags: code128, barcode generation, color customization, caption, png, aspose.barcode, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors and a bottom caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures colors and caption, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode bars (foreground) to black
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Set the image background to white
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Configure the caption that appears below the barcode
            generator.Parameters.CaptionBelow.Text = "Sample Caption";               // Caption text
            generator.Parameters.CaptionBelow.TextColor = Aspose.Drawing.Color.Green; // Caption color
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";               // Font family
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;                  // Font size
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center;      // Center alignment
            generator.Parameters.CaptionBelow.Visible = true;                         // Make caption visible

            // Define the output file path and save the barcode as PNG
            string outputPath = "barcode.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}