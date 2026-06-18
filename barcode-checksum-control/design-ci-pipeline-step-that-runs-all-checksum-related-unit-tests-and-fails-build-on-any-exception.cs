using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates checksum handling for various barcode types using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes a series of checksum validation tests.
    /// </summary>
    static void Main()
    {
        // Collect any test failures for reporting at the end.
        var failures = new List<string>();

        // ------------------------------------------------------------
        // Test 1: Code39 with checksum enabled, read with validation On
        // ------------------------------------------------------------
        try
        {
            // Create a temporary file path for the generated barcode image.
            string file = Path.Combine(Path.GetTempPath(), "code39_enabled.png");

            // Generate a Code39 barcode with checksum enabled.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "12345"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(file);
            }

            // Read the barcode back, enforcing checksum validation.
            using (var reader = new BarCodeReader(file, DecodeType.Code39FullASCII))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();

                // Verify that the barcode was read correctly.
                if (results.Length == 0 || results[0].CodeText != "12345")
                    failures.Add("Test 1 failed: Unexpected read result.");
            }

            // Clean up the temporary file.
            File.Delete(file);
        }
        catch (Exception ex)
        {
            failures.Add($"Test 1 exception: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Test 2: Code39 with checksum disabled, read with validation Off
        // ------------------------------------------------------------
        try
        {
            string file = Path.Combine(Path.GetTempPath(), "code39_disabled.png");

            // Generate a Code39 barcode with checksum disabled.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "12345"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                generator.Save(file);
            }

            // Read the barcode back, disabling checksum validation.
            using (var reader = new BarCodeReader(file, DecodeType.Code39FullASCII))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                var results = reader.ReadBarCodes();

                // Verify that the barcode was read correctly.
                if (results.Length == 0 || results[0].CodeText != "12345")
                    failures.Add("Test 2 failed: Unexpected read result.");
            }

            File.Delete(file);
        }
        catch (Exception ex)
        {
            failures.Add($"Test 2 exception: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Test 3: EAN13 (checksum mandatory) default settings, read with validation On
        // ------------------------------------------------------------
        try
        {
            string file = Path.Combine(Path.GetTempPath(), "ean13.png");

            // Generate an EAN13 barcode; checksum is mandatory and enabled by default.
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
            {
                // No explicit checksum setting; default should be Yes.
                generator.Save(file);
            }

            // Read the barcode back, enforcing checksum validation.
            using (var reader = new BarCodeReader(file, DecodeType.EAN13))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();

                // Verify that the barcode was read correctly.
                if (results.Length == 0 || results[0].CodeText != "123456789012")
                    failures.Add("Test 3 failed: Unexpected read result.");
            }

            File.Delete(file);
        }
        catch (Exception ex)
        {
            failures.Add($"Test 3 exception: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Test 4: Attempt to disable checksum for Code128 (should throw)
        // ------------------------------------------------------------
        try
        {
            // Attempt to generate a Code128 barcode with checksum disabled.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // The above line should throw; if it doesn't, fail the test.
                failures.Add("Test 4 failed: No exception when disabling checksum for Code128.");
            }
        }
        catch (Exception)
        {
            // Expected exception; test passes.
        }

        // -----------------
        // Summary of results
        // -----------------
        if (failures.Count == 0)
        {
            Console.WriteLine("ALL CHECKSUM TESTS PASSED.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failures.Count} test(s) failed.");
            foreach (var msg in failures)
            {
                Console.WriteLine(msg);
            }
        }
    }
}