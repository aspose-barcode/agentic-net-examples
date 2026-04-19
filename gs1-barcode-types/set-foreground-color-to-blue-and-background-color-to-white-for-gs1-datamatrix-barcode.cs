using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample GS1 DataMatrix code text (Application Identifier 01 with a 14‑digit GTIN)
        string codeText = "(01)12345678901231";

        // Create a barcode generator for GS1 DataMatrix
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image
            generator.Save("gs1datamatrix.png");
        }
    }
}