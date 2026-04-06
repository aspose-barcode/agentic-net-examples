using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123456";
            // Auto-size is default; no need to set BarHeight to zero
            generator.Save("barcode.png");
        }
    }
}