using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set margin (padding) around the symbol – 10 points on each side
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: set bar color for visibility
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image
            generator.Save("DataMatrixWithMargin.png");
        }
    }
}