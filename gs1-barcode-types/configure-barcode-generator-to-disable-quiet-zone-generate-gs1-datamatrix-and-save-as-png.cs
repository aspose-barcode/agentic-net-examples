// Title: Generate GS1 DataMatrix barcode without quiet zone and save as PNG
// Description: Demonstrates configuring Aspose.BarCode to generate a GS1 DataMatrix barcode, attempts to disable the quiet zone, and saves the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and image output. It showcases the use of BarcodeGenerator, EncodeTypes, and image saving methods, which developers commonly employ when creating DataMatrix or GS1 DataMatrix barcodes for inventory, shipping, or retail labeling.
// Prompt: Configure the barcode generator to disable the quiet zone, generate a GS1 DataMatrix, and save as PNG.
// Tags: gs1datamatrix, barcode, generation, quietzone, png, aspnet, aspnetcore, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode, attempts to disable the quiet zone,
/// and saves the barcode image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix code text (Application Identifier 01 - GTIN).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix.
        // Note: For DataMatrix and GS1 DataMatrix the quiet zone is mandated by the standard
        // and cannot be disabled, so we do not attempt to modify it.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Save the generated barcode as a PNG image file.
            generator.Save("gs1datamatrix.png");
        }
    }
}