using System;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Output file path
            string outputFile = "qr_low_error_correction.jpeg";

            // Create a QR code generator with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set error correction level to low (LevelL)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;

                // Save the barcode as a JPEG image
                generator.Save(outputFile, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"QR code saved to {outputFile}");
        }
    }
}