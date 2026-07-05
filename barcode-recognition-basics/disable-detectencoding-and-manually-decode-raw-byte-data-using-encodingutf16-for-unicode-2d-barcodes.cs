// Title: Demonstrate manual UTF-16 decoding of Unicode QR code without auto-detect
// Description: Shows how to generate a QR code with Unicode text, disable automatic encoding detection, and manually decode the raw byte data using Encoding.Unicode (UTF-16). Useful for handling 2D barcodes that contain Unicode characters.
// Prompt: Disable DetectEncoding and manually decode raw byte data using Encoding.UTF16 for Unicode 2D barcodes.
// Tags: qr, unicode, manual-decoding, detectencoding, encoding.unicode, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code containing Unicode text,
/// disables automatic encoding detection during reading, and manually
/// decodes the raw byte data using UTF-16 (Encoding.Unicode).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it back,
    /// and demonstrates manual decoding of the barcode text.
    /// </summary>
    static void Main()
    {
        // Sample Unicode text to encode in the QR code
        const string unicodeText = "Привет";

        // Create a QR code generator and set the code text using UTF-16 encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.SetCodeText(unicodeText, Encoding.Unicode);

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader for QR codes, using the memory stream as input
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    // Disable automatic detection of the text encoding
                    reader.BarcodeSettings.DetectEncoding = false;

                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    // Process each detected barcode result
                    foreach (var result in results)
                    {
                        // When DetectEncoding is disabled, CodeText may contain garbled data
                        Console.WriteLine("Raw CodeText (auto-detect disabled): " + result.CodeText);

                        // Manually decode the raw byte data using UTF-16 (Encoding.Unicode)
                        string decodedText = result.GetCodeText(Encoding.Unicode);
                        Console.WriteLine("Manually decoded text (UTF-16): " + decodedText);
                    }
                }
            }
        }
    }
}