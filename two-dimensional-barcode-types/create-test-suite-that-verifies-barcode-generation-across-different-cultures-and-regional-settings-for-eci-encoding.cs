// Title: ECI Encoding Verification Across Cultures for QR Barcodes
// Description: Demonstrates generating QR barcodes with ECI encoding for various cultures and verifying the decoded text matches the original.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator and BarCodeReader to create and read QR codes with ECI encodings. Useful for developers needing locale‑specific barcode generation, testing multilingual support, and ensuring correct encoding/decoding across different character sets. Covers key classes EncodeTypes, QREncodeMode, ECIEncodings, BarCodeImageFormat, and DecodeType.
// Prompt: Create a test suite that verifies barcode generation across different cultures and regional settings for ECI encoding.
// Tags: qr, eci, png, barcodegenerator, barcodereader, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates QR barcodes with ECI encoding for multiple cultures,
/// saves them as PNG files, and validates the decoded text using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test suite. Creates barcodes, saves them, reads them back,
    /// and reports pass/fail results for each culture-specific test case.
    /// </summary>
    static void Main()
    {
        // Define test cases: culture name, sample text, and the corresponding ECI encoding.
        var testCases = new List<(string Culture, string Text, ECIEncodings Encoding)>
        {
            ("Japanese", "こんにちは", ECIEncodings.Shift_JIS),
            ("Russian", "Привет", ECIEncodings.Win1251),
            ("Arabic", "مرحبا", ECIEncodings.ISO_8859_6),
            ("ChineseSimplified", "你好", ECIEncodings.GB2312),
            ("Greek", "Γειά", ECIEncodings.ISO_8859_7)
        };

        // Create a unique temporary folder for generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeECITest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");
        Console.WriteLine();

        // Iterate over each test case, generate the barcode, and verify decoding.
        foreach (var (culture, text, encoding) in testCases)
        {
            // Build the file path for the current culture's barcode image.
            string filePath = Path.Combine(outputFolder, $"{culture}.png");

            // Generate a QR barcode with the specified ECI encoding.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = encoding;
                generator.CodeText = text;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read back the barcode image and extract the decoded text.
            string decodedText = null;
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    decodedText = result.CodeText;
                    break; // Expect only one barcode per image.
                }
            }

            // Determine if the decoded text matches the original input.
            bool passed = decodedText != null && decodedText == text;
            Console.WriteLine($"{culture} ({encoding}): {(passed ? "PASS" : "FAIL")}");
            if (!passed)
            {
                Console.WriteLine($"  Expected: {text}");
                Console.WriteLine($"  Decoded : {decodedText ?? "null"}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Test suite completed.");
    }
}