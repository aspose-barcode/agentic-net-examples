using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string[] codeTexts = new string[]
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        for (int i = 0; i < codeTexts.Length; i++)
        {
            string codeText = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = codeText;
                generator.Save(filePath);
            }
        }
    }
}