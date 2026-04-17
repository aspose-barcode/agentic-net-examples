using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Alphanumeric code text to encode
        string codeText = "ABC123XYZ";

        // Create a DataMatrix barcode generator with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Default settings produce a square-shaped DataMatrix with automatic sizing
            generator.Save("datamatrix.png");
        }
    }
}