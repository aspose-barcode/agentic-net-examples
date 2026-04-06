using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Generate a sample barcode and store it in a memory stream
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image as PNG into the stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Read barcodes from the image stream
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}");
                        Console.WriteLine($"Text: {result.CodeText}");
                        Console.WriteLine($"Confidence: {result.Confidence}");
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}