using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image size using inches
            generator.Parameters.ImageWidth.Inches = 2f;
            generator.Parameters.ImageHeight.Inches = 1f;

            // Save the barcode as a PNG file
            generator.Save("barcode.png");
        }
    }
}