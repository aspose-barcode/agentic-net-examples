// Title: Generate a Code128 barcode with anti‑aliasing and save as PNG
// Description: Demonstrates creating a Code128 barcode, enabling anti‑aliasing and high resolution for a crisp PNG image suitable for screen display.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure rendering options such as anti‑aliasing, resolution, and colors using the BarcodeGenerator class. Typical use cases include producing high‑quality barcodes for web pages, mobile apps, or UI components where visual clarity is essential. Developers often need to adjust these settings to meet design guidelines and ensure readability across devices.
// Prompt: Create a barcode, apply anti‑aliasing settings, and export as PNG for crisp screen display.
// Tags: code128, anti-aliasing, png, barcode generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, applies anti‑aliasing,
/// sets a high resolution, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "barcode.png";

        // Initialize the BarcodeGenerator with Code128 symbology and sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable anti‑aliasing to smooth edges and improve visual quality.
            generator.Parameters.UseAntiAlias = true;

            // Set a higher resolution (dots per inch) for a sharper image on screens.
            generator.Parameters.Resolution = 300f;

            // Optional: Define foreground (barcode) and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the configured barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}