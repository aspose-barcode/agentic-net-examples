using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "123456";

            // Reduce the bar width by 20 percent (0.2 points as a relative reduction)
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.2f;

            // Save the barcode image
            generator.Save("barcode.png");
        }
    }
}