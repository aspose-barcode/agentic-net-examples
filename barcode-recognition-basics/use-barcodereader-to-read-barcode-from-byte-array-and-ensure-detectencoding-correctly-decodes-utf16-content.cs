// Title: Read QR Code from Byte Array with DetectEncoding UTF16 Support
// Description: Demonstrates using BarCodeReader to decode a QR code stored in a byte array, showing how DetectEncoding correctly interprets UTF‑16 encoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to generate a QR code with Unicode (UTF‑16) content, save it to a memory stream, and read it back using BarCodeReader. Key API classes include BarcodeGenerator, BarCodeReader, and BarcodeSettings. Typical use cases involve processing barcode images received as byte arrays (e.g., from databases or network streams) where proper character encoding detection is required.
// Prompt: Use BarCodeReader to read a barcode from a byte array and ensure DetectEncoding correctly decodes UTF16 content.
// Tags: qr, barcode, read, byte array, detectencoding, utf16, aspose.barcode, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code with UTF‑16 text, saves it to a memory stream,
/// and reads it back using <see cref="BarCodeReader"/> with different DetectEncoding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, then reads it twice:
    /// once with DetectEncoding enabled and once disabled, printing the results.
    /// </summary>
    static void Main()
    {
        // Sample text containing Unicode characters (including Chinese characters)
        string unicodeText = "Test UTF16 漢字";

        // Generate a QR code using UTF‑16 encoding and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, unicodeText))
        {
            // Apply UTF‑16 (Unicode) encoding to the code text
            generator.SetCodeText(unicodeText, Encoding.Unicode);
            // Adjust QR code module size (optional visual setting)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated QR code image to a MemoryStream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray(); // Convert stream to byte array for later reading

                // ------------------------------------------------------------
                // Read the barcode from the byte array with DetectEncoding = true
                // ------------------------------------------------------------
                using (var readStream = new MemoryStream(imageBytes))
                using (var reader = new BarCodeReader(readStream, DecodeType.QR))
                {
                    reader.BarcodeSettings.DetectEncoding = true; // Enable automatic encoding detection
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("DetectEncoding: true");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }
                }

                // ------------------------------------------------------------
                // Read the barcode from the byte array with DetectEncoding = false
                // ------------------------------------------------------------
                using (var readStream = new MemoryStream(imageBytes))
                using (var reader = new BarCodeReader(readStream, DecodeType.QR))
                {
                    reader.BarcodeSettings.DetectEncoding = false; // Disable automatic encoding detection
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("DetectEncoding: false");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}