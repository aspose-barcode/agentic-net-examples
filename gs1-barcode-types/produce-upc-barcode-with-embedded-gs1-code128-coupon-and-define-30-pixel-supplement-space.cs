// Title: Generate UPC-A barcode with GS1 Code128 coupon and supplement space
// Description: Creates a UPC‑A barcode that includes an embedded GS1 Code128 coupon and sets a 30‑pixel supplement area.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate composite barcodes, specifically the UPC‑A with GS1 Code128 coupon symbology. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and barcode parameter settings such as XDimension and Coupon.SupplementSpace. Developers working with retail or promotional barcodes can use this pattern to create composite barcodes for packaging, coupons, and inventory systems.
// Prompt: Produce a UPC‑A barcode with an embedded GS1 Code128 coupon and define 30‑pixel supplement space.
// Tags: upc-a,gs1,code128,coupon,barcode,aspnet,aspose.barcode,composite,barcode generation,png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with an embedded GS1 Code128 coupon
/// and a defined supplement space using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "UpcA_GS1Code128Coupon.png");

        // Create a BarcodeGenerator for the UPC‑A with GS1 Code128 coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(8102)03"))
        {
            // Set the X-dimension (module width) to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Define a 30‑pixel supplement space for the coupon part of the composite barcode.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30f;

            // Save the generated barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}