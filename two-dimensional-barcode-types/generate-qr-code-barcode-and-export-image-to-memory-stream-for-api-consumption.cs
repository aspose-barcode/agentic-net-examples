using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Sample QR code text
            string qrText = "https://example.com";

            // Create a QR Code generator with the specified text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Export the barcode image to a memory stream in PNG format
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);

                    // Reset stream position for further reading if needed
                    memoryStream.Position = 0;

                    // Example output: size of generated image in bytes
                    Console.WriteLine($"QR code image generated. Stream length: {memoryStream.Length} bytes");
                }
            }
        }
    }
}