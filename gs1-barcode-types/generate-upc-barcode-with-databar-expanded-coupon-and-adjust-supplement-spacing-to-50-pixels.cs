using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a UPC‑A DataBar Expanded coupon barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the barcode content.
        // Format: <UPC‑A part>(<AI>... )<DataBar Expanded part>
        string codeText = "514141100906(8110)106141416543213500110000310123196000";

        // Specify the output file path for the generated PNG image.
        string outputPath = "upc_databar_expanded.png";

        // Initialize the barcode generator for UPC‑A DataBar Expanded coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, codeText))
        {
            // Set the spacing (in points) between the main barcode and its supplement.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 50f;

            // Render and save the barcode image as a PNG file.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}