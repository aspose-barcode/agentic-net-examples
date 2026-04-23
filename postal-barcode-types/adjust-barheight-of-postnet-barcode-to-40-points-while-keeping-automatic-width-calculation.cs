using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Postnet barcode generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Postnet))
        {
            // Set the data to encode
            generator.CodeText = "12345";

            // Adjust the bar height to 40 points (automatic width calculation remains)
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the barcode image to a file
            generator.Save("postnet.png");
        }
    }
}