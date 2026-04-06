using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Original UTF-8 text to encode
        string originalText = "Привет мир";

        // Generate PDF417 barcode with UTF-8 encoding and store it in a memory stream
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
            {
                generator.SetCodeText(originalText, Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading
            ms.Position = 0;

            // Read the barcode and enable automatic encoding detection
            using (var reader = new BarCodeReader(ms, DecodeType.Pdf417))
            {
                reader.BarcodeSettings.DetectEncoding = true;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Decoded Text: " + result.CodeText);
                }
            }
        }
    }
}