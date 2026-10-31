// Title: Enable or Disable Anti-Aliasing for Barcode Images
// Description: Demonstrates how to toggle the UseAntiAlias setting in Aspose.BarCode to produce barcode images with smoother or sharper edges.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize image rendering. Developers often need to adjust anti‑aliasing for visual quality, especially when generating PNG or JPEG barcodes for web or print. The snippet shows typical configuration steps for enabling or disabling anti‑aliasing in barcode image creation.
// Prompt: Provide configuration to enable or disable anti‑aliasing on generated barcode images for sharper output.
// Tags: barcode, anti-aliasing, generation, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates enabling and disabling anti‑aliasing when generating Code128 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates two PNG barcodes – one with anti‑aliasing enabled and one without.
    /// </summary>
    static void Main()
    {
        // Set up the output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Text to encode in the barcode
        string codeText = "Sample123";

        // Generate barcode with anti-aliasing enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable smoother edges by turning on anti-aliasing
            generator.Parameters.UseAntiAlias = true;
            string filePath = Path.Combine(outputDir, "Code128_AntiAlias.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved with anti-aliasing: {filePath}");
        }

        // Generate barcode with anti-aliasing disabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Disable anti-aliasing for a sharper, pixel‑perfect image
            generator.Parameters.UseAntiAlias = false;
            string filePath = Path.Combine(outputDir, "Code128_NoAntiAlias.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved without anti-aliasing: {filePath}");
        }
    }
}