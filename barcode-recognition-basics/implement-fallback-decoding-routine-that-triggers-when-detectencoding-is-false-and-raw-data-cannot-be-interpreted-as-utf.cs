// Title: QR Code Generation with Custom Encoding and Fallback Decoding
// Description: Demonstrates generating a QR code using a custom Windows-1253 (Greek) encoding, then reading it with encoding detection disabled and applying a UTF-8 fallback strategy.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for scanning, and the BarcodeSettings.DetectEncoding property to control automatic character set detection. Developers often need to handle custom encodings and provide fallback decoding logic when raw barcode data cannot be interpreted as UTF-8.
// Prompt: Implement a fallback decoding routine that triggers when DetectEncoding is false and raw data cannot be interpreted as UTF8.
// Tags: qr code, custom encoding, fallback decoding, aspose.barcode, barcode generation, barcode recognition, utf8, windows-1253

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR code generation with a custom encoding and a fallback decoding routine when UTF‑8 decoding fails.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it without automatic encoding detection,
    /// attempts UTF‑8 decoding, and falls back to the original custom encoding if needed.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample_qr.png");

        // ------------------------------------------------------------
        // Generate a QR code using a custom Windows-1253 (Greek) encoding
        // ------------------------------------------------------------
        Encoding customEncoding = Encoding.GetEncoding(1253); // Greek code page
        string codeText = "AsposeΣΑΩ";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the code text with the custom encoding
            generator.SetCodeText(codeText, customEncoding);
            // Adjust QR module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 8;
            // Save the generated QR code as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode with automatic encoding detection turned off
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.QR;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Disable automatic detection of character encoding
            reader.BarcodeSettings.DetectEncoding = false;

            // Iterate through all detected barcodes (only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");

                // --------------------------------------------------------
                // Attempt to decode the raw bytes as strict UTF-8
                // --------------------------------------------------------
                byte[] rawBytes = result.CodeBytes;
                string decodedText;
                try
                {
                    // Throw on invalid UTF-8 sequences to trigger fallback
                    Encoding utf8Strict = new UTF8Encoding(false, true);
                    decodedText = utf8Strict.GetString(rawBytes);
                    Console.WriteLine($"UTF8 Decoded Text: {decodedText}");
                }
                catch (DecoderFallbackException)
                {
                    // ----------------------------------------------------
                    // Fallback: decode using the original custom Windows-1253 encoding
                    // ----------------------------------------------------
                    decodedText = customEncoding.GetString(rawBytes);
                    Console.WriteLine($"Fallback Decoded Text (1253): {decodedText}");
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program exit
        }
    }
}