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
            generator.CodeText = "123456";

            // Set XDimension (smallest bar width) to 0.5 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Set Y dimension (image height) to 30 mm
            generator.Parameters.ImageHeight.Millimeters = 30f;

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}