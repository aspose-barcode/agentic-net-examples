using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating QR barcodes with various ECI encodings,
/// reading them back, and verifying the decoded text matches the original.
/// </summary>
class Program
{
    /// <summary>
    /// Represents a single test case containing the text to encode and the desired ECI encoding.
    /// </summary>
    private class TestCase
    {
        public string Text { get; set; }
        public ECIEncodings Encoding { get; set; }

        public TestCase(string text, ECIEncodings encoding)
        {
            Text = text;
            Encoding = encoding;
        }
    }

    /// <summary>
    /// Entry point of the application. Executes a series of QR barcode tests with different cultures and encodings.
    /// </summary>
    static void Main()
    {
        // Define a collection of test cases covering multiple languages and ECI encodings.
        var testCases = new List<TestCase>
        {
            new TestCase("Hello World", ECIEncodings.ISO_8859_1), // Latin (ISO-8859-1)
            new TestCase("Привет", ECIEncodings.UTF8),           // Russian (UTF-8)
            new TestCase("こんにちは", ECIEncodings.Shift_JIS),   // Japanese (Shift_JIS)
            new TestCase("مرحبا", ECIEncodings.Win1256),         // Arabic (Windows-1256)
            new TestCase("你好", ECIEncodings.UTF8)              // Chinese (UTF-8)
        };

        // Execute each test case and output the result.
        foreach (var test in testCases)
        {
            bool success = RunEciQrTest(test.Text, test.Encoding);
            Console.WriteLine($"Text: \"{test.Text}\" | Encoding: {test.Encoding} => {(success ? "PASS" : "FAIL")}");
        }
    }

    /// <summary>
    /// Generates a QR barcode using the specified ECI encoding, reads it back,
    /// and verifies that the decoded text matches the original input.
    /// </summary>
    /// <param name="codeText">The text to encode into the QR barcode.</param>
    /// <param name="eciEncoding">The ECI encoding to apply.</param>
    /// <returns>True if the decoded text matches the original; otherwise, false.</returns>
    private static bool RunEciQrTest(string codeText, ECIEncodings eciEncoding)
    {
        // Create a barcode generator for QR type with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure the generator to use ECI mode and set the desired encoding.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = eciEncoding;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Initialize a barcode reader to decode the QR code from the stream.
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    // Ensure the reader attempts to detect the encoding (default is true).
                    reader.BarcodeSettings.DetectEncoding = true;

                    // Read all barcodes found in the stream.
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        // Verify that the decoded text matches the original input.
                        if (result.CodeText == codeText)
                        {
                            return true; // Test passed.
                        }
                    }
                }
            }
        }

        return false; // Test failed.
    }
}