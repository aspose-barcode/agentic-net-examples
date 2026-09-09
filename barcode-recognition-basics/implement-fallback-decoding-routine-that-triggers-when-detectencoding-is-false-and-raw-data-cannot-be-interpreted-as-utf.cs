// Title: QR Code Generation and Fallback Decoding with Custom Encoding
// Description: Demonstrates generating a QR code using a custom Windows-1253 encoding and reading it with DetectEncoding disabled, applying a fallback decoding routine when the default UTF-8 interpretation fails.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for scanning, and handling encoding issues with DetectEncoding. Developers often need to generate barcodes in non‑UTF8 encodings and reliably decode them across different systems, making fallback strategies essential.
// Prompt: Implement a fallback decoding routine that triggers when DetectEncoding is false and raw data cannot be interpreted as UTF8.
// Tags: qr code, custom encoding, fallback decoding, aspose.barcode, barcode generation, barcode recognition, c#

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR code generation with a custom encoding and fallback decoding when automatic encoding detection is disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it without automatic encoding detection,
    /// and applies a fallback decoding routine if the decoded text contains replacement characters.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder for the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "FallbackDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "customEncodingQR.png");

        // --------------------------------------------------------------------
        // Generate a QR code using the Windows-1253 (Greek) encoding.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size.
            string text = "AsposeΣΑΩ";
            Encoding customEncoding = Encoding.GetEncoding(1253);
            generator.SetCodeText(text, customEncoding); // Apply custom encoding.
            generator.Save(imagePath, BarCodeImageFormat.Png); // Save as PNG.
        }

        Console.WriteLine("Barcode generated at: " + imagePath);
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Read the barcode with automatic encoding detection turned off.
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = false; // Disable auto-detection.

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string rawText = result.CodeText;
                // Determine if fallback decoding is needed (presence of replacement char �).
                bool needsFallback = rawText != null && rawText.Contains('�');

                if (needsFallback)
                {
                    // Fallback decoding using the known custom encoding (Windows-1253).
                    string fallbackText = result.GetCodeText(Encoding.GetEncoding(1253));
                    Console.WriteLine("Fallback decoded text: " + fallbackText);
                }
                else
                {
                    Console.WriteLine("Decoded text: " + rawText);
                }

                Console.WriteLine("Code Type: " + result.CodeTypeName);
                Console.WriteLine("Reading Quality: " + result.ReadingQuality);
                Console.WriteLine();
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional).
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}