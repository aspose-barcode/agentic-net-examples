// Title: Generate GS1 DataMatrix barcode with custom module size and BMP output
// Description: Demonstrates creating a GS1 DataMatrix barcode, setting its module size to 5 pixels, and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. Typical use cases include encoding product identifiers for retail, logistics, and inventory systems where GS1 standards are required. Developers often need to customize visual parameters such as XDimension and export the barcode to common image formats like BMP for integration into documents or label printing workflows.
// Prompt: Generate a GS1 DataMatrix barcode, set module size to 5 pixels, and export as BMP.
// Tags: gs1datamatrix, barcode, generation, module size, bmp, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode, configuring its module size, and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode (GS1 Application Identifier 01 with a 14‑digit GTIN)
        string codeText = "(01)12345678901231";

        // Output file path for the generated BMP image
        string outputPath = "gs1_datamatrix.bmp";

        // Initialize the barcode generator with GS1 DataMatrix symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module (X‑dimension) size to 5 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the barcode image in BMP format
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to {outputPath}");
    }
}