using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set individual paddings (points).
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Set image resolution (dpi) if desired.
            generator.Parameters.Resolution = 96;

            // Save the barcode image.
            generator.Save("datamatrix.png");
        }

        Console.WriteLine("DataMatrix barcode generated with custom paddings.");
    }
}