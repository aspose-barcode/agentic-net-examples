using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string outputPath = "maxicode.png";

        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            memoryStream.Position = 0;

            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(fileStream);
            }
        }

        Console.WriteLine($"MaxiCode PNG generated at {Path.GetFullPath(outputPath)}");
    }
}