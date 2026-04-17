using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string outputFile = "gs1code128.jpg";
        const string gs1Data = "(01)12345678901231";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Data))
        {
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            generator.Parameters.CaptionBelow.Text = "Custom Human‑Readable Text";
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center;

            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Barcode saved to {outputFile}");
    }
}