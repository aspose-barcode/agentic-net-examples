// Title: Generate UPC‑A barcode with embedded GS1 Code128 coupon and 30‑pixel supplement
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 Code128 coupon and sets a 30‑pixel supplement space.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on composite symbologies that combine UPC‑A with GS1‑Code128 coupons. It showcases the use of BarcodeGenerator, EncodeTypes, and coupon parameters to embed supplemental data, a common requirement for retail and promotional barcode creation. Developers often need to generate such combined barcodes for product labeling and coupon integration.
// Prompt: Produce a UPC‑A barcode with an embedded GS1 Code128 coupon and define 30‑pixel supplement space.
// Tags: upc-a, gs1-code128, coupon, supplement, barcode generation, aspnet, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a UPC‑A barcode with an embedded GS1‑Code128 coupon
/// and configures a 30‑pixel supplement space.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text.
        // Format: "UPCA part (GS1Code128 part)"
        const string codeText = "514141100906(8102)03";

        // Define the output file name.
        const string outputPath = "upc_gs1code128_coupon.png";

        // Initialize the generator for the composite UPC‑A with GS1‑Code128 coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Set the supplement (coupon) space to 30 pixels.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30f;

            // Save the generated barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}