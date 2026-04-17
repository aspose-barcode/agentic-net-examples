using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Linear component (GS1 Code 128) – example GTIN
        string linearPart = "(01)12345678901231";

        // 2D component – AI 11 (production date) in YYMMDD format
        string productionDate = DateTime.Now.ToString("yyMMdd");
        string twoDPart = $"(11){productionDate}";

        // Combine parts with the required '|' separator
        string fullCodeText = $"{linearPart}|{twoDPart}";

        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCodeText))
        {
            // Set linear component type
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set 2D component type (MicroPDF417 variant CC-A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Enforce GS1 encoding for the 2D component
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = true;

            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image
            generator.Save("gs1_composite.png");
        }
    }
}