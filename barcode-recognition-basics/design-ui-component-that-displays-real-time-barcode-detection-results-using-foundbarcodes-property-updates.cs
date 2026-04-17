using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare sample data
        var samples = new (BaseEncodeType encodeType, string codeText)[]
        {
            (EncodeTypes.Code128, "Hello123"),
            (EncodeTypes.QR, "https://example.com")
        };

        foreach (var sample in samples)
        {
            // Generate barcode image in memory
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            using (var bitmap = generator.GenerateBarCodeImage())
            using (var ms = new MemoryStream())
            {
                // Save bitmap to a memory stream (optional, just to demonstrate saving)
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;

                // Read barcodes from the generated image
                using (var reader = new BarCodeReader(bitmap, DecodeType.Code128, DecodeType.QR))
                {
                    // Perform detection
                    reader.ReadBarCodes();

                    // Use FoundBarCodes property to access results
                    var results = reader.FoundBarCodes;
                    Console.WriteLine($"Detected {results.Length} barcode(s) for sample \"{sample.codeText}\":");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Text: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}