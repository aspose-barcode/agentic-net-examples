// Title: Generate Code128 Barcode with Anti-Aliasing and Export as PNG
// Description: Demonstrates creating a Code128 barcode, enabling anti‑aliasing, setting a high resolution, and saving it as a PNG image for clear on‑screen rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to configure rendering options such as anti‑aliasing and resolution. Typical use cases include producing high‑quality barcodes for web pages, mobile apps, or any UI where crisp visual quality is required. Developers often need to adjust image parameters and export formats, making this a reference for common barcode creation tasks.
// Prompt: Create a barcode, apply anti‑aliasing settings, and export as PNG for crisp screen display.
// Tags: code128, barcode generation, anti-aliasing, png, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, applies anti‑aliasing,
/// sets a high resolution, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Create the output directory if it does not already exist
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Text to encode in the barcode
        string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable anti‑aliasing for smoother rendering
            generator.Parameters.UseAntiAlias = true;

            // Set a high resolution (300 DPI) for crisp screen display
            generator.Parameters.Resolution = 300f;

            // Define the module (bar) width in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}