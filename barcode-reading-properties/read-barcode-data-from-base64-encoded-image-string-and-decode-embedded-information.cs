using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image and get its Base64 string.
        string base64Image;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                base64Image = Convert.ToBase64String(ms.ToArray());
            }
        }

        // Decode the Base64 string back to an image stream.
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (var imageStream = new MemoryStream(imageBytes))
        {
            // Create a reader that detects all supported barcode types.
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Barcode Text: {result.CodeText}");
                }
            }
        }
    }
}