// Title: Generate UPC-A barcode with GS1 Code128 coupon and 30-pixel supplement space
// Description: Demonstrates creating a UPC‑A barcode that includes an embedded GS1 Code128 coupon, setting a 30‑pixel supplement area.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon. Developers often need to embed coupons or supplemental data in UPC‑A barcodes for retail promotions; the key API classes include BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. The snippet illustrates setting X‑dimension, supplement space, and saving the image, a common workflow for generating printable barcode graphics.
// Prompt: Produce a UPC‑A barcode with an embedded GS1 Code128 coupon and define 30‑pixel supplement space.
// Tags: upc-a, gs1, code128, coupon, supplement space, barcode generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode with an embedded GS1 Code128 coupon
/// and a 30‑pixel supplement space, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures dimensions, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "UpcA_GS1Code128Coupon.png");

        // Initialize the barcode generator with the UPC‑A GS1 Code128 coupon symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(8110)ASPOSE"))
        {
            // Set the X-dimension (module width) to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Define a 30‑pixel supplement space for the coupon.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}