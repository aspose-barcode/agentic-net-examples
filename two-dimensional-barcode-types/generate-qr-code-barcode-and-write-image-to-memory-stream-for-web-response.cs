using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Sample data to encode in the QR Code
            const string codeText = "https://example.com";

            // Create a QR Code generator with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Optional: set error correction level (e.g., Medium)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Generate the barcode image into a memory stream (PNG format)
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);

                    // Reset stream position if further processing is needed
                    memoryStream.Position = 0;

                    // For demonstration, output the size of the generated image
                    Console.WriteLine($"QR Code PNG generated, size: {memoryStream.Length} bytes");
                }
            }
        }
    }
}