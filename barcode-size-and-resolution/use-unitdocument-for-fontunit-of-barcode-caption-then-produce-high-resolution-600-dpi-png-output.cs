using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.Visible = true;

            generator.Parameters.Resolution = 600f;

            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            generator.Save("barcode_600dpi.png");
        }
    }
}