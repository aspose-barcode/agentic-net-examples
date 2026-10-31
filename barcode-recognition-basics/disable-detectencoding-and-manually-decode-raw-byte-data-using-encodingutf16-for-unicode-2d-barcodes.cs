// Title: Decode Unicode QR Code with Manual UTF-16 Decoding
// Description: Demonstrates generating a QR code with Unicode text using UTF-16 encoding, then reading it with DetectEncoding disabled and manually decoding the raw bytes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create 2D barcodes with custom text encoding and BarCodeReader to extract raw byte data when automatic encoding detection is turned off. Developers often need to control encoding for Unicode symbols in QR codes, especially when interoperating with systems that require specific character sets.
// Prompt: Disable DetectEncoding and manually decode raw byte data using Encoding.UTF16 for Unicode 2D barcodes.
// Tags: qr,unicode,utf-16,detectencoding,barcode-generation,barcode-recognition,aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates disabling DetectEncoding and manually decoding raw byte data using UTF-16 for a Unicode QR code.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "qr_utf16.png");

        // --------------------------------------------------------------------
        // Define the Unicode text to encode in the QR code
        // --------------------------------------------------------------------
        string originalText = "Aspose Unicode テスト";

        // --------------------------------------------------------------------
        // Generate a QR code using UTF-16 (Unicode) encoding
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 8; // Set module size
            generator.SetCodeText(originalText, Encoding.Unicode); // Apply manual UTF-16 encoding
            generator.Save(barcodePath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode with automatic encoding detection turned off
        // and decode the raw bytes manually using UTF-16
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = false; // Disable auto-detection
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                byte[] rawBytes = result.CodeBytes; // Retrieve raw byte data
                string decodedText = Encoding.Unicode.GetString(rawBytes); // Manual UTF-16 decode
                Console.WriteLine($"Decoded Text: {decodedText}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}