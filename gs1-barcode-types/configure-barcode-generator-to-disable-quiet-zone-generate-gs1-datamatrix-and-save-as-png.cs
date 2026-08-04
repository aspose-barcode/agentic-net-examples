// Title: Generate GS1 DataMatrix barcode without quiet zone and save as PNG
// Description: Demonstrates how to create a GS1 DataMatrix barcode using Aspose.BarCode, configure resolution, and save the image as PNG. The quiet zone remains at its default because the GS1 DataMatrix standard mandates its presence.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of EncodeTypes, BarcodeGenerator, and generator parameters. Typical use cases include creating GS1-compliant DataMatrix symbols for product identification, inventory tracking, and packaging. Developers often need to adjust resolution, format, and other settings while adhering to symbology standards.
// Prompt: Configure the barcode generator to disable the quiet zone, generate a GS1 DataMatrix, and save as PNG.
// Tags: gs1, datamatrix, barcode, generation, png, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix payload (GTIN‑14 in AI (01))
        string codeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // The quiet zone cannot be disabled for DataMatrix/GS1DataMatrix symbols
            // because the standard requires its presence. No quiet‑zone configuration is applied.

            // Optionally increase the image resolution (e.g., 300 DPI) for higher quality output
            generator.Parameters.Resolution = 300; // DPI

            // Save the generated barcode as a PNG image file
            generator.Save("gs1datamatrix.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("GS1 DataMatrix barcode generated: gs1datamatrix.png");
    }
}