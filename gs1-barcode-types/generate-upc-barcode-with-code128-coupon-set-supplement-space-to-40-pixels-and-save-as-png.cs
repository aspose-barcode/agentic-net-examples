using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for UPC‑A with GS1‑128 coupon.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "514141100906(8102)03"))
        {
            // Set the space between the main barcode and the supplement to 40 pixels.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 40f;

            // Save the barcode image as PNG.
            generator.Save("upc_code128_coupon.png");
        }
    }
}