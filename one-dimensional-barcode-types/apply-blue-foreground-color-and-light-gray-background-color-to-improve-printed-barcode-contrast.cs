// Title: Apply custom foreground and background colors to a barcode image
// Description: Demonstrates how to set a blue foreground color and a light‑gray background color for a Code128 barcode using Aspose.BarCode, then save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance with color properties. It uses the BarcodeGenerator class and its Parameters to modify BarColor and BackColor, a common requirement when improving print contrast or matching branding guidelines. Developers often need to adjust these settings for various output formats such as PNG, JPEG, or PDF.
// Prompt: Apply blue foreground color and light‑gray background color to improve printed barcode contrast.
// Tags: barcode, color, foreground, background, code128, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with custom foreground and background colors and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies color settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890".
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode (foreground) color to blue to enhance visual contrast.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background color to light gray for better readability on print.
            generator.Parameters.BackColor = Color.LightGray;

            // Save the configured barcode as a PNG file at the specified location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}