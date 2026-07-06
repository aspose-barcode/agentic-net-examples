// Title: Generate GS1 DataMatrix barcode with custom module size and BMP output
// Description: Demonstrates creating a GS1 DataMatrix barcode, setting its module size to 5 pixels, and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. Typical use cases include encoding product identifiers (GTIN) and lot numbers for supply‑chain labeling, where developers need to control visual parameters like X‑dimension and export to raster formats such as BMP.
// Prompt: Generate a GS1 DataMatrix barcode, set module size to 5 pixels, and export as BMP.
// Tags: gs1datamatrix, barcode generation, module size, bmp, aspose.barcode, csharp

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode,
/// configures the module size, and saves the result as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix code text (GTIN and lot number) using Application Identifiers.
        const string codeText = "(01)12345678901231(10)LOT123";

        // Initialize the barcode generator for GS1 DataMatrix with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module (X‑dimension) size to 5 pixels to control barcode density.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode image as a BMP file.
            generator.Save("gs1datamatrix.bmp");
        }
    }
}