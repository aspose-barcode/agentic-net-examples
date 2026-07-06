// Title: Generate GS1 DataMatrix barcode with ECC200 and save as PNG
// Description: Demonstrates creating a GS1 DataMatrix barcode using Aspose.BarCode, setting error correction level ECC200, and exporting the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixEccType to configure error correction and save the result. Developers often need to generate GS1-compliant DataMatrix codes for product identification and require control over ECC levels for reliability.
// Prompt: Generate a GS1 DataMatrix barcode with error correction level 200 and export as PNG.
// Tags: gs1datamatrix, barcode generation, error correction, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode with ECC200 error correction
/// and saves it as a PNG image using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix code text (GTIN) to encode.
        const string codeText = "(01)12345678901231";

        // Specify the output file path for the generated PNG image.
        const string outputPath = "gs1datamatrix.png";

        // Initialize the barcode generator for GS1 DataMatrix with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the DataMatrix error correction level to ECC200 (value 200).
            generator.Parameters.Barcode.DataMatrix.DataMatrixEcc = DataMatrixEccType.Ecc200;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been successfully saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to '{outputPath}'.");
    }
}