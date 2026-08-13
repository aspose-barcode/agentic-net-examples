// Title: Generate GS1 DataMatrix barcode with ECC200 and save as PNG
// Description: Demonstrates creating a GS1 DataMatrix barcode using Aspose.BarCode, setting the error correction level to ECC200, and exporting the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on 2‑D symbologies such as DataMatrix. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixEccType to configure barcode parameters, a common requirement for developers who need to embed GS1‑compliant DataMatrix codes in packaging, inventory, or logistics applications. Typical use cases include generating high‑quality PNG images for printing or digital display.
// Prompt: Generate a GS1 DataMatrix barcode with error correction level 200 and export as PNG.
// Tags: gs1, datamatrix, barcode, generation, png, ecc200, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a GS1 DataMatrix barcode, configures ECC200 error correction,
/// and saves the barcode as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix payload: Application Identifier (01) followed by a 14‑digit GTIN.
        const string codeText = "(01)01234567890123";

        // Initialize the barcode generator for the GS1 DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the DataMatrix error correction level to ECC200 (standard level 200).
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Optional: increase the image resolution to 300 DPI for higher visual quality.
            generator.Parameters.Resolution = 300;

            // Save the generated barcode as a PNG image file.
            generator.Save("gs1datamatrix.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("GS1 DataMatrix barcode generated: gs1datamatrix.png");
    }
}