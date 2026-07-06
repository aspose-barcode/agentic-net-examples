// Title: Apply 10‑pixel margin to GS1 Code 128 barcode and save as JPEG
// Description: Demonstrates how to generate a GS1 Code 128 barcode, add a uniform 10‑pixel margin, and export it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode padding parameters. Typical use cases include creating GS1‑compliant barcodes for product labeling with custom margins for better readability. Developers often need to adjust padding, size, and output format when integrating barcodes into documents or images.
// Prompt: Apply a 10‑pixel margin around a GS1 Code 128 barcode and save as JPEG.
// Tags: gs1, code128, barcode, margin, padding, jpeg, aspose.barcode, generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates a GS1 Code 128 barcode, applies a 10‑pixel margin on all sides, and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures padding, and writes the JPEG file.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code 128 data (AI (01) for GTIN)
        const string codeText = "(01)12345678901231";

        // Initialize the barcode generator with the GS1 Code 128 symbology and the sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Apply a uniform 10‑pixel margin (padding) around the barcode
            generator.Parameters.Barcode.Padding.Left.Pixels   = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels    = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode as a JPEG image file
            generator.Save("gs1code128.jpg");
        }
    }
}