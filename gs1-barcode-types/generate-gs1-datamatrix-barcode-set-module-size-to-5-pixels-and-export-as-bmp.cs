using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // GS1 DataMatrix code text (example with GTIN Application Identifier)
        string codeText = "(01)12345678901231";

        // Create a barcode generator for GS1 DataMatrix
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module (X‑Dimension) size to 5 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Export the barcode as a BMP image
            generator.Save("gs1datamatrix.bmp");
        }
    }
}