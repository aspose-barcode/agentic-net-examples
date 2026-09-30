// Title: Set caption position to top and customize caption color in barcode PNG
// Description: Demonstrates how to place a caption above a Code128 barcode and apply a custom text color before saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode appearance. Typical use cases include adding descriptive text above or below barcodes and styling the caption for branding or readability. Developers often need to adjust caption position, font, and color when integrating barcodes into reports, labels, or UI elements.
// Prompt: Set the caption position to top and apply a custom caption color before saving as PNG.
// Tags: code128, barcode, caption, color, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a top caption and custom caption color, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures caption settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the caption text that will appear above the barcode.
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Apply a custom color (blue) to the caption text.
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}