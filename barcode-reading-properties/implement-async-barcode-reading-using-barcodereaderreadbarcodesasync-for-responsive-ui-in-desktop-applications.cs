using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0;

                using (BarCodeReader reader = new BarCodeReader(memoryStream))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}