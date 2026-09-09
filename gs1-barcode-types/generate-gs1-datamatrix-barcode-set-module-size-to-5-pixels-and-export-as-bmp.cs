// Title: Generate GS1 DataMatrix barcode with custom module size and BMP output
// Description: Demonstrates creating a GS1 DataMatrix barcode, setting the module (X‑dimension) size to 5 pixels, and saving the image as a BMP file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. It shows configuring barcode parameters such as XDimension and exporting the result in BMP format, a common requirement for applications that need high‑resolution, device‑independent barcode images.
// Prompt: Generate a GS1 DataMatrix barcode, set module size to 5 pixels, and export as BMP.
// Tags: gs1datamatrix, barcode, generation, xdimension, bmp, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode, configuring its module size,
/// and saving it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and writes the output file.
    /// </summary>
    static void Main(string[] args)
    {
        // Define the barcode content (GS1 Application Identifier 01 with GTIN)
        string codeText = "(01)12345678901231";

        // Set the output file path
        string outputPath = "gs1_datamatrix.bmp";

        // Initialize the generator with GS1 DataMatrix symbology and the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module size (X dimension) to 5 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}