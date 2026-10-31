// Title: Independent Caption Colors for PDF417 Barcode
// Description: Demonstrates how to set different text colors for the top and bottom captions of a PDF417 barcode and save it as a PNG image.
// Category-Description: Shows Aspose.BarCode generation techniques for customizing barcode captions. This example belongs to the barcode appearance customization category, illustrating use of BarcodeGenerator, EncodeTypes, and caption parameters (CaptionAbove, CaptionBelow) to control visibility, text, and color. Developers often need to tailor caption styling for branding or UI requirements.
// Prompt: Change the caption color independently for top and bottom captions in the same barcode image.
// Tags: pdf417, caption, color, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode with separate colors for the top and bottom captions,
/// then saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures caption colors,
    /// and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output image file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "CaptionColor.png");

        // Initialize the barcode generator with PDF417 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleCode"))
        {
            // Optional: adjust barcode dimensions for better readability.
            generator.Parameters.Barcode.Pdf417.Rows = 12;          // Set number of rows.
            generator.Parameters.Barcode.XDimension.Pixels = 2;   // Set module size.

            // Configure the top caption: make it visible, set text, and apply green color.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Green;

            // Configure the bottom caption: make it visible, set text, and apply blue color.
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Blue;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"Barcode with independent caption colors saved to: {outputPath}");
    }
}