using System;
using System.Collections.Generic;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        var ean13Codes = new List<string>
        {
            "1234567890128",
            "4006381333931",
            "5901234123457"
        };

        int index = 1;
        foreach (var code in ean13Codes)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, code))
            {
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                string fileName = $"ean13_{index}.png";
                generator.Save(fileName);
                Console.WriteLine($"Saved {fileName}");
            }

            index++;
        }
    }
}