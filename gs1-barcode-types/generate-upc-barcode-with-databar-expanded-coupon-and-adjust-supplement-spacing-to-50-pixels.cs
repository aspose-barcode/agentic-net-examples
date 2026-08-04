// Title: Generate UPC‑A barcode with DataBar Expanded coupon and custom supplement spacing
// Description: Demonstrates how to create a UPC‑A barcode that includes a GS1 DataBar Expanded coupon and set the supplement spacing to 50 pixels.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon. It illustrates typical retail scenarios where a UPC‑A barcode is combined with a DataBar Expanded coupon, and developers often need to adjust supplement spacing for proper scanning. The key API classes include BarcodeGenerator, EncodeTypes, and the Barcode parameters hierarchy.
// Prompt: Generate a UPC‑A barcode with a DataBar Expanded coupon and adjust supplement spacing to 50 pixels.
// Tags: upc-a, databar expanded, supplement spacing, png, barcodegenerator, encode types

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a UPC‑A barcode containing a GS1 DataBar Expanded coupon
/// and customizes the supplement spacing before saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, configures supplement spacing,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "upc_databar_coupon.png";

        // UPC‑A data (12 digits) followed by DataBar Expanded coupon data enclosed in parentheses.
        // The format complies with GS1 specifications for combined barcode types.
        string codeText = "514141100906(8110)106141416543213500110000310123196000";

        // Initialize the barcode generator for the UPC‑A with GS1 DataBar Expanded coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, codeText))
        {
            // Adjust the spacing between the main barcode and the supplement (coupon) to 50 pixels.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 50f;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}