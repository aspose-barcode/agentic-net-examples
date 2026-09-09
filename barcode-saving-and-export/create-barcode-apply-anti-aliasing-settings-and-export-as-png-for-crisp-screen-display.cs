// Title: Generate a Code128 barcode with anti-aliasing and export as PNG
// Description: Demonstrates creating a Code128 barcode, enabling anti‑aliasing, setting a high resolution, and saving it as a PNG image for clear on‑screen display.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure rendering options such as anti‑aliasing, resolution, and module size using the BarcodeGenerator class. Typical use cases include producing high‑quality barcodes for web pages, mobile apps, or any UI where crisp visual quality is required. Developers often need to adjust these settings to meet branding guidelines or improve scan reliability on screens.
// Prompt: Create a barcode, apply anti‑aliasing settings, and export as PNG for crisp screen display.
// Tags: barcode, code128, anti-aliasing, png, generation, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with anti‑aliasing, high resolution, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.png");
        // Text to encode in the barcode
        string codeText = "ASPOSE123";

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable anti‑aliasing for smoother on‑screen rendering
            generator.Parameters.UseAntiAlias = true;

            // Set a higher resolution (300 DPI) for crisp display
            generator.Parameters.Resolution = 300f;

            // Optional: adjust the module (X) dimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}