using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code with Unicode text,
/// saving it to a memory stream, and then reading it back
/// while handling encoding manually.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it, and attempts UTF‑8 decoding.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated QR code image.
        using (var ms = new MemoryStream())
        {
            // Generate a QR code containing the Unicode text "Привет".
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Привет"))
            {
                // Explicitly set the code text with UTF‑8 encoding.
                generator.SetCodeText("Привет", Encoding.UTF8);
                // Save the QR code image as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading.
            ms.Position = 0;

            // Initialize a barcode reader for QR codes using the memory stream.
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Disable automatic encoding detection to work with raw bytes.
                reader.BarcodeSettings.DetectEncoding = false;

                // Iterate over all detected barcodes (should be one in this case).
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the raw CodeText as returned by the reader.
                    Console.WriteLine($"Raw CodeText: {result.CodeText}");

                    // Convert the raw CodeText string to its original byte representation
                    // using the system's default encoding (the reader's output encoding).
                    byte[] rawBytes = Encoding.Default.GetBytes(result.CodeText);
                    string decodedText;
                    // Prepare a UTF‑8 encoder that does not emit a BOM and throws on invalid bytes.
                    var utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

                    try
                    {
                        // Attempt to decode the raw bytes as UTF‑8.
                        decodedText = utf8.GetString(rawBytes);
                        Console.WriteLine($"Decoded as UTF-8: {decodedText}");
                    }
                    catch (DecoderFallbackException)
                    {
                        // If UTF‑8 decoding fails, fall back to Windows‑1252 encoding.
                        var fallbackEncoding = Encoding.GetEncoding("windows-1252");
                        decodedText = fallbackEncoding.GetString(rawBytes);
                        Console.WriteLine($"Fallback decoded (Windows-1252): {decodedText}");
                    }
                }
            }
        }
    }
}