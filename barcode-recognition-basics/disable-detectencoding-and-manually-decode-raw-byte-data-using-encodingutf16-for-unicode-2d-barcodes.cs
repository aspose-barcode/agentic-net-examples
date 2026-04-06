using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Unicode text to encode
        const string originalText = "Привет";

        // Create barcode image in memory
        using (var ms = new MemoryStream())
        {
            // Generate QR code with UTF-16 (Unicode) encoding
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Encode the text as UTF-16 bytes (little‑endian)
                generator.SetCodeText(originalText, Encoding.Unicode);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Prepare stream for reading
            ms.Position = 0;

            // Read the barcode without automatic encoding detection
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Disable automatic detection of code text encoding
                reader.BarcodeSettings.DetectEncoding = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // This will show the raw (incorrect) interpretation
                    Console.WriteLine("Auto decoded CodeText: " + result.CodeText);

                    // Manually decode the raw bytes using UTF-16
                    string decoded = result.GetCodeText(Encoding.Unicode);
                    Console.WriteLine("Manually decoded CodeText: " + decoded);
                }
            }
        }
    }
}