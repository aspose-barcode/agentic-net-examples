using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Calculate XDimension in pixels for a 2 mm module width at 300 dpi
            // 1 inch = 25.4 mm, so pixels = (mm * dpi) / 25.4
            float xDimensionPixels = 2f * 300f / 25.4f; // ≈ 23.62 pixels

            // Apply the calculated XDimension
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Set the text to encode
            generator.CodeText = "123456";

            // Save the barcode image
            generator.Save("barcode.png");
        }
    }
}