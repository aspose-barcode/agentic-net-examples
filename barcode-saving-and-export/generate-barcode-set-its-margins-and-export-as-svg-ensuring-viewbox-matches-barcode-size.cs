using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "1234567890"))
        {
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            generator.Parameters.Resolution = 96f;

            generator.Save("barcode.svg");
        }
    }
}