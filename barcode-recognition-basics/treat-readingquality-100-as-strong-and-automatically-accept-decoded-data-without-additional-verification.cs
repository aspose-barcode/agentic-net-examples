using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";
        const string text = "HelloWorld";

        // Generate a QR code (strong confidence) and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            generator.Save(filePath);
        }

        // Read the barcode from the saved file
        using (var reader = new BarCodeReader(filePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Treat ReadingQuality 100 as strong and accept automatically
                if (result.ReadingQuality == 100.0)
                {
                    Console.WriteLine($"Accepted (Strong): {result.CodeText}");
                }
                else
                {
                    Console.WriteLine($"Low quality ({result.ReadingQuality}%): {result.CodeText}");
                }
            }
        }
    }
}