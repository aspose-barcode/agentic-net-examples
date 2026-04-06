using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create first barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "12345";
            generator.Save("code1.png");
        }

        // Create second barcode image with different text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "ABCDE";
            generator.Save("code2.png");
        }

        // Initialize the barcode reader
        using (var reader = new BarCodeReader())
        {
            // Set the type of barcode to recognize
            reader.SetBarCodeReadType(DecodeType.Code128);

            // First image
            reader.SetBarCodeImage("code1.png");
            Console.WriteLine("Reading from code1.png:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Change source image after construction
            reader.SetBarCodeImage("code2.png");
            Console.WriteLine("Reading from code2.png:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}