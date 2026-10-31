// Title: Barcode Generation Test Across Cultures with ECI Encoding
// Description: Demonstrates generating QR barcodes using ECI encoding for different cultures and verifying the decoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to create QR codes with Extended Channel Interpretation (ECI) encoding, a common requirement when handling multilingual text. Developers often need to ensure that barcodes are correctly generated and decoded across various regional settings, making this pattern useful for automated testing and internationalization scenarios.
// Prompt: Create a test suite that verifies barcode generation across different cultures and regional settings for ECI encoding.
// Tags: barcode symbology, eci encoding, culture, qr, generation, recognition, testing, aspose.barcode

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple test suite that generates QR barcodes with ECI encoding for multiple cultures,
/// then reads them back to verify that the decoded text matches the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test suite. Generates, reads, and validates QR codes for a set of culture-specific test cases.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeECITest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases: each includes a culture identifier, sample text, and the corresponding ECI encoding.
        var testCases = new List<(string CultureName, string Text, ECIEncodings Encoding)>
        {
            ("en-US", "Hello World", ECIEncodings.UTF8),
            ("el-GR", "ΑΒΓΔΕ", ECIEncodings.ISO_8859_7),   // Greek
            ("ru-RU", "Привет", ECIEncodings.Win1251),    // Cyrillic
            ("ja-JP", "こんにちは", ECIEncodings.UTF8)    // Japanese (UTF8)
        };

        // Iterate over each test case, adjusting the current thread culture accordingly.
        foreach (var (cultureName, text, eciEncoding) in testCases)
        {
            // Apply the culture for the current iteration.
            CultureInfo culture = new CultureInfo(cultureName);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            // Build the output file path for the generated QR code image.
            string fileName = $"QR_{cultureName}.png";
            string filePath = Path.Combine(tempFolder, fileName);

            try
            {
                // Generate a QR code with ECI mode enabled.
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
                {
                    generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                    generator.Parameters.Barcode.QR.ECIEncoding = eciEncoding;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Prepare a reader to decode the generated QR code.
                BaseDecodeType decodeType = DecodeType.QR;
                using (var reader = new BarCodeReader(filePath, decodeType))
                {
                    // Use high‑performance quality settings for faster decoding.
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Attempt to read the barcode(s) from the image.
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"[FAIL] No barcode detected for culture {cultureName}.");
                    }
                    else
                    {
                        // Compare the decoded text with the original input.
                        string decodedText = results[0].CodeText;
                        bool match = decodedText == text;
                        Console.WriteLine($"[ {(match ? "PASS" : "FAIL")} ] Culture: {cultureName}, Original: \"{text}\", Decoded: \"{decodedText}\"");
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any unexpected errors that occur during generation or decoding.
                Console.WriteLine($"[ERROR] Culture: {cultureName}, Exception: {ex.Message}");
            }
        }

        // Cleanup: optionally delete temporary files (commented out to allow inspection).
        // Directory.Delete(tempFolder, true);
    }
}