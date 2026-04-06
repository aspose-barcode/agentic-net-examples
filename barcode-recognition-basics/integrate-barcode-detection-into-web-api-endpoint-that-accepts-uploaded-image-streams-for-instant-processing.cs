using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a barcode image and keep it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Test12345";
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading

                // Detect barcodes from the image stream
                using (var reader = new BarCodeReader(imageStream))
                {
                    // Specify which barcode types to look for (optional)
                    reader.SetBarCodeReadType(DecodeType.Code128);
                    var results = reader.ReadBarCodes();

                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes were detected.");
                    }
                }
            }
        }
    }
}