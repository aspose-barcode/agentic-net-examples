// Title: Read QR barcode from byte array with UTF-16 detection
// Description: Demonstrates using BarCodeReader to decode a QR code stored in a byte array, ensuring DetectEncoding correctly interprets UTF-16 encoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading category, showcasing how to generate a QR code with Unicode content, store it in memory, and read it back using BarCodeReader. Key API classes include BarcodeGenerator, BarCodeReader, and BarcodeSettings. Typical use cases involve processing barcodes in streams without file I/O, handling multilingual data, and verifying encoding detection. Developers often need to read barcodes from network streams, databases, or in-memory buffers while preserving original character encoding.
// Prompt: Use BarCodeReader to read a barcode from a byte array and ensure DetectEncoding correctly decodes UTF16 content.
// Tags: qr, barcode, reading, utf16, encoding, memorystream, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code with UTF-16 encoded text,
/// stores it in a memory stream, and reads it back using BarCodeReader
/// with encoding detection enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, reads it from a byte array,
    /// and verifies that DetectEncoding correctly decodes the original UTF-16 text.
    /// </summary>
    static void Main()
    {
        // Sample text containing Unicode characters (will be encoded as UTF-16)
        const string originalText = "Привет";

        // Generate a QR barcode with UTF-16 encoded text and save it to a memory stream
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the text using UTF-16 (Unicode) encoding
            generator.SetCodeText(originalText, Encoding.Unicode);

            using (var ms = new MemoryStream())
            {
                // Save the barcode image as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                // Retrieve the underlying byte array for later reading
                barcodeBytes = ms.ToArray();
            }
        }

        // Read the barcode from the byte array using BarCodeReader
        using (var ms = new MemoryStream(barcodeBytes))
        using (var reader = new BarCodeReader(ms, DecodeType.QR))
        {
            // Enable automatic detection of the codetext encoding
            reader.BarcodeSettings.DetectEncoding = true;

            // Iterate through all detected barcodes (only one expected)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected CodeText: " + result.CodeText);
                // Verify that the detected text matches the original UTF-16 text
                if (result.CodeText == originalText)
                {
                    Console.WriteLine("Encoding detection succeeded.");
                }
                else
                {
                    Console.WriteLine("Encoding detection failed.");
                }
            }
        }
    }
}