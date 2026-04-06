using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "CUSTOM123"))
        {
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            var ctParams = generator.Parameters.Barcode.CodeTextParameters;
            ctParams.Location = CodeLocation.Above;
            ctParams.Alignment = TextAlignment.Center;
            ctParams.Space.Point = 5f;
            ctParams.Font.Size.Point = 12f;
            ctParams.Font.Style = FontStyle.Bold;
            ctParams.Color = Color.Blue;

            var padding = generator.Parameters.Barcode.Padding;
            padding.Top.Point = 10f;
            padding.Bottom.Point = 10f;
            padding.Left.Point = 15f;
            padding.Right.Point = 15f;

            generator.Save("custom_barcode.png");
        }
    }
}