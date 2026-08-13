// Title: Demonstrate enabling and disabling anti-aliasing for barcode images
// Description: Shows how to configure the UseAntiAlias property of Aspose.BarCode to generate barcode PNGs with sharper or pixelated rendering.
// Category-Description: This example belongs to the Aspose.BarCode image rendering category, illustrating how to control image quality using the BarcodeGenerator.Parameters.UseAntiAlias property. Developers working with barcode generation often need to balance visual clarity and file size, and this snippet demonstrates typical use cases for toggling anti‑aliasing when saving barcodes to PNG format.
// Prompt: Provide configuration to enable or disable anti‑aliasing on generated barcode images for sharper output.
// Tags: barcode, anti-aliasing, image rendering, code128, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Code128 barcodes with anti‑aliasing enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two PNG barcode images demonstrating the effect of the UseAntiAlias setting.
    /// </summary>
    static void Main()
    {
        // Generate a barcode with anti‑aliasing enabled for smoother edges
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable anti‑aliasing
            generator.Parameters.UseAntiAlias = true;

            // Save the image as PNG
            generator.Save("barcode_anti_alias_enabled.png");
        }

        // Generate a barcode with anti‑aliasing disabled for a crisper, pixelated look
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable anti‑aliasing
            generator.Parameters.UseAntiAlias = false;

            // Save the image as PNG
            generator.Save("barcode_anti_alias_disabled.png");
        }

        // Inform the user that the images have been created
        Console.WriteLine("Barcode images generated with anti‑aliasing enabled and disabled.");
    }
}