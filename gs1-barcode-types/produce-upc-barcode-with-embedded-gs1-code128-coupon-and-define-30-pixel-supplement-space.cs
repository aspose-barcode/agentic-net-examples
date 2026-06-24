using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a UPC‑A barcode with a GS1‑Code128 coupon using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application that generates a UPC‑A barcode with an embedded GS1‑Code128 coupon.
    /// </summary>
    static void Main()
    {
        // Example UPC‑A barcode with an embedded GS1‑Code128 coupon.
        // Codetext format: "UPCA part (GS1Code128 part)"
        const string codeText = "514141100906(8102)03";

        // Create the generator for the UpcaGs1Code128Coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Define a 30‑pixel supplement (coupon) space.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 30f;

            // Save the barcode image to a file.
            const string outputPath = "upc_a_gs1_coupon.png";
            generator.Save(outputPath);

            // Inform the user where the barcode was saved.
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}