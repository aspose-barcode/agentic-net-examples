using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 (you can choose any 1D symbology)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable automatic scaling to fit a narrow width
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 150f;   // narrow width
            generator.Parameters.ImageHeight.Point = 50f;   // appropriate height

            // Center-align the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the barcode image
            generator.Save("barcode.png");
        }
    }
}