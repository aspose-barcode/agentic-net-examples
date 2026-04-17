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
        // Unicode text to encode
        const string originalText = "Привет";

        // Generate QR barcode with UTF8 encoding and store it in a memory stream
        using (MemoryStream ms = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Encode the text using UTF8 (adds BOM if needed)
                generator.SetCodeText(originalText, Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Prepare the stream for reading
            ms.Position = 0;

            // Read the barcode with DetectEncoding disabled
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.QR))
            {
                reader.BarcodeSettings.DetectEncoding = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Attempt to get the raw CodeText (may be garbled because DetectEncoding is false)
                    string rawCodeText = result.CodeText;
                    Console.WriteLine("Raw CodeText (DetectEncoding=false): " + rawCodeText);

                    // Fallback: decode the raw bytes using UTF8 explicitly
                    string fallbackCodeText = result.GetCodeText(Encoding.UTF8);
                    Console.WriteLine("Fallback decoded CodeText (UTF8): " + fallbackCodeText);
                }
            }
        }
    }
}