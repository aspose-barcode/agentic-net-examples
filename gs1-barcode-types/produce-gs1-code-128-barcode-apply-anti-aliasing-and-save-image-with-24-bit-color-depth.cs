// Title: Generate GS1 Code 128 barcode with anti-aliasing and 24-bit PNG output
// Description: Demonstrates creating a GS1 Code 128 barcode, applying anti-aliasing for smooth rendering, and saving it as a 24‑bit PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. It shows typical tasks such as setting symbology, configuring rendering options like anti‑aliasing, and specifying colors before saving to a common image format. Developers looking for barcode creation, image quality tuning, and format-specific output will find similar examples useful.
// Prompt: Produce a GS1 Code 128 barcode, apply anti‑aliasing, and save the image with 24‑bit color depth.
// Tags: gs1, code128, barcode, generation, anti-aliasing, png, 24-bit, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// applies anti‑aliasing for smoother rendering, and saves it as a 24‑bit PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures rendering options,
    /// and writes the output file.
    /// </summary>
    static void Main()
    {
        // GS1 Code 128 requires AI parentheses, e.g., (01) for GTIN
        const string gs1Code128Text = "(01)12345678901231";

        // Create a generator for the GS1 Code 128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Code128Text))
        {
            // Enable anti‑aliasing for smoother rendering
            generator.Parameters.UseAntiAlias = true;

            // Set foreground (bars) and background colors – default PNG is 24‑bit
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode as a PNG file (24‑bit color depth)
            generator.Save("gs1code128.png");
        }
    }
}