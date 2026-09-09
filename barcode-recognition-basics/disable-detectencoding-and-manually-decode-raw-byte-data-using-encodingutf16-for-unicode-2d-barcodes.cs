// Title: Disable DetectEncoding and manually decode Unicode QR barcode using UTF-16
// Description: Demonstrates generating a QR code with UTF-16 encoded text, disabling automatic encoding detection during reading, and manually decoding the raw bytes to retrieve the original Unicode string.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating 2D barcodes and BarCodeReader for extracting raw byte data. Developers often need to control encoding handling for Unicode barcodes, especially when automatic detection is unsuitable. The key API classes include BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and related settings.
// Prompt: Disable DetectEncoding and manually decode raw byte data using Encoding.UTF16 for Unicode 2D barcodes.
// Tags: qr, unicode, encoding, detectencoding, manual-decoding, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code with UTF-16 encoded text,
/// reads it with automatic encoding detection disabled, and manually decodes the raw bytes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, reading, and cleanup.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare temporary directory and file paths
        // ------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "unicode_qr.png");

        // ------------------------------------------------------------
        // Define Unicode text to encode (Japanese "Hello")
        // ------------------------------------------------------------
        string unicodeText = "こんにちは";

        // ------------------------------------------------------------
        // Generate QR code using raw UTF-16 bytes (manual encoding)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set module size (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 8;

            // Encode the text as UTF-16 (Unicode) without adding an ECI designator
            generator.SetCodeText(unicodeText, Encoding.Unicode);

            // Optional: set display text for the human‑readable part of the barcode
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = unicodeText;

            // Save the generated barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with DetectEncoding disabled and decode manually
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Turn off automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== Read with DetectEncoding = false ===");
                Console.WriteLine($"Raw CodeText (as string): {result.CodeText}");

                // Manually decode the raw byte array using UTF-16 (Unicode)
                string manualDecoded = Encoding.Unicode.GetString(result.CodeBytes);
                Console.WriteLine($"Manually decoded (UTF-16): {manualDecoded}");
            }
        }

        // ------------------------------------------------------------
        // Optional: demonstrate automatic detection for comparison
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Enable automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = true;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== Read with DetectEncoding = true ===");
                Console.WriteLine($"Auto decoded CodeText: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directory
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}