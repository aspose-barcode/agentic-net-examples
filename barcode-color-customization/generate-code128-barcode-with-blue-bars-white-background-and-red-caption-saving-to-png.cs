// Title: Generate Code128 barcode with custom colors and caption
// Description: Creates a Code128 barcode with blue bars, white background, and a red caption above, then saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to customize barcode appearance using the BarcodeGenerator class. It covers setting bar and background colors, adding a caption, and exporting to PNG—common tasks for developers creating branded or visually distinct barcodes for packaging, labeling, and inventory systems.
// Prompt: Generate a Code128 barcode with blue bars, white background, and red caption, saving to PNG.
// Tags: code128, barcode, generation, color, caption, png, aspose.barcode, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode with custom colors and a caption,
/// then saving the result as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name.
        string outputPath = "code128.png";

        // Initialize the barcode generator with Code128 symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the bar (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to white.
            generator.Parameters.BackColor = Color.White;

            // Enable and configure a caption placed above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}