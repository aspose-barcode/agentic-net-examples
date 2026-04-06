using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Recognize the barcode from the bitmap
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}