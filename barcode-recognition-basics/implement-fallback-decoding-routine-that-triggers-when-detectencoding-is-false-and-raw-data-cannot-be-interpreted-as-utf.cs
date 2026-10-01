// Title: QR Code Generation and Decoding with Encoding Fallback
// Description: Demonstrates generating a QR barcode, reading it with encoding detection disabled, and decoding the raw bytes using UTF‑8 with a fallback to the system default encoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for scanning images, and handling raw byte data when DetectEncoding is turned off. Developers often need to control encoding handling for non‑UTF8 data, making this pattern useful for custom decoding scenarios.
// Prompt: Implement a fallback decoding routine that triggers when DetectEncoding is false and raw data cannot be interpreted as UTF8.
// Tags: qr, barcode generation, barcode recognition, encoding fallback, utf8, aspose.barcode, c#

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that creates a QR barcode, reads it without automatic encoding detection,
/// and decodes the raw bytes using UTF‑8 with a fallback to the system default encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode with some sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Save the barcode image using default settings
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Create a BarCodeReader with DetectEncoding disabled
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Disable automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = false;

            // Read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                // Decode the raw bytes with UTF‑8 fallback logic
                string decodedText = DecodeWithFallback(result);
                Console.WriteLine($"Decoded Text: {decodedText}");
                Console.WriteLine($"Symbology: {result.CodeTypeName}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }

    /// <summary>
    /// Decodes the barcode result using UTF‑8 when possible; otherwise falls back to the default encoding.
    /// </summary>
    /// <param name="result">The barcode result containing raw bytes.</param>
    /// <returns>The decoded string.</returns>
    private static string DecodeWithFallback(BarCodeResult result)
    {
        // If raw bytes are unavailable, return the already decoded text (or empty string)
        if (result.CodeBytes == null || result.CodeBytes.Length == 0)
        {
            return result.CodeText ?? string.Empty;
        }

        // UTF‑8 decoder that throws on invalid byte sequences
        Encoding utf8Strict = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
        try
        {
            return utf8Strict.GetString(result.CodeBytes);
        }
        catch (DecoderFallbackException)
        {
            // Fallback to the system's default encoding when UTF‑8 decoding fails
            return Encoding.Default.GetString(result.CodeBytes);
        }
    }
}