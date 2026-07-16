// Title: Enable or disable anti-aliasing for barcode images
// Description: Demonstrates how to configure the Aspose.BarCode generator to turn anti‑aliasing on or off, producing sharper or pixelated barcode images.
// Category-Description: This example belongs to the Aspose.BarCode image rendering configuration category. It shows usage of the BarcodeGenerator class and its Parameters property to control rendering options such as UseAntiAlias and Resolution. Developers often need to adjust these settings to optimize barcode clarity for different display or printing requirements.
// Prompt: Provide configuration to enable or disable anti‑aliasing on generated barcode images for sharper output.
// Tags: barcode symbology, anti-aliasing, image rendering, code128, aspnet, aspose.barcode, generation, resolution, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates enabling and disabling anti‑aliasing when generating Code128 barcodes with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images: one with anti‑aliasing enabled and one with it disabled.
    /// </summary>
    static void Main()
    {
        // Generate a barcode with anti‑aliasing enabled for smoother edges.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AntiAliasOn"))
        {
            // Turn on anti‑aliasing to improve visual quality.
            generator.Parameters.UseAntiAlias = true;
            // Set a higher resolution (dots per inch) for sharper output.
            generator.Parameters.Resolution = 300f;
            // Save the image as PNG.
            generator.Save("barcode_anti_alias_on.png");
        }

        // Generate a barcode with anti‑aliasing disabled for a crisp, pixelated appearance.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AntiAliasOff"))
        {
            // Turn off anti‑aliasing to retain sharp pixel edges.
            generator.Parameters.UseAntiAlias = false;
            // Keep the same resolution for a fair comparison.
            generator.Parameters.Resolution = 300f;
            // Save the image as PNG.
            generator.Save("barcode_anti_alias_off.png");
        }

        // Inform the user that the process has completed.
        Console.WriteLine("Barcode images generated successfully.");
    }
}