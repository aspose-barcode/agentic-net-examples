using System;
using Aspose.BarCode.Generation;

namespace BarcodeSvgExample
{
    class Program
    {
        static void Main()
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
            {
                generator.Parameters.Barcode.Padding.Top.Point = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 10f;
                generator.Parameters.Barcode.Padding.Left.Point = 10f;
                generator.Parameters.Barcode.Padding.Right.Point = 10f;

                generator.Save("barcode.svg");
            }
        }
    }
}