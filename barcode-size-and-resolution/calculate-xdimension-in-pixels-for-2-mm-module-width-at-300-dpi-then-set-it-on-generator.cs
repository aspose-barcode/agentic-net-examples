using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // 2 mm module width converted to pixels at 300 dpi
        float millimeters = 2f;
        float inches = millimeters / 25.4f;          // 1 inch = 25.4 mm
        float dpi = 300f;
        float xDimensionPixels = inches * dpi;      // ≈ 23.622 px

        // Create a barcode generator (Code128 used as an example)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123456";

            // Set the calculated XDimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Save the barcode image
            generator.Save("barcode.png");
        }
    }
}