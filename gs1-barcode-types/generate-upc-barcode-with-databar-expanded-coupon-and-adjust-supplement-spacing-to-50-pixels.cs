// Title: Generate UPC‑A DataBar Expanded Coupon barcode with custom supplement spacing
// Description: Demonstrates creating a UPC‑A barcode that includes a DataBar Expanded coupon and setting the supplement spacing to 50 pixels.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon. It shows configuring barcode parameters such as X‑Dimension and supplement spacing, common tasks for developers creating retail coupons or product labels that require precise layout control.
// Prompt: Generate a UPC‑A barcode with a DataBar Expanded coupon and adjust supplement spacing to 50 pixels.
// Tags: upc-a, databar expanded, coupon, supplement spacing, barcode generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a DataBar Expanded coupon and custom supplement spacing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path for the generated PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "UpcA_DatabarCoupon.png");

        try
        {
            // Initialize the barcode generator with the UPC‑A DataBar Expanded coupon symbology and data
            using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
            {
                // Set the X‑Dimension (module width) to 2 pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Adjust the supplement spacing to 50 pixels as required
                generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 50f;

                // Save the generated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}