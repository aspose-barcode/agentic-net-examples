using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Barcode with anti-aliasing enabled (sharper edges)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.UseAntiAlias = true;
            generator.Save("barcode_anti_alias.png");
        }

        // Barcode with anti-aliasing disabled (pixelated edges)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.UseAntiAlias = false;
            generator.Save("barcode_no_anti_alias.png");
        }
    }
}