using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Example UPC‑A code with GS1‑128 coupon part.
        const string codeText = "514141100906(8102)03";

        // Create the barcode generator for UPC‑A with GS1‑128 coupon.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Define supplement space of 30 pixels between the main barcode and the coupon part.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30f;

            // Save the generated barcode image.
            generator.Save("upc_gs1_coupon.png");
        }
    }
}