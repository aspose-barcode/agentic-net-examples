// Title: Generate GS1 DataMatrix Barcode and Export as BMP
// Description: Demonstrates creating a GS1 DataMatrix barcode with a specific module size and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. It covers setting barcode parameters such as XDimension (module size) and exporting the result to a bitmap file. Developers working with product identification, inventory systems, or any scenario requiring GS1 DataMatrix symbols will find this pattern useful for creating compliant barcodes programmatically.
// Prompt: Generate a GS1 DataMatrix barcode, set module size to 5 pixels, and export as BMP.
// Tags: gs1, datamatrix, barcode, generation, module-size, bmp, aspose.barcode, aspose.barcode.generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a GS1 DataMatrix barcode,
/// configures its module size, and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix payload: Application Identifier (01) + 14‑digit GTIN
        string codeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the module (X‑dimension) size to 5 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a BMP file
            generator.Save("gs1_datamatrix.bmp");
        }
    }
}