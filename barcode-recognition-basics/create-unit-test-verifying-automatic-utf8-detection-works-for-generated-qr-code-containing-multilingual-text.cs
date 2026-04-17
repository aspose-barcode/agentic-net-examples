using System;
using System.IO;
using System.Text;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Multilingual text containing Latin, Cyrillic and Chinese characters
        const string originalText = "Hello Привет 你好";

        // Generate QR code with UTF-8 encoding and save to a memory stream
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.SetCodeText(originalText, Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Ensure the stream is ready for reading
            ms.Position = 0;

            // Read with automatic UTF-8 detection enabled
            string detectedText;
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                reader.BarcodeSettings.DetectEncoding = true;
                var result = reader.ReadBarCodes().FirstOrDefault();
                detectedText = result?.CodeText ?? string.Empty;
                Console.WriteLine("DetectEncoding = true  => " + detectedText);
            }

            // Reset stream position for the second read
            ms.Position = 0;

            // Read with automatic detection disabled
            string nonDetectedText;
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                reader.BarcodeSettings.DetectEncoding = false;
                var result = reader.ReadBarCodes().FirstOrDefault();
                nonDetectedText = result?.CodeText ?? string.Empty;
                Console.WriteLine("DetectEncoding = false => " + nonDetectedText);
            }

            // Verify that detection restored the original text
            if (detectedText == originalText)
            {
                Console.WriteLine("Test Passed: Automatic UTF-8 detection correctly restored the original text.");
            }
            else
            {
                Console.WriteLine("Test Failed: Detected text does not match the original.");
            }

            // Show the effect of disabling detection (may appear garbled)
            if (nonDetectedText != originalText)
            {
                Console.WriteLine("As expected, disabling detection yields different (likely garbled) text.");
            }
        }
    }
}