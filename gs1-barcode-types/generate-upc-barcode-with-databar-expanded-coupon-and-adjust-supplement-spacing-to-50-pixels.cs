// Title: Generate UPC‑A Barcode with DataBar Expanded Coupon and Custom Supplement Spacing
// Description: This example creates a UPC‑A barcode that includes a DataBar Expanded coupon and sets the supplement spacing to 50 pixels.
// Category-Description: Demonstrates Aspose.BarCode generation of composite barcodes using EncodeTypes.UpcaGs1DatabarCoupon. It showcases configuring the Coupon.SupplementSpace property, a common requirement when integrating UPC‑A with DataBar Expanded for retail coupons. Developers working with composite symbologies, supplement adjustments, and image output will find this pattern useful.
// Prompt: Generate a UPC‑A barcode with a DataBar Expanded coupon and adjust supplement spacing to 50 pixels.
// Tags: upc-a, databar expanded, supplement spacing, barcode generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode containing a DataBar Expanded coupon
/// and customizes the supplement spacing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures supplement spacing,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for UPC‑A with a DataBar Expanded coupon
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon))
        {
            // Define the composite code text (UPC‑A part followed by DataBar part)
            generator.CodeText = "514141100906(8110)106141416543213500110000310123196000";

            // Adjust the supplement (coupon) spacing to 50 pixels
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 50f;

            // Save the generated barcode image to a file
            generator.Save("upc_a_databar_coupon.png");
        }
    }
}