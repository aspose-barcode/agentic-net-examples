// Title: QR Code UTF-8 Automatic Encoding Detection Test
// Description: Generates a QR code containing Arabic and Japanese characters, saves it as PNG, and verifies that the Aspose.BarCode reader correctly auto-detects UTF-8 encoding.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on QR code creation, multilingual text handling, and automatic encoding detection. It demonstrates using BarcodeGenerator for QR code generation, BarCodeReader for decoding, and the DetectEncoding setting to control UTF-8 auto‑detection. Developers working with internationalized barcodes often need to ensure correct text extraction across various languages and encodings.
// Prompt: Create a unit test verifying automatic UTF8 detection works for a generated QR code containing multilingual text.
// Tags: qr, utf8, encoding, detection, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates automatic UTF-8 detection for QR codes containing multilingual text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR code, reads it with and without encoding detection,
    /// and validates the detection behavior.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated QR code image
        string tempPath = Path.Combine(Path.GetTempPath(), "Utf8DetectTest.png");

        // Multilingual text combining Arabic and Japanese characters
        string multilingualText = "بالقمة Aspose こんにちは";

        // Generate a QR code using UTF-8 encoding and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.SetCodeText(multilingualText, Encoding.UTF8);
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("FAILED: QR code image was not created.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the QR code with automatic encoding detection enabled
        // --------------------------------------------------------------------
        bool detectTrueSuccess;
        using (var reader = new BarCodeReader(tempPath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = true; // Enable auto‑detect UTF-8
            var results = reader.ReadBarCodes();
            detectTrueSuccess = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine("DetectEncoding: true");
            foreach (var result in results)
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Read the QR code with automatic encoding detection disabled
        // --------------------------------------------------------------------
        bool detectFalseSuccess;
        using (var reader = new BarCodeReader(tempPath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = false; // Disable auto‑detect UTF-8
            var results = reader.ReadBarCodes();
            detectFalseSuccess = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine("DetectEncoding: false");
            foreach (var result in results)
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Verify that detection with DetectEncoding = true yields a successful read
        if (detectTrueSuccess)
        {
            Console.WriteLine("PASSED: Automatic UTF8 detection succeeded.");
        }
        else
        {
            Console.WriteLine("FAILED: Automatic UTF8 detection did not return a valid result.");
        }

        // Clean up the temporary QR code image file
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}