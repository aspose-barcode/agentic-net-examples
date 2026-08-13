// Title: Disable DetectEncoding and manually decode Unicode QR barcode
// Description: Demonstrates generating a QR code with UTF-16 text, disabling automatic encoding detection during recognition, and manually decoding the raw bytes using Encoding.Unicode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on custom encoding handling. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings to control encoding detection, a common requirement when working with Unicode 2D barcodes such as QR codes. Developers often need to disable automatic detection to process raw byte data and apply specific character encodings.
// Prompt: Disable DetectEncoding and manually decode raw byte data using Encoding.UTF16 for Unicode 2D barcodes.
// Tags: qr, unicode, encoding, detectencoding, manualdecode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates disabling automatic encoding detection and manually decoding raw byte data for a Unicode QR barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code with UTF-16 text, reads it without encoding detection, and decodes using Encoding.Unicode.
    /// </summary>
    static void Main()
    {
        // Original Unicode text to encode
        const string originalText = "Привет";

        // Create a QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the text as UTF-16 (Unicode) bytes
            generator.SetCodeText(originalText, Encoding.Unicode);

            // Save the generated barcode image to a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader for QR codes
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    // Turn off automatic detection of the text encoding
                    reader.BarcodeSettings.DetectEncoding = false;

                    // Iterate through all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Display the raw CodeText (may appear garbled because DetectEncoding is disabled)
                        Console.WriteLine("Raw CodeText: " + result.CodeText);

                        // Manually decode the raw bytes using UTF-16 (Unicode) encoding
                        string decodedText = result.GetCodeText(Encoding.Unicode);
                        Console.WriteLine("Decoded with UTF-16: " + decodedText);
                    }
                }
            }
        }
    }
}