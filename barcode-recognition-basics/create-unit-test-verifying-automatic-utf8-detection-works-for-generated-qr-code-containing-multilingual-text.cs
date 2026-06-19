using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code with multilingual UTF‑8 text,
/// then reading it back to verify correct encoding detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it, and validates the decoded text.
    /// </summary>
    static void Main()
    {
        // Multilingual text containing Latin, Cyrillic, Chinese characters and an emoji
        string originalText = "Hello Привет 你好 🌍";

        // Create a memory stream to hold the generated QR code image
        using (var ms = new MemoryStream())
        {
            // Generate the QR code with UTF‑8 encoding
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Explicitly set the code text and its encoding
                generator.SetCodeText(originalText, Encoding.UTF8);
                // Save the barcode image as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading
            ms.Position = 0;

            // Initialize a barcode reader for QR codes using the memory stream
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Ensure automatic encoding detection is enabled (true by default)
                reader.BarcodeSettings.DetectEncoding = true;

                string decodedText = null;

                // Read all barcodes in the stream (expecting a single QR code)
                foreach (var result in reader.ReadBarCodes())
                {
                    decodedText = result.CodeText;
                    break; // Exit after the first result
                }

                // Compare the decoded text with the original to verify success
                if (decodedText == originalText)
                {
                    Console.WriteLine("Test passed: UTF-8 detection succeeded.");
                }
                else
                {
                    Console.WriteLine("Test failed: Decoded text does not match.");
                    Console.WriteLine("Original: " + originalText);
                    Console.WriteLine("Decoded : " + (decodedText ?? "null"));
                }
            }
        }
    }
}