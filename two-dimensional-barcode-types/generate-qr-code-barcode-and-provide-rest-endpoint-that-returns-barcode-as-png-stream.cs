using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace AsposeBarcodeDemo
{
    class Program
    {
        static void Main()
        {
            // Simulate a REST request for a QR code
            byte[] pngData = GetQrCodePng("https://example.com");

            // For demonstration, write the PNG to a file
            const string outputPath = "qr_demo.png";
            File.WriteAllBytes(outputPath, pngData);
            Console.WriteLine($"QR code PNG saved to {outputPath}");
        }

        // Simulated REST endpoint: returns PNG bytes of a QR code
        static byte[] GetQrCodePng(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("QR code text cannot be null or empty.", nameof(text));

            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set QR error correction level (optional)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Set image resolution (optional)
                generator.Parameters.Resolution = 300;

                using (var memoryStream = new MemoryStream())
                {
                    // Save barcode image to the memory stream in PNG format
                    generator.Save(memoryStream, BarCodeImageFormat.Png);

                    // Return the PNG data as a byte array
                    return memoryStream.ToArray();
                }
            }
        }
    }
}