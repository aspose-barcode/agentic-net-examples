// Title: QR Code Generation and Decoding with UTF-8 Fallback
// Description: Demonstrates generating a QR code containing Unicode text, reading it with encoding detection disabled, and applying a fallback decoding when UTF‑8 fails.
// Prompt: Implement a fallback decoding routine that triggers when DetectEncoding is false and raw data cannot be interpreted as UTF8.
// Tags: qr, barcode, encoding, fallback, aspose.barcode, utf8, windows-1252

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR code with Unicode text, reads it without automatic encoding detection,
/// and demonstrates a manual fallback decoding strategy when UTF‑8 decoding is not successful.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a QR code containing Cyrillic text using UTF‑8 encoding.
        // ------------------------------------------------------------
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for QR codes with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Привет"))
            {
                // Explicitly set the code text with UTF‑8 encoding to ensure correct byte representation.
                generator.SetCodeText("Привет", Encoding.UTF8);

                // Save the generated barcode image into the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // ------------------------------------------------------------
            // 2. Prepare the stream for reading the barcode image.
            // ------------------------------------------------------------
            ms.Position = 0; // Reset stream position to the beginning.

            // ------------------------------------------------------------
            // 3. Read the barcode with automatic encoding detection turned off.
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Disable automatic detection so we can control the decoding process.
                reader.BarcodeSettings.DetectEncoding = false;

                // Iterate over all detected barcodes (in this case, just one).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Attempt to decode the raw data using UTF‑8.
                    string textUtf8 = result.GetCodeText(Encoding.UTF8);

                    // Determine whether a fallback is needed:
                    // - Empty or null result indicates decoding failure.
                    // - Presence of the Unicode replacement character (�) signals invalid UTF‑8 sequences.
                    bool needFallback = string.IsNullOrEmpty(textUtf8) || textUtf8.Contains('\uFFFD');

                    if (needFallback)
                    {
                        // ------------------------------------------------------------
                        // 4. Fallback decoding: try Windows‑1252 (or any other desired encoding).
                        // ------------------------------------------------------------
                        string fallbackText = result.GetCodeText(Encoding.GetEncoding(1252));
                        Console.WriteLine($"Fallback decoded text: {fallbackText}");
                    }
                    else
                    {
                        // UTF‑8 decoding succeeded; output the result.
                        Console.WriteLine($"UTF8 decoded text: {textUtf8}");
                    }
                }
            }
        }
    }
}