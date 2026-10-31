// Title: Generate Code128 barcode with custom colors and bottom caption
// Description: Creates a Code128 barcode image with black bars on a white background and adds a green caption below the barcode.
// Category-Description: This example demonstrates basic barcode generation using Aspose.BarCode. It shows how to set barcode and background colors, configure caption visibility, text, color, and font, and save the result as a PNG image. Developers working with Aspose.BarCode often need to customize visual appearance of barcodes for branding or UI integration.
// Prompt: Generate a barcode with black bars, white background, and green caption positioned at the bottom.
// Tags: code128, barcode generation, color customization, caption, png, aspose.barcode, aspose.drawing

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
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "barcode.png";

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode bar color to black
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Set the image background color to white
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Enable and configure the caption displayed below the barcode
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Green Caption";
            generator.Parameters.CaptionBelow.TextColor = Aspose.Drawing.Color.Green;
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}