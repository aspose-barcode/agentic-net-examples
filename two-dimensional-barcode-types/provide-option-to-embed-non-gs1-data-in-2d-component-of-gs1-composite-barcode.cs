using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Linear (GS1) part and non‑GS1 2D part separated by '|'
        string linearPart = "(01)01234567890123";
        string twoDPart = "HelloWorld"; // non‑GS1 data for the 2D component
        string codetext = $"{linearPart}|{twoDPart}";

        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Linear component type
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // 2D component type (CC_A, CC_B, CC_C, or Auto)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            // Allow non‑GS1 data in the 2D component
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Set dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image
            generator.Save("gs1composite_non_gs1_2d.png");
        }
    }
}