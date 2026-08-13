// Title: Generate high‑resolution barcode image for glossy label printing
// Description: Demonstrates how to create a Code128 barcode image at 600 DPI, suitable for high‑quality glossy label output.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and resolution settings. Typical use cases include producing printable barcodes for product labels, packaging, and inventory tags where high resolution and color control are required. Developers often need to adjust DPI, anti‑aliasing, and colors to meet printing specifications.
// Prompt: Generate a barcode image at 600 DPI resolution for high‑quality glossy label printing.
// Tags: barcode, code128, resolution, dpi, png, aspose.barcode, image generation, anti-aliasing, color

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode image at 600 DPI for high‑quality glossy label printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures resolution, colors, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample code text.
        using (Aspose.BarCode.Generation.BarcodeGenerator generator = new Aspose.BarCode.Generation.BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 600 DPI for high‑quality printing.
            generator.Parameters.Resolution = 600f;

            // Enable anti‑aliasing to improve visual smoothness.
            generator.Parameters.UseAntiAlias = true;

            // Configure bar (foreground) and background colors suitable for glossy labels.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image as PNG (lossless format) with the specified resolution.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode image saved to {outputPath}");
    }
}