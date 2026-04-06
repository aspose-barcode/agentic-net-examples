using System;
using System.IO;
using System.Text;
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
                // Encode the text using UTF-8 (adds BOM if supported)
                generator.SetCodeText(originalText, Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Ensure the stream position is reset before reading
            ms.Position = 0;

            // ---------- Test with automatic encoding detection enabled ----------
            using (var readerDetect = new BarCodeReader(ms, DecodeType.QR))
            {
                // DetectEncoding is true by default, set explicitly for clarity
                readerDetect.BarcodeSettings.DetectEncoding = true;

                bool detectionPassed = false;
                foreach (BarCodeResult result in readerDetect.ReadBarCodes())
                {
                    if (result.CodeText == originalText)
                    {
                        detectionPassed = true;
                        Console.WriteLine("DetectEncoding=TRUE: Success – decoded text matches original.");
                    }
                    else
                    {
                        Console.WriteLine($"DetectEncoding=TRUE: Failure – decoded '{result.CodeText}' does not match original.");
                    }
                }

                if (!detectionPassed)
                {
                    Console.WriteLine("DetectEncoding=TRUE: No matching barcode found.");
                }
            }

            // Reset stream again for the second read
            ms.Position = 0;

            // ---------- Test with automatic encoding detection disabled ----------
            using (var readerNoDetect = new BarCodeReader(ms, DecodeType.QR))
            {
                readerNoDetect.BarcodeSettings.DetectEncoding = false;

                bool mismatchDetected = false;
                foreach (BarCodeResult result in readerNoDetect.ReadBarCodes())
                {
                    if (result.CodeText != originalText)
                    {
                        mismatchDetected = true;
                        Console.WriteLine("DetectEncoding=FALSE: Success – decoded text differs as expected.");
                        Console.WriteLine($"Decoded text: '{result.CodeText}'");
                    }
                    else
                    {
                        Console.WriteLine("DetectEncoding=FALSE: Failure – decoded text unexpectedly matches original.");
                    }
                }

                if (!mismatchDetected)
                {
                    Console.WriteLine("DetectEncoding=FALSE: No barcode read or text unexpectedly matches original.");
                }
            }
        }
    }
}