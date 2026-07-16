// Title: Barcode Generation and Verification with ECI Encoding Across Cultures
// Description: Demonstrates generating QR barcodes using ECI encoding for different cultural texts and verifies them by reading back the encoded data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to handle multilingual content via ECI encodings. Developers often need to ensure correct encoding for international applications, making this pattern useful for testing and validation across locales.
// Prompt: Create a test suite that verifies barcode generation across different cultures and regional settings for ECI encoding.
// Tags: qr, eci, barcode generation, barcode recognition, png, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition, culture, localization

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates QR barcodes with ECI encoding for various cultures, saves them as PNG files,
/// and validates the encoded text by reading the barcodes back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test suite. Executes barcode generation and verification for each test case.
    /// </summary>
    static void Main()
    {
        // Define test cases: culture identifier, sample text, and the corresponding ECI encoding.
        var testCases = new List<(string Culture, string Text, ECIEncodings Encoding)>
        {
            ("en-US", "Hello", ECIEncodings.UTF8),
            ("ja-JP", "こんにちは", ECIEncodings.Shift_JIS),
            ("ru-RU", "Привет", ECIEncodings.ISO_8859_5),
            ("ar-SA", "مرحبا", ECIEncodings.ISO_8859_6),
            ("zh-CN", "你好", ECIEncodings.GB18030)
        };

        // Ensure the output directory exists for storing generated barcode images.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each test case: generate, save, read, and verify the barcode.
        foreach (var (culture, text, encoding) in testCases)
        {
            // Construct the file path for the current culture's barcode image.
            string filePath = Path.Combine(outputDir, $"{culture}.png");

            // Generate a QR barcode with the specified ECI encoding.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = text;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECIEncoding;
                generator.Parameters.Barcode.QR.ECIEncoding = encoding;
                generator.Save(filePath);
            }

            // Read the generated barcode and verify that the decoded text matches the original.
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                // Enable automatic detection of the encoding used in the barcode.
                reader.BarcodeSettings.DetectEncoding = true;

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"[{culture}] No barcode detected.");
                    continue;
                }

                string decoded = results[0].CodeText;
                bool match = decoded == text;
                Console.WriteLine($"[{culture}] Original: \"{text}\" | Decoded: \"{decoded}\" | Match: {match}");
            }
        }

        // Optional cleanup: remove generated files and directory.
        // foreach (var file in Directory.GetFiles(outputDir, "*.png"))
        // {
        //     File.Delete(file);
        // }
        // Directory.Delete(outputDir);
    }
}