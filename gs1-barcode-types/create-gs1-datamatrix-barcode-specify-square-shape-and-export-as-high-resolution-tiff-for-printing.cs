// Title: Generate GS1 DataMatrix barcode and save as high‑resolution TIFF
// Description: Demonstrates creating a GS1 DataMatrix barcode with a square shape and exporting it as a 300 dpi TIFF image suitable for printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. It shows setting barcode parameters such as resolution and DataMatrix version to produce a square symbol, then saving the result in a high‑resolution TIFF format. Developers working on printing, packaging, or inventory systems often need to generate GS1 DataMatrix barcodes for compliance and readability, and this snippet provides a concise reference.
// Prompt: Create a GS1 DataMatrix barcode, specify square shape, and export as high‑resolution TIFF for printing.
// Tags: gs1, datamatrix, barcode, generation, tiff, high-resolution, printing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode and saves it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting TIFF file.
        string outPath = Path.Combine(outputDir, "GS1DataMatrix.tiff");

        // GS1 DataMatrix payload (example GTIN with AI (01)).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the image resolution to 300 DPI for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Choose a square DataMatrix version (32x32 modules) to enforce a square shape.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

            // Save the barcode as a TIFF image.
            generator.Save(outPath, BarCodeImageFormat.Tiff);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}