using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR code generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the text to encode
                generator.CodeText = "Hello, Aspose!";

                // Set resolution to 300 dpi
                generator.Parameters.Resolution = 300f;

                // Save the barcode image to a memory stream in PNG format
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    // Example usage of the generated stream
                    Console.WriteLine($"Generated QR code image size: {memoryStream.Length} bytes");
                }
            }
        }
    }
}