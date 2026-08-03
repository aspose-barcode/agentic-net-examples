// Title: Save Code128 barcode as BMP with custom foreground color
// Description: Demonstrates generating a Code128 barcode and saving it as a BMP image while applying a custom bar color.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. Typical use cases include creating printable barcodes with brand‑specific colors for inventory, shipping, or retail applications. Developers often need to customize colors, formats, and symbologies before exporting images.
// Prompt: Save a Code128 barcode to a BMP file using a custom foreground color.
// Tags: code128, barcode, save, bmp, foreground color, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    /// <summary>
    /// Entry point for the barcode generation sample.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a Code128 barcode, applies a custom blue bar color, and saves it as a BMP file.
        /// </summary>
        static void Main()
        {
            // Define the output file path
            string outputPath = "code128.bmp";

            // Initialize the barcode generator with Code128 symbology and sample data
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Apply a custom foreground color to the bars
                generator.Parameters.Barcode.BarColor = Color.Blue;

                // Export the barcode to a BMP image file
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
            }

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}