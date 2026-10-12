// Title: Generate UPC‑A Barcode with Code128 Coupon and Supplement Space
// Description: Creates a UPC‑A barcode that includes a Code128 coupon, configures a 40‑pixel supplement space, and saves the result as a PNG image.
// Category-Description: This example demonstrates Aspose.BarCode generation for composite symbologies, specifically UPC‑A with an embedded Code128 coupon. It showcases the use of BarcodeGenerator, EncodeTypes, and the Coupon supplement settings to control spacing. Developers working with retail packaging, promotional coupons, or any scenario requiring combined barcode data can reference this pattern for creating and customizing such barcodes.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, set supplement space to 40 pixels, and save as PNG.
// Tags: upc-a, code128, coupon, supplement-space, png, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a UPC‑A barcode with an embedded Code128 coupon,
/// configure the supplement space, and save the image as PNG using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "UpcA_Code128_Coupon.png");

        // Initialize the barcode generator with the composite symbology and data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(8102)03"))
        {
            // Configure the supplement (coupon) space to 40 pixels.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 40f;

            // Render and save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}