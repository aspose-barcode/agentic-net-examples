// Title: Read QR Code from Byte Array with UTF-16 Detection
// Description: Demonstrates generating a QR code containing Unicode text, saving it to a byte array, and reading it back using BarCodeReader with DetectEncoding enabled and disabled to show proper UTF‑16 decoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code, the BarCodeReader to decode barcodes from streams, and the DetectEncoding setting to automatically handle Unicode (UTF‑16) content. Developers working with multi‑language data, internationalization, or any scenario requiring accurate text encoding during barcode scanning can refer to this pattern.
// Prompt: Use BarCodeReader to read a barcode from a byte array and ensure DetectEncoding correctly decodes UTF16 content.
// Tags: qr,utf16,detectencoding,barcodegeneration,barcoderecognition,bytearray,aspnet,aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code with Unicode text, saves it to a byte array,
/// and reads it back using <see cref="BarCodeReader"/> with different DetectEncoding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, writes it to a memory stream,
    /// and demonstrates barcode reading with DetectEncoding enabled and disabled.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample Unicode text that requires UTF-16 encoding (Japanese greeting).
        string codeText = "こんにちは世界";

        // Create a QR code generator with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the module size (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Explicitly set the code text encoding to Unicode (UTF-16).
            generator.SetCodeText(codeText, Encoding.Unicode);

            // Save the generated barcode image to a memory stream and obtain the raw byte array.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // ------------------------------------------------------------
                // Read barcode with DetectEncoding enabled (true)
                // ------------------------------------------------------------
                Console.WriteLine("DetectEncoding: true");
                using (var msRead = new MemoryStream(imageBytes))
                {
                    using (var reader = new BarCodeReader(msRead, DecodeType.QR))
                    {
                        // Enable automatic detection of the text encoding.
                        reader.BarcodeSettings.DetectEncoding = true;

                        // Perform the recognition and iterate over all results.
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"CodeType: {result.CodeTypeName}");
                            Console.WriteLine($"CodeText: {result.CodeText}");
                        }
                    }
                }

                // ------------------------------------------------------------
                // Read barcode with DetectEncoding disabled (false)
                // ------------------------------------------------------------
                Console.WriteLine("DetectEncoding: false");
                using (var msRead = new MemoryStream(imageBytes))
                {
                    using (var reader = new BarCodeReader(msRead, DecodeType.QR))
                    {
                        // Disable automatic detection; the reader will use default encoding.
                        reader.BarcodeSettings.DetectEncoding = false;

                        // Perform the recognition and iterate over all results.
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"CodeType: {result.CodeTypeName}");
                            Console.WriteLine($"CodeText: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}