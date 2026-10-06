// Title: Generate UPC‑A barcode with GS1 Code128 coupon and supplement space
// Description: Creates a UPC‑A barcode that includes an embedded GS1 Code128 coupon and defines a 30‑pixel supplement area.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation for retail and promotional use cases. It utilizes the EncodeTypes.UpcaGs1Code128Coupon symbology, the BarcodeGenerator class, and related parameter objects to configure dimensions and supplemental space. Developers often need to embed coupon data within standard product barcodes and control visual layout for printing.
// Prompt: Produce a UPC‑A barcode with an embedded GS1 Code128 coupon and define 30‑pixel supplement space.
// Tags: upc-a, gs1, code128, coupon, supplement-space, barcode-generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a UPC‑A barcode with an embedded GS1 Code128 coupon
/// and a custom supplement space using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "UpcGs1Code128Coupon.png");

        try
        {
            // Initialize the barcode generator with the UPC‑A GS1 Code128 coupon symbology
            // and the data string that includes the coupon information.
            using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "123456789012(8110)1234"))
            {
                // Set the X‑dimension (module width) to 2 pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Define a 30‑pixel supplement space for the coupon portion.
                generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}