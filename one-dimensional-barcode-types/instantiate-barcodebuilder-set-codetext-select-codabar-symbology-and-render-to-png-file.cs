using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a BarcodeGenerator for Codabar symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the text to be encoded
            generator.CodeText = "A123456A";

            // Save the generated barcode as a PNG file
            generator.Save("codabar.png");
        }
    }
}