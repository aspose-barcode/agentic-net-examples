using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading

                // Read the barcode using BarCodeReader inside a using block to ensure disposal
                using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}