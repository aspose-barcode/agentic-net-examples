using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string originalText = "Слово";

        // Generate QR barcode with UTF-8 encoding and save to memory stream
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.SetCodeText(originalText, Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Automatic detection (DetectEncoding = true)
            ms.Position = 0;
            string autoDetectedText = null;
            using (var readerAuto = new BarCodeReader(ms, DecodeType.QR))
            {
                readerAuto.BarcodeSettings.DetectEncoding = true;
                foreach (BarCodeResult result in readerAuto.ReadBarCodes())
                {
                    autoDetectedText = result.CodeText;
                    break; // only one barcode expected
                }
            }

            // Manual decoding (DetectEncoding = false, then GetCodeText with UTF8)
            ms.Position = 0;
            string manualDecodedText = null;
            using (var readerManual = new BarCodeReader(ms, DecodeType.QR))
            {
                readerManual.BarcodeSettings.DetectEncoding = false;
                foreach (BarCodeResult result in readerManual.ReadBarCodes())
                {
                    manualDecodedText = result.GetCodeText(Encoding.UTF8);
                    break; // only one barcode expected
                }
            }

            // Verify both results are identical
            if (autoDetectedText == manualDecodedText && autoDetectedText == originalText)
            {
                Console.WriteLine("Test passed: automatic and manual decoding produce identical results.");
            }
            else
            {
                Console.WriteLine("Test failed:");
                Console.WriteLine($"Original text       : {originalText}");
                Console.WriteLine($"Auto-detected text  : {autoDetectedText ?? "null"}");
                Console.WriteLine($"Manual decoded text: {manualDecodedText ?? "null"}");
            }
        }
    }
}