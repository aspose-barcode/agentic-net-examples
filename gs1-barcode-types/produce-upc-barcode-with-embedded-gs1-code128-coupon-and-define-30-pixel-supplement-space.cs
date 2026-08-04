// Title: Generate UPC‑A barcode with GS1 Code128 coupon and supplement space
// Description: Demonstrates creating a UPC‑A barcode that includes an embedded GS1 Code128 coupon and sets a 30‑pixel supplement spacing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon. It illustrates typical retail scenarios where a UPC‑A product barcode is combined with a GS1 Code128 coupon, and how to control the visual layout by defining supplement space. Developers working with barcode creation, coupon integration, or custom barcode formatting will find this pattern useful.
/// Prompt: Produce a UPC‑A barcode with an embedded GS1 Code128 coupon and define 30‑pixel supplement space.
/// Tags: upc-a, gs1, code128, coupon, supplement, barcode, generation, aspose.barcode, image

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a UPC‑A barcode containing a GS1 Code128 coupon
/// and configures a 30‑pixel supplement space between the main barcode and its supplement.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text: UPC‑A part (12 digits) followed by GS1 Code128 coupon part.
        string codeText = "514141100906(8102)03";

        // Initialize the barcode generator for UPC‑A with an embedded GS1 Code128 coupon.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Set a 30‑pixel space between the main barcode and its supplement.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 30f;

            // Save the generated barcode image to a file.
            generator.Save("upc_coupon.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated: upc_coupon.png");
    }
}