// Title: Generate Barcode with Custom Foreground and Background Colors
// Description: Demonstrates how to create a barcode image with specific bar, background, and text colors using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize the visual appearance of barcodes. It uses the BarcodeGenerator class along with its Parameters property to set bar color, background color, and code text color. Typical use cases include branding, UI integration, and producing print‑ready images where color schemes must match corporate guidelines.
// Prompt: Implement method to generate barcode with custom foreground and background colors for branding purposes.
// Tags: barcode symbology, generation, custom colors, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode with custom colors for branding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with blue bars, light gray background, and black text, then saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "custom_color_barcode.png");

        // Barcode content and symbology
        string codeText = "AsposeBrand";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Initialize the barcode generator with the chosen symbology and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the bar (foreground) color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image
            generator.Parameters.BackColor = Color.LightGray;

            // Set the color of the human‑readable code text
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Black;

            // Save the barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}