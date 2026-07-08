// Title: Apply custom foreground and background colors to a Code128 barcode
// Description: Demonstrates how to set a blue foreground and light‑gray background for a Code128 barcode using Aspose.BarCode, improving visual contrast for printed labels.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating color customization with BarcodeGenerator and its Parameters properties. Developers often need to adjust barcode colors to match branding or enhance readability on various media. The snippet shows typical usage of EncodeTypes, BarCodeImageFormat, and color settings for printable barcode images.
// Prompt: Apply blue foreground color and light‑gray background color to improve printed barcode contrast.
// Tags: code128, color, png, barcodegenerator, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying custom foreground and background colors to a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with blue bars on a light‑gray background and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode (foreground) color to blue for better contrast
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode as a PNG file at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}