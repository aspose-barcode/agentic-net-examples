// Title: Calculate XDimension in Pixels for 2 mm Module Width at 300 dpi
// Description: Demonstrates how to compute the X‑dimension (module width) in pixels for a 2 mm barcode module at 300 dpi and apply it to a barcode generator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode sizing by setting resolution and X‑dimension. It uses the BarcodeGenerator, EncodeTypes, and generation parameters classes, which are commonly employed when developers need precise physical dimensions for printed barcodes. Typical use cases include packaging, labeling, and compliance with industry standards that require exact module widths.
// Prompt: Calculate XDimension in Pixels for 2 mm module width at 300 dpi, then set it on generator.
// Tags: barcode, xdimension, resolution, dpi, code128, image, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that calculates the X‑dimension in pixels for a 2 mm module width at 300 dpi
/// and applies the value to a barcode generator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Performs the calculation, configures the generator, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Desired module (X‑dimension) width: 2 mm.
        // Convert millimetres to inches: 2 mm = 0.0787401575 inches.
        // At 300 dpi, pixels = inches × DPI ≈ 23.622 pixels.
        // Use the exact float value for maximum precision.
        const float xDimensionPixels = 23.622f;
        const float resolutionDpi = 300f;

        // Create a barcode generator for Code128 (any symbology could be used here).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the generator's resolution to match the DPI used in the calculation.
            generator.Parameters.Resolution = resolutionDpi;

            // Apply the calculated X‑dimension in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Example codetext to encode.
            generator.CodeText = "1234567890";

            // Save the generated barcode as a PNG image.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated with the specified settings.
        Console.WriteLine(
            "Barcode generated with XDimension = {0} pixels at {1} DPI.",
            xDimensionPixels,
            resolutionDpi);
    }
}