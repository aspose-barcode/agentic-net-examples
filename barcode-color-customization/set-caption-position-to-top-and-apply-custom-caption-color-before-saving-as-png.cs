// Title: Set top caption and custom color for PDF417 barcode and save as PNG
// Description: Demonstrates how to place a caption above a PDF417 barcode, customize its text color, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to configure barcode appearance. It covers setting caption visibility, text, font, and color—common tasks when customizing barcodes for branding or instructional purposes. Developers often need to adjust these properties before rendering barcodes to various image formats.
// Prompt: Set the caption position to top and apply a custom caption color before saving as PNG.
// Tags: pdf417, caption, png, aspose.barcode, aspose.drawing, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a PDF417 barcode with a top caption,
/// applies a custom caption color, and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "BarcodeWithTopCaption.png");

        // Create a BarcodeGenerator for PDF417 symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleCodeText"))
        {
            // Enable the caption above the barcode and set its text
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Apply a custom green color to the caption text
            generator.Parameters.CaptionAbove.TextColor = Color.Green;

            // Optionally increase the caption font size for better readability
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;

            // Render and save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}