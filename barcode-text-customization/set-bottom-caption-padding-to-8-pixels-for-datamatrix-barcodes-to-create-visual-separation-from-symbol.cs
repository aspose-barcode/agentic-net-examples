using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix))
        {
            // Set the data to encode
            generator.CodeText = "123456";

            // Set bottom caption padding to 8 pixels to create visual separation
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 8f;

            // Save the barcode image
            generator.Save("datamatrix.png");
        }
    }
}