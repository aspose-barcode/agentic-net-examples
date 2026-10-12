// Title: Set independent colors for top and bottom captions in a barcode image
// Description: Demonstrates how to assign different text colors to the above and below captions of a barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with captions. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a Code128 barcode, add top and bottom captions, and control their visual properties such as visibility, text, font size, and color. Developers often need to tailor barcode labels for branding or informational purposes, making caption styling a common requirement.
// Prompt: Change the caption color independently for top and bottom captions in the same barcode image.
// Tags: barcode, caption, color, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with separate colored captions above and below the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures caption properties, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Determine the output folder path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full file path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "BarcodeWithCaptions.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the top caption (above the barcode).
            generator.Parameters.CaptionAbove.Visible = true;          // Make the caption visible.
            generator.Parameters.CaptionAbove.Text = "Top Caption";   // Set caption text.
            generator.Parameters.CaptionAbove.TextColor = Color.Red; // Set caption text color.
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f; // Set font size.

            // Configure the bottom caption (below the barcode).
            generator.Parameters.CaptionBelow.Visible = true;          // Make the caption visible.
            generator.Parameters.CaptionBelow.Text = "Bottom Caption"; // Set caption text.
            generator.Parameters.CaptionBelow.TextColor = Color.Blue; // Set caption text color.
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;   // Set font size.

            // Save the barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}