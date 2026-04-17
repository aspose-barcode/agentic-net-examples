using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample GS1 DataMatrix code text (AI 01 for GTIN)
        const string codeText = "(01)12345678901231";

        // Create the barcode generator for GS1 DataMatrix
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Ensure the DataMatrix is square by setting aspect ratio to 1
            generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f;

            // Optionally select a specific square symbol size (e.g., 20x20 modules)
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Set a high resolution suitable for printing (e.g., 300 DPI)
            generator.Parameters.Resolution = 300;

            // Save the barcode as a high‑resolution TIFF file
            generator.Save("gs1_datamatrix.tif");
        }
    }
}