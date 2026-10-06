// Title: Generate UPC-A barcode with DataBar Expanded coupon and custom supplement spacing
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 DataBar Expanded coupon and setting the supplement spacing to 50 pixels. The barcode is saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on UPC‑A and GS1 DataBar symbologies. It shows how to configure barcode parameters such as X‑dimension and supplement spacing using the BarcodeGenerator and its Parameters API. Developers often need to generate retail barcodes with coupons or additional data, and this snippet illustrates the typical setup for such use cases.
// Prompt: Generate a UPC‑A barcode with a DataBar Expanded coupon and adjust supplement spacing to 50 pixels.
// Tags: upc-a, databar expanded, supplement spacing, barcode generation, aspose.barcode, png output

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
    /// Entry point that creates the barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutput");
        Directory.CreateDirectory(outputDir);

        // Build full output file path
        string outputPath = Path.Combine(outputDir, "UpcA_DatabarCoupon.png");

        // Initialize barcode generator with specific symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Set X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Set supplement spacing to 50 pixels
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 50;

            // Save barcode as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform user of saved location
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}