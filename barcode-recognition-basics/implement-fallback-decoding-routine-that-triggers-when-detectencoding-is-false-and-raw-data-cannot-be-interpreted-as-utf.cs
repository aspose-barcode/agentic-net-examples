// Title: QR Code Generation and Fallback Decoding with Aspose.BarCode
// Description: Demonstrates creating a QR code containing Cyrillic text, saving it to a memory stream, and decoding it with custom encoding handling, including a fallback when UTF‑8 decoding fails.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting raw byte data. Developers often need to control text encoding, disable automatic detection, and implement fallback strategies for non‑UTF‑8 payloads, especially when handling international characters.
// Prompt: Implement a fallback decoding routine that triggers when DetectEncoding is false and raw data cannot be interpreted as UTF8.
// Tags: qr, unicode, encoding, fallback, decoding, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a QR code with Cyrillic text, saves it to a memory stream,
/// and reads it back using a custom decoding routine that includes a fallback
/// when UTF‑8 decoding is not possible.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves to a stream,
    /// and reads the barcode with explicit encoding handling.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated QR code image.
        using (var ms = new MemoryStream())
        {
            // Generate a QR code containing the Cyrillic word "Привет".
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Привет"))
            {
                // Explicitly set the code text encoding to UTF‑8.
                generator.SetCodeText("Привет", Encoding.UTF8);
                // Save the QR code as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading.
            ms.Position = 0;

            // Initialize a barcode reader for QR codes, disabling automatic encoding detection.
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                reader.BarcodeSettings.DetectEncoding = false;

                // Iterate over all detected barcodes (there should be only one in this example).
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("=== Detected Barcode ===");
                    Console.WriteLine("Symbology: " + result.CodeTypeName);

                    // Attempt to decode the raw bytes using strict UTF‑8 decoding.
                    string decodedText;
                    var strictUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
                    try
                    {
                        decodedText = strictUtf8.GetString(result.CodeBytes);
                        Console.WriteLine("Decoded (UTF-8): " + decodedText);
                    }
                    catch (DecoderFallbackException)
                    {
                        // Fallback: decode using Windows‑1252 (or any other appropriate fallback encoding).
                        var fallbackEncoding = Encoding.GetEncoding(1252);
                        decodedText = fallbackEncoding.GetString(result.CodeBytes);
                        Console.WriteLine("Decoded (fallback encoding 1252): " + decodedText);
                    }

                    // Output the raw byte sequence for diagnostic purposes.
                    Console.WriteLine("Raw bytes: " + BitConverter.ToString(result.CodeBytes));
                }
            }
        }
    }
}