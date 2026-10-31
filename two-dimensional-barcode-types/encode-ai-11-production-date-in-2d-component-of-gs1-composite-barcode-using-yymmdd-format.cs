using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1CompositeAI11.png");
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, "(01)98898765432106|(11)991231"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            gen.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            gen.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"GS1 Composite barcode with AI 11 saved to: {outputPath}");
    }
}