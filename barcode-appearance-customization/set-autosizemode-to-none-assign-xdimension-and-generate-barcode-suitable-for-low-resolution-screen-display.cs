using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Increase XDimension to make bars thicker, which helps on low‑resolution screens.
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // When AutoSizeMode is None, BarHeight must be set explicitly for 1D barcodes.
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode image.
            generator.Save("lowres_barcode.png");
        }

        Console.WriteLine("Barcode generated: lowres_barcode.png");
    }
}