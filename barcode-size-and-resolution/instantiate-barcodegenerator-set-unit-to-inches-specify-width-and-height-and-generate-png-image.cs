using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "123ABC";

            // Set image size using inches
            generator.Parameters.ImageWidth.Inches = 2f;
            generator.Parameters.ImageHeight.Inches = 1f;

            // Save the barcode as a PNG image
            generator.Save("barcode.png");
        }
    }
}