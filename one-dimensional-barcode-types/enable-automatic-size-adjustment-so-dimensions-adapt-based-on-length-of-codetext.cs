using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string[] codeTexts = new string[]
        {
            "12345",
            "1234567890",
            "12345678901234567890"
        };

        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        foreach (string text in codeTexts)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = text;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

                generator.Parameters.ImageWidth.Pixels = 300;
                generator.Parameters.ImageHeight.Pixels = 150;

                string filePath = Path.Combine(outputDir, $"barcode_{text.Length}chars.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode for '{text}' to '{filePath}'.");
            }
        }
    }
}