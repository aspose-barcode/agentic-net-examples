using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a Code39 barcode image with spaces in the code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "A B C 1 2 3"))
        {
            generator.Save("barcode.png");
        }

        using (FileStream fs = new FileStream("barcode.png", FileMode.Open, FileAccess.Read))
        {
            using (BarCodeReader reader = new BarCodeReader(fs, DecodeType.Code39))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}