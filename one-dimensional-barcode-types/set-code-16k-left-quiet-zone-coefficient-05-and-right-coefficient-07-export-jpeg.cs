using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K))
        {
            generator.CodeText = "12345678901234567890";

            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 10;

            generator.Save("code16k.jpg");
        }
    }
}