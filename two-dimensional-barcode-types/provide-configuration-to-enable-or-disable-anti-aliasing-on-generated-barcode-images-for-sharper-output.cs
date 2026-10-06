// Title: Demonstrate enabling and disabling anti-aliasing for barcode images
// Description: Shows how to configure the UseAntiAlias property of Aspose.BarCode to generate PNG barcodes with sharper or standard rendering.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to control rendering options such as anti‑aliasing. It uses the BarcodeGenerator class and its Parameters property, which are commonly employed when creating barcodes for web, print, or mobile applications. Developers often need to toggle anti‑aliasing to balance visual quality and file size.
// Prompt: Provide configuration to enable or disable anti‑aliasing on generated barcode images for sharper output.
// Tags: barcode, anti-aliasing, code128, png, aspnet, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to enable and disable anti‑aliasing when generating barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two Code128 barcodes: one with anti‑aliasing enabled (default) and one with it disabled.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeAntiAliasDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);

        // Text to encode in the barcode
        string codeText = "Sample123";

        // Generate barcode with anti-aliasing enabled (default)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable anti-aliasing for smoother edges
            generator.Parameters.UseAntiAlias = true;
            // Build the full file path for the output image
            string filePath = Path.Combine(outputDir, "barcode_aa_enabled.png");
            // Save the barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
            // Inform the user where the file was saved
            Console.WriteLine($"Saved with anti-aliasing enabled: {filePath}");
        }

        // Generate barcode with anti-aliasing disabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Disable anti-aliasing for a sharper, pixelated look
            generator.Parameters.UseAntiAlias = false;
            // Build the full file path for the output image
            string filePath = Path.Combine(outputDir, "barcode_aa_disabled.png");
            // Save the barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
            // Inform the user where the file was saved
            Console.WriteLine($"Saved with anti-aliasing disabled: {filePath}");
        }
    }
}