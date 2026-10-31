// Title: Generate UPC‑A barcode with Code128 coupon and custom supplement space
// Description: Demonstrates creating a UPC‑A barcode that includes a Code128 coupon, configuring the supplement spacing, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon. It illustrates setting barcode parameters such as X‑Dimension and coupon supplement space, common tasks for developers needing combined symbologies for retail and promotional applications. Typical use cases include generating product barcodes with attached coupon data for printing or digital distribution.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, set supplement space to 40 pixels, and save as PNG.
// Tags: upc-a, code128, coupon, supplement-space, png, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with an embedded Code128 coupon,
/// configuring supplement spacing, and saving the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, saves it, and reports the result.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "upc_a_coupon.png");
        try
        {
            // Initialize generator with UPC‑A GS1 Code128 coupon symbology and data string
            using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "012345678905(8102)03"))
            {
                // Set the X‑Dimension (module width) to 2 pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Set the supplement space (gap between main barcode and coupon) to 40 pixels
                generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 40f;
                // Save the generated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
            // Inform the user where the file was saved
            Console.WriteLine("Barcode saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or saving
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}