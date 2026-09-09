// Title: Generate UPC‑A DataBar Expanded Coupon barcode with custom supplement spacing
// Description: Demonstrates how to create a UPC‑A barcode that includes a GS1 DataBar Expanded coupon and set the supplement spacing to 50 pixels. The resulting image is saved as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on UPC‑A and GS1 DataBar symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension and Coupon.SupplementSpace. Developers often need to generate retail barcodes with supplemental data for coupons or promotional offers, and this snippet illustrates the typical API calls required.
// Prompt: Generate a UPC‑A barcode with a DataBar Expanded coupon and adjust supplement spacing to 50 pixels.
// Tags: upc-a, databar expanded, coupon, supplement spacing, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a GS1 DataBar Expanded coupon and custom supplement spacing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build full output file path
        string outputPath = Path.Combine(outputDir, "UpcA_DatabarCoupon.png");

        // Initialize barcode generator with specific symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Set barcode module width (X-dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Adjust supplement spacing to 50 pixels
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 50f;

            // Save barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform user of saved file location
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}