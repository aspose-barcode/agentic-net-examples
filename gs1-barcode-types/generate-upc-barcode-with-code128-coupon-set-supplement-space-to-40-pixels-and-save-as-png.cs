// Title: Generate UPC‑A barcode with Code128 coupon and supplement spacing
// Description: Demonstrates how to create a UPC‑A barcode that includes a GS1‑128 coupon supplement, configure the supplement spacing, and save the result as a PNG image.
// Category-Description: Examples of barcode generation using Aspose.BarCode, focusing on composite symbologies that combine a primary barcode with a supplemental component such as a GS1‑128 coupon. The code shows how to use the BarcodeGenerator class, set EncodeTypes, adjust barcode parameters like Coupon.SupplementSpace, and export to common image formats. Developers often need these patterns to embed promotional data or additional product information alongside standard barcodes.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, set supplement space to 40 pixels, and save as PNG.
// Tags: upc-a, code128, coupon, supplement-space, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that creates a UPC‑A barcode with a GS1‑128 coupon supplement,
/// configures the supplement spacing, and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file name.
        const string outputPath = "upc_a_coupon.png";

        // Initialize the barcode generator for the composite UPC‑A / GS1‑128 coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon))
        {
            // Set the barcode text: 12‑digit UPC‑A value followed by the Code128 supplement in parentheses.
            generator.CodeText = "514141100906(8102)03";

            // Configure the space (in points) between the main barcode and the supplement.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 40f;

            // Render and save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}