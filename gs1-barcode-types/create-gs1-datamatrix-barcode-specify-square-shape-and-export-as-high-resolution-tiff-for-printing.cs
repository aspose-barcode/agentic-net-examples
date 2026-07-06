// Title: Generate GS1 DataMatrix Barcode and Export as High‑Resolution TIFF
// Description: Demonstrates creating a GS1 DataMatrix barcode with a square shape and saving it as a 300 DPI TIFF image for printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixVersion classes to produce print‑ready barcodes. Developers often need to control barcode dimensions, resolution, and output format for high‑quality print media, making this a typical use case for Aspose.BarCode.
// Prompt: Create a GS1 DataMatrix barcode, specify square shape, and export as high‑resolution TIFF for printing.
// Tags: gs1, datamatrix, barcode, generation, tiff, high-resolution, printing, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode,
/// configures it as a square symbol, and saves it as a high‑resolution TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, sets printing‑ready parameters,
    /// and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Sample GS1 DataMatrix code text (Application Identifier 01 and 10)
        string codeText = "(01)12345678901231(10)ABC123";

        // Initialize the barcode generator for GS1 DataMatrix with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set high resolution (300 DPI) suitable for printing
            generator.Parameters.Resolution = 300f;

            // Configure DataMatrix to be square by selecting a square ECC200 version
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
            generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f;

            // Optional: define foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a high‑resolution TIFF image
            generator.Save("gs1_datamatrix.tiff");
        }

        Console.WriteLine("GS1 DataMatrix barcode generated successfully.");
    }
}