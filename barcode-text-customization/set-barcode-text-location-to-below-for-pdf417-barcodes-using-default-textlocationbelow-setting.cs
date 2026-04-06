using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            generator.CodeText = "Sample PDF417 Text";
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Save("pdf417.png");
        }
    }
}