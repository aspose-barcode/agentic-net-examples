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

            // Configure image size in millimeters
            generator.Parameters.ImageHeight.Millimeters = 30f; // BarCodeHeight = 30 mm
            generator.Parameters.ImageWidth.Millimeters = 50f;  // BarCodeWidth = 50 mm

            // Save the barcode as a JPEG file
            generator.Save("barcode.jpg");
        }
    }
}