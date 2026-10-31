// Title: Apply blue foreground and light‑gray background to barcode
// Description: Demonstrates setting barcode foreground color to blue and background to light gray to improve printed contrast.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to customize visual appearance of generated barcodes using BarcodeGenerator, EncodeTypes, and color properties. Typical use cases include enhancing readability on printed media, adjusting contrast for scanners, and branding. Developers often need to modify BarColor and BackColor to match design requirements.
// Prompt: Apply blue foreground color and light‑gray background color to improve printed barcode contrast.
// Tags: barcode, code128, color, foreground, background, contrast, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors for improved contrast.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image with blue bars and a light‑gray background,
    /// saves it to a temporary folder, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode (foreground) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to a light gray (RGB 211,211,211)
            generator.Parameters.BackColor = Color.FromArgb(211, 211, 211);

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}