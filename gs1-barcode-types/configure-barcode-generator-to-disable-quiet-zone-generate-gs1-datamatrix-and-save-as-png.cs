using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample GS1 DataMatrix code text (AI (01) for GTIN)
        string codeText = "(01)12345678901231";

        // Create the barcode generator for GS1 DataMatrix
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Disable quiet zone (not applicable for DataMatrix in Aspose.BarCode,
            // but the property is set here for completeness if it becomes available)
            // generator.Parameters.Barcode.DataMatrix.QuietZone = false; // placeholder

            // Optional: adjust X-Dimension if needed
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the barcode as PNG
            generator.Save("gs1datamatrix.png");
        }
    }
}