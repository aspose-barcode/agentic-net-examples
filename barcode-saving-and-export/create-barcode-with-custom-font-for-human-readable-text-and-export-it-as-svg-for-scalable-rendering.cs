using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "CustomFont123";

            var font = generator.Parameters.Barcode.CodeTextParameters.Font;
            font.FamilyName = "Courier New";
            font.Size.Point = 20f;

            generator.Save("custom_font_barcode.png");
        }
    }
}