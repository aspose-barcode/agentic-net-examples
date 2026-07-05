// Title: Barcode checksum generation and validation example
// Description: Demonstrates generating barcodes with checksum enabled, reading them back with checksum validation, and reporting results.
// Prompt: Design a CI pipeline step that runs all checksum‑related unit tests and fails the build on any exception.
// Tags: barcode, checksum, ean13, code39fullascii, code128, unit-test, aspose

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Executes a series of checksum‑related barcode tests and reports pass/fail results.
/// </summary>
class Program
{
    /// <summary>
    /// Executes a single test, captures any exception, and records failures.
    /// </summary>
    /// <param name="testName">Descriptive name of the test.</param>
    /// <param name="testAction">Action containing the test logic.</param>
    /// <param name="failures">Collection to which failed test names are added.</param>
    static void RunTest(string testName, Action testAction, List<string> failures)
    {
        try
        {
            testAction();
            Console.WriteLine($"PASS: {testName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL: {testName} – {ex.Message}");
            failures.Add(testName);
        }
    }

    /// <summary>
    /// Entry point. Sets up a temporary workspace, runs checksum tests, and reports the overall outcome.
    /// </summary>
    static void Main()
    {
        // Collect names of any tests that fail
        var failedTests = new List<string>();

        // Create a temporary directory for generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeChecksumTests");
        Directory.CreateDirectory(tempDir);

        // ---------- Test 1: EAN13 checksum generation and validation ----------
        RunTest("EAN13_Checksum_Enabled", () =>
        {
            string filePath = Path.Combine(tempDir, "ean13.png");

            // Generate an EAN13 barcode; checksum will be auto‑calculated
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
            {
                // Explicitly enable checksum generation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(filePath);
            }

            // Read the barcode with checksum validation turned ON
            using (var reader = new BarCodeReader(filePath, DecodeType.EAN13))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    throw new InvalidOperationException("No barcode detected.");

                var result = results[0];
                // Verify that the checksum is present and matches the expected value (8)
                if (string.IsNullOrEmpty(result.Extended.OneD.CheckSum))
                    throw new InvalidOperationException("Checksum not retrieved.");
                if (result.Extended.OneD.CheckSum != "8")
                    throw new InvalidOperationException($"Unexpected checksum: {result.Extended.OneD.CheckSum}");
            }

            // Clean up the generated image
            File.Delete(filePath);
        }, failedTests);

        // ---------- Test 2: Code39FullASCII checksum generation and validation ----------
        RunTest("Code39FullASCII_Checksum_Enabled", () =>
        {
            string filePath = Path.Combine(tempDir, "code39.png");

            // Generate a Code39FullASCII barcode with checksum enabled and displayed
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC123"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true; // show checksum in human‑readable text
                generator.Save(filePath);
            }

            // Read the barcode with checksum validation turned ON
            using (var reader = new BarCodeReader(filePath, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    throw new InvalidOperationException("No barcode detected.");

                var result = results[0];
                if (string.IsNullOrEmpty(result.Extended.OneD.CheckSum))
                    throw new InvalidOperationException("Checksum not retrieved.");
                // For Code39FullASCII the checksum is calculated; we just ensure it exists
            }

            // Clean up the generated image
            File.Delete(filePath);
        }, failedTests);

        // ---------- Test 3: Code128 checksum validation (always present) ----------
        RunTest("Code128_Checksum_Validation", () =>
        {
            string filePath = Path.Combine(tempDir, "code128.png");

            // Generate a Code128 barcode (checksum is always included)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                generator.Save(filePath);
            }

            // Read the barcode with checksum validation turned ON
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    throw new InvalidOperationException("No barcode detected.");

                var result = results[0];
                // Code128 does not expose checksum via OneD, but validation succeeds if no exception is thrown
            }

            // Clean up the generated image
            File.Delete(filePath);
        }, failedTests);

        // ---------- Summary ----------
        if (failedTests.Count > 0)
        {
            Console.WriteLine($"FAILED: {failedTests.Count} tests failed.");
        }
        else
        {
            Console.WriteLine("PASSED: All checksum tests passed.");
        }

        // Attempt to delete the temporary folder (ignore any cleanup errors)
        try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }
    }
}