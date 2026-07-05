// Title: Read QR barcode from byte array with UTF-16 detection
// Description: Demonstrates generating a QR code containing Cyrillic text, saving it to a byte array, and using BarCodeReader with DetectEncoding to correctly decode UTF‑16 content.
// Prompt: Use BarCodeReader to read a barcode from a byte array and ensure DetectEncoding correctly decodes UTF16 content.
// Tags: qr, barcode, utf16, detectencoding, aspose, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR barcode with UTF‑16 encoded text,
/// reads it from a byte array, and demonstrates the effect of the DetectEncoding setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, saves it to memory, and reads it back with and without encoding detection.
    /// </summary>
    static void Main()
    {
        // Original Unicode text (contains Cyrillic characters)
        const string originalText = "Привет";

        // Generate a QR barcode with UTF-16 (Unicode) encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the text using UTF-16 (little endian)
            generator.SetCodeText(originalText, Encoding.Unicode);

            // Save the barcode image to a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] barcodeBytes = ms.ToArray();

                // ------------------------------------------------------------
                // Read the barcode from the byte array with DetectEncoding enabled
                // ------------------------------------------------------------
                using (var readStream = new MemoryStream(barcodeBytes))
                using (var reader = new BarCodeReader(readStream, DecodeType.QR))
                {
                    // Ensure the engine detects the UTF-16 encoding
                    reader.BarcodeSettings.DetectEncoding = true;

                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Decoded with DetectEncoding = true: " + result.CodeText);
                    }
                }

                // ------------------------------------------------------------
                // Demonstrate decoding without DetectEncoding (should be garbled)
                // ------------------------------------------------------------
                using (var readStream = new MemoryStream(barcodeBytes))
                using (var reader = new BarCodeReader(readStream, DecodeType.QR))
                {
                    // Disable automatic encoding detection
                    reader.BarcodeSettings.DetectEncoding = false;

                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Decoded with DetectEncoding = false: " + result.CodeText);
                    }
                }
            }
        }
    }
}