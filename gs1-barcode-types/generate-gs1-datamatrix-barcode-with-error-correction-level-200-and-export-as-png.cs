// Title: Generate GS1 DataMatrix barcode with ECC200 and save as PNG
// Description: This example creates a GS1 DataMatrix barcode using Aspose.BarCode, sets the error correction level to ECC200, and exports the result as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for GS1 DataMatrix symbology, covering key API classes such as BarcodeGenerator, EncodeTypes, DataMatrixEccType, and BarCodeImageFormat. Typical use cases include encoding product identifiers for supply chain automation and printing high‑density machine‑readable labels. Developers working with barcode creation often need to configure dimensions, error correction, and output formats, making this example a useful reference for quick implementation.
// Prompt: Generate a GS1 DataMatrix barcode with error correction level 200 and export as PNG.
// Tags: gs1, datamatrix, barcode, generation, png, ecc200, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode with ECC200 error correction
/// and saves it as a PNG file using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, configures its parameters,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path where the PNG image will be saved.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1_datamatrix.png");

        // GS1 DataMatrix payload: (01) indicates a GTIN-14 identifier.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the DataMatrix error correction level to ECC200.
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Define the module size (X dimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image to the output path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}