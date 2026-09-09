// Title: Apply custom colors to a Code128 barcode image
// Description: Demonstrates setting a blue foreground and light‑gray background for a Code128 barcode, improving print contrast.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance using the BarcodeGenerator and its Parameters properties. Developers often need to adjust bar and background colors for better readability on various media, and this snippet shows the typical API usage for such visual customizations.
// Prompt: Apply blue foreground color and light‑gray background color to improve printed barcode contrast.
// Tags: code128, color, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying custom foreground and background colors to a Code128 barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode with blue bars on a light‑gray background and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG barcode image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}