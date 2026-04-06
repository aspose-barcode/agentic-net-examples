using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "12345";

            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode to the memory stream as PNG
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading

                // Read the barcode from the memory stream
                using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();

                    foreach (var result in results)
                    {
                        // Get the bounding rectangle of the detected barcode
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    }
                }
            }
        }
    }
}