using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample UPC‑A with DataBar Expanded coupon data.
        const string codeText = "514141100906(8110)106141416543213500110000310123196000";

        // Create the barcode generator for the UPC‑A DataBar Expanded coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, codeText))
        {
            // Adjust the space between the main barcode and its supplement to 50 pixels.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 50f;

            // Save the generated barcode image.
            const string outputFile = "upc_a_databar_coupon.png";
            generator.Save(outputFile);
            Console.WriteLine($"Barcode saved to {outputFile}");
        }
    }
}