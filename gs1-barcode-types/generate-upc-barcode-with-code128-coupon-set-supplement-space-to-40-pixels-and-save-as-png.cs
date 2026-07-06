// Title: Generate UPC‑A barcode with Code128 coupon and supplement space
// Description: Demonstrates creating a UPC‑A barcode that includes a Code128 coupon, configuring the supplement spacing, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on combined symbologies such as UPC‑A with GS1‑128 coupons. It showcases the use of BarcodeGenerator, EncodeTypes, and the Coupon settings to customize supplement spacing. Developers often need to generate composite barcodes for retail and promotional applications, adjusting visual parameters before exporting to common image formats.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, set supplement space to 40 pixels, and save as PNG.
// Tags: upc-a, code128, coupon, supplement-space, png, generation, aspose.barcode, encode-types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a UPC‑A barcode with an embedded Code128 coupon,
/// sets the supplement (coupon) spacing, and saves the image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        const string outputPath = "upc_code128_coupon.png";

        // Sample codetext representing a UPC‑A value with a GS1‑128 coupon segment
        const string codeText = "514141100906(8102)03";

        // Initialize the barcode generator with the combined UPC‑A/GS1‑128 coupon symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Configure the supplement (coupon) space to 40 pixels
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 40f;

            // Save the generated barcode image in PNG format to the specified path
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved successfully
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}