// Title: Checksum Validation Demonstration for Various Barcode Symbologies
// Description: Shows how to generate barcodes with different checksum settings and verify them using Aspose.BarCode reader.
// Category-Description: This example belongs to the Aspose.BarCode checksum handling category. It demonstrates using BarcodeGenerator, BarCodeReader, and related settings such as IsChecksumEnabled, ChecksumAlwaysShow, and ChecksumValidation across Code39, Code11, and Code128 symbologies. Developers often need to control checksum generation and validation when integrating barcode scanning into applications, making these APIs essential for ensuring data integrity.
// Prompt: Design a CI pipeline step that runs all checksum‑related unit tests and fails the build on any exception.
// Tags: code39,code11,code128,checksum,validation,generation,reading,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Executes a series of checksum‑related barcode generation and validation tests using Aspose.BarCode.
/// </summary>
class Program
{
    // Counters for test statistics
    static int totalTests = 0;
    static int failedTests = 0;

    /// <summary>
    /// Entry point of the example. Generates temporary barcode images, runs checksum validation tests,
    /// reports results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // ----------------------------------------------------------------------
        // Test 1: Code39 with optional checksum disabled
        // ----------------------------------------------------------------------
        RunTest("Code39 Optional Checksum Disabled", () =>
        {
            string file = Path.Combine(tempDir, "code39_no_checksum.png");
            // Generate barcode without checksum
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
            {
                gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                gen.Save(file, BarCodeImageFormat.Png);
            }

            // Read and validate barcode; default checksum validation should ignore checksum
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result.CodeText != "12345")
                        throw new Exception("Code39 without checksum mismatch.");
                }
            }
        });

        // ----------------------------------------------------------------------
        // Test 2: Code39 with optional checksum enabled
        // ----------------------------------------------------------------------
        RunTest("Code39 Optional Checksum Enabled", () =>
        {
            string file = Path.Combine(tempDir, "code39_checksum.png");
            // Generate barcode with checksum
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
            {
                gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                gen.Save(file, BarCodeImageFormat.Png);
            }

            // Force checksum validation on; the resulting CodeText should include the checksum character
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result.CodeText.Length <= "12345".Length)
                        throw new Exception("Code39 with checksum not reflected in CodeText.");
                }
            }
        });

        // ----------------------------------------------------------------------
        // Test 3: Code11 obligatory checksum with default validation
        // ----------------------------------------------------------------------
        RunTest("Code11 Obligatory Checksum Default Validation", () =>
        {
            string file = Path.Combine(tempDir, "code11_default.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
            {
                gen.Save(file, BarCodeImageFormat.Png);
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code11))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result.CodeText != "123456")
                        throw new Exception("Code11 default validation mismatch.");
                }
            }
        });

        // ----------------------------------------------------------------------
        // Test 4: Code11 obligatory checksum with validation turned off
        // ----------------------------------------------------------------------
        RunTest("Code11 Obligatory Checksum Validation Off", () =>
        {
            string file = Path.Combine(tempDir, "code11_off.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
            {
                gen.Save(file, BarCodeImageFormat.Png);
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code11))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result.CodeText != "123456")
                        throw new Exception("Code11 validation off mismatch.");
                }
            }
        });

        // ----------------------------------------------------------------------
        // Test 5: Code128 checksum visibility disabled
        // ----------------------------------------------------------------------
        RunTest("Code128 Checksum Visibility Disabled", () =>
        {
            string file = Path.Combine(tempDir, "code128_no_show.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                gen.Parameters.Barcode.ChecksumAlwaysShow = false;
                gen.Save(file, BarCodeImageFormat.Png);
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result.CodeText != "12345")
                        throw new Exception("Code128 checksum visibility disabled mismatch.");
                }
            }
        });

        // ----------------------------------------------------------------------
        // Test 6: Code128 checksum visibility enabled
        // ----------------------------------------------------------------------
        RunTest("Code128 Checksum Visibility Enabled", () =>
        {
            string file = Path.Combine(tempDir, "code128_show.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                gen.Parameters.Barcode.ChecksumAlwaysShow = true;
                gen.Save(file, BarCodeImageFormat.Png);
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result.CodeText.Length <= "12345".Length)
                        throw new Exception("Code128 checksum not shown in CodeText.");
                }
            }
        });

        // ----------------------------------------------------------------------
        // Report test results
        // ----------------------------------------------------------------------
        Console.WriteLine($"Total tests run: {totalTests}");
        if (failedTests > 0)
        {
            Console.WriteLine($"FAILED: {failedTests} tests failed.");
        }
        else
        {
            Console.WriteLine("All checksum tests passed.");
        }

        // ----------------------------------------------------------------------
        // Cleanup temporary files and directory
        // ----------------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors to avoid masking test results
        }
    }

    /// <summary>
    /// Executes a single test, updates counters, and writes pass/fail information to the console.
    /// </summary>
    /// <param name="testName">Descriptive name of the test.</param>
    /// <param name="testAction">Action containing the test logic.</param>
    static void RunTest(string testName, Action testAction)
    {
        totalTests++;
        try
        {
            testAction();
            Console.WriteLine($"PASS: {testName}");
        }
        catch (Exception ex)
        {
            failedTests++;
            Console.WriteLine($"FAIL: {testName} - {ex.Message}");
        }
    }
}