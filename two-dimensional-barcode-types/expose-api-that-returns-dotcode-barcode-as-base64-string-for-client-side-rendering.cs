using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string sampleText = "Aspose";
        string base64 = GenerateDotCodeBase64(sampleText);
        Console.WriteLine("Base64 PNG of DotCode barcode:");
        Console.WriteLine(base64);
    }

    static string GenerateDotCodeBase64(string codeText)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}