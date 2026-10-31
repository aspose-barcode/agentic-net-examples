// Title: QR Code Generation with Custom Encoding and Fallback Decoding
// Description: Demonstrates generating a QR code using Windows-1253 encoding, then reading it with encoding detection disabled and applying a fallback decoding routine when UTF-8 conversion fails.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes with a specific text encoding and the BarCodeReader for extracting raw data. Developers often need to control encoding handling, disable automatic detection, and provide custom fallback logic when the default UTF-8 interpretation is unsuitable.
// Prompt: Implement a fallback decoding routine that triggers when DetectEncoding is false and raw data cannot be interpreted as UTF8.
// Tags: qr code, encoding, fallback decoding, aspose.barcode, barcode generation, barcode recognition, windows-1253, utf8

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR code creation with a custom Windows‑1253 encoding and a manual fallback decoding strategy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it without automatic encoding detection,
    /// and applies a fallback decoding routine when UTF‑8 conversion is not possible.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr_custom.png");

        // ---------------------------------------------------------------
        // Generate a QR code using Windows‑1253 (Greek) encoding.
        // ---------------------------------------------------------------
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 8;                     // Set module size.
            gen.SetCodeText("AsposeΣΑΩ", Encoding.GetEncoding(1253));          // Apply custom encoding.
            gen.Save(imagePath, BarCodeImageFormat.Png);                      // Save as PNG.
        }

        // ---------------------------------------------------------------
        // Read the barcode with automatic encoding detection turned off.
        // ---------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = false;                    // Disable auto‑detect.

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the raw data as UTF‑8.
                string utf8Text = result.GetCodeText(Encoding.UTF8);

                // If UTF‑8 decoding produced replacement characters, fall back to the original encoding.
                if (utf8Text.Contains("\uFFFD") || utf8Text.Contains("?"))
                {
                    string fallbackText = result.GetCodeText(Encoding.GetEncoding(1253));
                    Console.WriteLine($"Fallback decoded text: {fallbackText}");
                }
                else
                {
                    Console.WriteLine($"UTF8 decoded text: {utf8Text}");
                }
            }
        }

        // ---------------------------------------------------------------
        // Clean up temporary files and directories.
        // ---------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}