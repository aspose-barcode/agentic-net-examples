using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate sample barcodes for UPC-A, UPC-E and EAN-8
        GenerateBarcode(EncodeTypes.UPCA, "012345678905", "upca.png");
        GenerateBarcode(EncodeTypes.UPCE, "012345678905", "upce.png");
        GenerateBarcode(EncodeTypes.EAN8, "1234567", "ean8.png");

        // Initialize the reader and configure it to decode the three retail symbologies
        using (var reader = new BarCodeReader())
        {
            // MultiDecodeType includes UPC-A, UPC-E and EAN-8
            reader.BarCodeReadType = new MultiDecodeType(DecodeType.UPCA, DecodeType.UPCE, DecodeType.EAN8);

            // Process each generated image
            string[] files = { "upca.png", "upce.png", "ean8.png" };
            foreach (var file in files)
            {
                reader.SetBarCodeImage(file);
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {file}");
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                }
            }
        }
    }

    // Helper method to create and save a barcode image
    static void GenerateBarcode(BaseEncodeType type, string codeText, string fileName)
    {
        using (var generator = new BarcodeGenerator(type))
        {
            generator.CodeText = codeText;
            generator.Save(fileName);
        }
    }
}