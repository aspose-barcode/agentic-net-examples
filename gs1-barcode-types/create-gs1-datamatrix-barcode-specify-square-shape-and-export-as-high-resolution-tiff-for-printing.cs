using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 DataMatrix barcode and saves it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 DataMatrix barcode with a sample GTIN and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Sample GS1 DataMatrix code text (GTIN with AI (01))
        string codeText = "(01)12345678901231";

        // Create the barcode generator for GS1 DataMatrix using the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Ensure a square DataMatrix by selecting a square version (20x20 modules)
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
            // Force the aspect ratio to 1:1 for a perfect square
            generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f;

            // Set a high resolution (e.g., 300 DPI) suitable for printing
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a high‑resolution TIFF image
            generator.Save("GS1DataMatrix.tiff");
        }

        // Inform the user that the barcode has been generated and saved
        Console.WriteLine("GS1 DataMatrix barcode generated and saved as GS1DataMatrix.tiff");
    }
}