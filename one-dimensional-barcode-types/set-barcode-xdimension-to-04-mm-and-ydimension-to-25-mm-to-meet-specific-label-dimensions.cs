using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the X‑dimension (smallest bar/space width) to 0.4 mm.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.4f;

            // Set the Y‑dimension (bar height) to 25 mm.
            generator.Parameters.Barcode.BarHeight.Millimeters = 25f;

            // Save the generated barcode image to a file.
            generator.Save("barcode.png");
        }
    }
}