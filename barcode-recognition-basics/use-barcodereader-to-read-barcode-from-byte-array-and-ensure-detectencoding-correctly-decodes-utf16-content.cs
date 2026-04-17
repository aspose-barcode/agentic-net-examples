using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Original text containing Unicode characters
        const string originalText = "Привет"; // Russian word "Hello"

        // Encode the text as UTF-16 (Unicode) and generate a QR barcode into a memory stream
        byte[] barcodeBytes;
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Encode using UTF-16 (little endian) – Encoding.Unicode
                generator.SetCodeText(originalText, Encoding.Unicode);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Capture the generated image bytes
            barcodeBytes = ms.ToArray();
        }

        // Read the barcode from the byte array with DetectEncoding enabled
        using (var inputStream = new MemoryStream(barcodeBytes))
        {
            using (var reader = new BarCodeReader(inputStream, DecodeType.QR))
            {
                // Force engine to detect the original encoding (UTF-16)
                reader.BarcodeSettings.DetectEncoding = true;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Detected (DetectEncoding=true): " + result.CodeText);
                }
            }
        }

        // Read the same barcode with DetectEncoding disabled for comparison
        using (var inputStream = new MemoryStream(barcodeBytes))
        {
            using (var reader = new BarCodeReader(inputStream, DecodeType.QR))
            {
                reader.BarcodeSettings.DetectEncoding = false;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Detected (DetectEncoding=false): " + result.CodeText);
                }
            }
        }
    }
}