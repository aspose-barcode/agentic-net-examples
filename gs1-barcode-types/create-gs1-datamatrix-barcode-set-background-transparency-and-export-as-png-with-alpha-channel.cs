using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Example GS1 DataMatrix code text (AI 01 for GTIN)
        string codeText = "(01)01234567890128";

        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set background to transparent to keep alpha channel in PNG
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as PNG; transparency is preserved
            generator.Save("gs1datamatrix.png");
        }
    }
}