// Title: Custom Barcode Generation with Colored Elements
// Description: Demonstrates creating a Code128 barcode with custom bar, background, text, and caption colors in a single generation step.
// Prompt: Create a barcode with custom bar, background, text, and caption colors in a single generation step.
// Tags: barcode, code128, color, caption, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode with customized colors for the bars,
/// background, text, and an additional caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image with custom visual settings.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the color of the barcode bars to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to yellow.
            generator.Parameters.BackColor = Color.Yellow;

            // Set the color of the human‑readable text (code text) to green.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green;

            // Enable and configure a caption displayed above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Custom Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG file.
            generator.Save("custom_barcode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated successfully.");
    }
}