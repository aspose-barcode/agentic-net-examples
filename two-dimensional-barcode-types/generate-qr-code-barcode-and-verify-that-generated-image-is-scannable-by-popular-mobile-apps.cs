using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "qr.png";
        const string codeText = "https://example.com";

        // Generate QR Code with high error correction level
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save(filePath);
        }

        // Verify that the generated image can be read
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.QR))
        {
            bool readable = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("QR Code is scannable.");
                Console.WriteLine("Decoded Text: " + result.CodeText);
                readable = true;
                break;
            }

            if (!readable)
            {
                Console.WriteLine("QR Code could not be read.");
            }
        }
    }
}