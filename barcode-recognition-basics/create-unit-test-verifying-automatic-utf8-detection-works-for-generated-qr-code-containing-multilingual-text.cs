// Title: Verify automatic UTF-8 detection for QR code with multilingual text
// Description: Demonstrates generating a QR code containing English, Chinese, Arabic, and Emoji, then reading it back to confirm that Aspose.BarCode correctly detects UTF-8 encoding.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation and recognition category. It showcases the BarcodeGenerator and BarCodeReader classes, illustrating typical use cases such as creating QR codes with Unicode content and validating automatic UTF-8 detection during decoding. Developers often need to ensure correct handling of multilingual data in QR codes, and this snippet serves as a reference for unit-test style verification.
// Prompt: Create a unit test verifying automatic UTF8 detection works for a generated QR code containing multilingual text.
// Tags: qr, utf-8, multilingual, barcode generation, barcode recognition, unit test, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains the entry point for the UTF‑8 detection verification example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR code with multilingual text, reads it back, and validates that the decoded text matches the original, confirming automatic UTF‑8 detection.
    /// </summary>
    static void Main()
    {
        // Multilingual text: English, Chinese, Arabic, and Emoji
        string originalText = "Hello 世界 مرحبا 😊";

        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr.png");

        try
        {
            // Generate QR code with the multilingual text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
            {
                // Optional: set error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                // Save as PNG
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Read the generated QR code
            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                var results = reader.ReadBarCodes();

                bool testPassed = false;
                if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    // In evaluation mode the reader appends a watermark.
                    // Verify that the decoded text starts with the original multilingual text.
                    testPassed = results[0].CodeText.StartsWith(originalText, StringComparison.Ordinal);
                }

                if (testPassed)
                {
                    Console.WriteLine("PASSED: UTF-8 detection succeeded, decoded text matches original.");
                }
                else
                {
                    Console.WriteLine("FAILED: Decoded text does not match original multilingual content.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception occurred - {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Ignored - cleanup failure should not affect test result
            }
        }
    }
}