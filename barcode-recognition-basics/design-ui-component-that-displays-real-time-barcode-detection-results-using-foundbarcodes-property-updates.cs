using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Step 1: Generate a barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123456";
            // Save to a memory stream as PNG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Step 2: Read the barcode from the memory stream.
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Perform detection.
                    reader.ReadBarCodes();

                    // Step 3: Display detection results using FoundBarCodes property.
                    BarCodeResult[] results = reader.FoundBarCodes;
                    Console.WriteLine($"Detected {results.Length} barcode(s):");
                    for (int i = 0; i < results.Length; i++)
                    {
                        BarCodeResult result = results[i];
                        Console.WriteLine($"[{i + 1}] Type: {result.CodeTypeName}");
                        Console.WriteLine($"    CodeText: {result.CodeText}");
                        Console.WriteLine($"    Confidence: {result.Confidence}");
                        Console.WriteLine($"    ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
        }
    }
}