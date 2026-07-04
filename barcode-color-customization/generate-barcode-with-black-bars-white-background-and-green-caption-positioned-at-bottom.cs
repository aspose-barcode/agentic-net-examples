// Title: Generate Code128 barcode with custom colors and caption
// Description: Demonstrates creating a Code128 barcode with black bars, white background, and a green caption placed below the barcode.
// Prompt: Generate a barcode with black bars, white background, and green caption positioned at the bottom.
// Tags: code128, barcode, caption, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with black bars, white background,
    /// and a green caption displayed beneath the barcode, then saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode's bar color to black and the background to white
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Enable and configure the caption that appears below the barcode
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Sample Caption";
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionBelow.TextColor = Color.Green;

            // Save the generated barcode image to a PNG file named "barcode.png"
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been successfully created
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}