using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Sample text containing Unicode characters
        string unicodeText = "Привет";

        // Generate a QR code with UTF-16 (Unicode) encoding and store it in a memory stream
        using (var memoryStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Encode the text using UTF-16 (Encoding.Unicode)
                generator.SetCodeText(unicodeText, Encoding.Unicode);
                // Save the barcode image to the memory stream as PNG
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Ensure the stream is positioned at the beginning for reading
            memoryStream.Position = 0;

            // Read the barcode with DetectEncoding enabled (should correctly decode UTF-16)
            using (var reader = new BarCodeReader())
            {
                reader.BarcodeSettings.DetectEncoding = true;
                reader.BarCodeReadType = DecodeType.QR;
                reader.SetBarCodeImage(memoryStream);
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("DetectEncoding = true  => CodeText: " + result.CodeText);
                }
            }

            // Reset the stream position for the second read
            memoryStream.Position = 0;

            // Read the barcode with DetectEncoding disabled (may produce garbled text)
            using (var reader = new BarCodeReader())
            {
                reader.BarcodeSettings.DetectEncoding = false;
                reader.BarCodeReadType = DecodeType.QR;
                reader.SetBarCodeImage(memoryStream);
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("DetectEncoding = false => CodeText: " + result.CodeText);
                }
            }
        }
    }
}