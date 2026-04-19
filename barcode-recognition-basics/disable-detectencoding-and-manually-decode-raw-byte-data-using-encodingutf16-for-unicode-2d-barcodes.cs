using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define barcode text containing Unicode characters
        const string unicodeText = "Привет мир";

        // Output file for the generated barcode image
        string imagePath = Path.Combine(Environment.CurrentDirectory, "qr_utf16.png");

        // -----------------------------------------------------------------
        // Generate QR barcode using UTF-16 encoding (little‑endian)
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the Unicode text as UTF-16 bytes (BOM will be added automatically)
            generator.SetCodeText(unicodeText, Encoding.Unicode);
            // Save the image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -----------------------------------------------------------------
        // Read the barcode without automatic encoding detection
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Disable automatic detection of code text encoding
            reader.BarcodeSettings.DetectEncoding = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Manually decode the raw bytes using UTF-16
                string decodedText = result.GetCodeText(Encoding.Unicode);
                Console.WriteLine("Manually decoded CodeText (UTF-16): " + decodedText);
            }
        }
    }
}