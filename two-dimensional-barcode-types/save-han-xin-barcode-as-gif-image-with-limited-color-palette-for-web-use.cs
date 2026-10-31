using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinGifDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HanXin.gif");

        string codeText = "Sample123";

        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.UseAntiAlias = false;
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}