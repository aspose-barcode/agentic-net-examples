// Title: Generate a Code128 barcode with anti‑aliasing and save as PNG
// Description: Demonstrates creating a Code128 barcode, enabling anti‑aliasing, setting a high resolution, and exporting it as a PNG image for clear on‑screen rendering.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure rendering parameters such as anti‑aliasing and resolution using the BarcodeGenerator class. Typical use cases include generating barcodes for web or UI display where visual clarity is essential. Developers often need to adjust these settings to produce crisp images for screens or print.
// Prompt: Create a barcode, apply anti‑aliasing settings, and export as PNG for crisp screen display.
// Tags: code128, barcode generation, png, anti-aliasing, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with anti‑aliasing and PNG output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, applies anti‑aliasing, sets resolution, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Ensure the target directory exists before saving
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable anti‑aliasing to smooth the barcode edges
            generator.Parameters.UseAntiAlias = true;

            // Set a higher DPI (e.g., 300) for sharper on‑screen rendering
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}