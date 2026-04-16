using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a MaxiCode barcode generator with simple codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test"))
        {
            // Set a custom margin of 10 pixels on all sides.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the generated barcode image to a file.
            generator.Save("maxicode.png");
        }
    }
}