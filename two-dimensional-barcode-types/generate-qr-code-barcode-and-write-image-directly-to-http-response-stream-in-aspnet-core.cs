using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Simulate an ASP.NET Core HttpResponse body stream
        using (var responseStream = new MemoryStream())
        {
            // Create a QR Code generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to encode
                generator.CodeText = "https://example.com";

                // Configure QR error correction level (high)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the barcode image directly to the simulated response stream as PNG
                generator.Save(responseStream, BarCodeImageFormat.Png);
            }

            // Example output: display the size of the generated image
            Console.WriteLine($"Generated QR code image size: {responseStream.Length} bytes");
        }
    }
}