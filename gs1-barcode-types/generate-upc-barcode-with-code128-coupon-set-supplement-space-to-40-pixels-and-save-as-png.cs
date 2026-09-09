// Title: Generate UPC‑A barcode with Code128 coupon and supplement space
// Description: Creates a UPC‑A barcode that includes a Code128 coupon, sets the supplement space to 40 pixels, and saves the result as a PNG image.
// Category-Description: This example demonstrates Aspose.BarCode generation for retail and promotional use cases. It utilizes the BarcodeGenerator class with EncodeTypes.UpcaGs1Code128Coupon to embed a coupon in a UPC‑A symbology. Developers often need to customize supplement spacing, output formats, and file handling when creating barcodes for point‑of‑sale systems, marketing materials, or inventory tracking.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, set supplement space to 40 pixels, and save as PNG.
// Tags: upc-a,code128,coupon,supplement-space,png,barcode-generation,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a UPC‑A barcode with an embedded Code128 coupon,
/// configure the supplement space, and save the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "UpcA_Code128Coupon.png");

        // Create a BarcodeGenerator for the UPC‑A with GS1 Code128 coupon symbology,
        // using the provided data string that includes the coupon information.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(8110)ASPOSE"))
        {
            // Set the supplement (coupon) space to 40 pixels.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 40;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}