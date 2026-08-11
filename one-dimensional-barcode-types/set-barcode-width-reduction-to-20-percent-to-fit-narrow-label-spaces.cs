// Title: Apply 20% Width Reduction to a Code128 Barcode
// Description: Demonstrates how to generate a Code128 barcode and reduce its width by 20 percent using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. Typical use cases include creating compact barcodes for narrow label spaces, adjusting visual dimensions without altering encoded data, and exporting to common image formats. Developers often need to fine‑tune barcode size for printing constraints, and this snippet illustrates the standard approach.
// Prompt: Set barcode width reduction to 20 percent to fit narrow label spaces.
// Tags: code128, width reduction, barcode generation, png, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, applies a 20 percent width reduction, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures width reduction, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Define the data to encode in the barcode.
            generator.CodeText = "123456";

            // Apply a 20 percent width reduction (approximately 0.2 points).
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.2f;

            // Specify the output file name and format (PNG by default).
            string outputFile = "barcode.png";

            // Render and save the barcode image to disk.
            generator.Save(outputFile);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to '{outputFile}' with 20% width reduction.");
        }
    }
}