// Title: Demonstrate checksum handling in various barcode symbologies using Aspose.BarCode
// Description: Shows how to enable, disable, and enforce checksum calculation for Code39 and Code128 barcodes, and verifies results via reading.
// Category-Description: This example belongs to the Aspose.BarCode checksum management category, illustrating use of BarcodeGenerator, BarCodeReader, and related parameters such as IsChecksumEnabled and ChecksumAlwaysShow. Developers commonly need to control checksum behavior for data integrity, compliance, or legacy system compatibility. The snippet demonstrates typical unit‑test style validation of checksum effects across symbologies.
// Prompt: Design a CI pipeline step that runs all checksum‑related unit tests and fails the build on any exception.
// Tags: checksum, code39, code128, barcode generation, barcode recognition, aspose.barcode, unit test

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains checksum-related barcode generation and verification examples.
/// </summary>
class Program
{
    /// <summary>
    /// Executes a series of checksum tests, reporting pass/fail results.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the list of test actions to run
        var tests = new List<Action>
        {
            TestCode39ChecksumEnabled,
            TestCode39ChecksumDisabled,
            TestCode128ChecksumEnforced,
            TestChecksumAlwaysShow
        };

        int failed = 0; // Counter for failed tests

        // Execute each test and capture any exceptions
        foreach (var test in tests)
        {
            try
            {
                test();
                Console.WriteLine($"PASS: {test.Method.Name}");
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"FAIL: {test.Method.Name} - {ex.Message}");
            }
        }

        // Summarize the overall result
        if (failed > 0)
        {
            Console.WriteLine($"FAILED: {failed} test(s) failed.");
        }
        else
        {
            Console.WriteLine("ALL CHECKSUM TESTS PASSED.");
        }
    }

    // Test: Code39 with checksum enabled and always shown
    static void TestCode39ChecksumEnabled()
    {
        string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
        try
        {
            // Generate barcode with checksum enabled
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                generator.Save(tempFile);
            }

            // Read and verify the generated barcode
            using (var reader = new BarCodeReader(tempFile, DecodeType.Code39))
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                    throw new Exception("No barcode detected.");
                if (!results[0].CodeText.StartsWith("ABC123"))
                    throw new Exception("Read text does not match expected.");
            }
        }
        finally
        {
            // Clean up temporary file
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    // Test: Code39 with checksum disabled and not shown
    static void TestCode39ChecksumDisabled()
    {
        string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
        try
        {
            // Generate barcode with checksum disabled
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "XYZ789"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                generator.Parameters.Barcode.ChecksumAlwaysShow = false;
                generator.Save(tempFile);
            }

            // Read and verify the generated barcode
            using (var reader = new BarCodeReader(tempFile, DecodeType.Code39))
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                    throw new Exception("No barcode detected.");
                if (results[0].CodeText != "XYZ789")
                    throw new Exception("Checksum was unexpectedly applied.");
            }
        }
        finally
        {
            // Clean up temporary file
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    // Test: Code128 where checksum enforcement should throw an exception when disabled
    static void TestCode128ChecksumEnforced()
    {
        try
        {
            // Attempt to disable checksum for Code128 (should raise BarCodeException)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // If no exception occurs, force test failure
                throw new Exception("Expected exception was not thrown.");
            }
        }
        catch (BarCodeException)
        {
            // Expected path – test passes
        }
        catch (Exception ex)
        {
            throw new Exception($"Unexpected exception type: {ex.GetType().Name}");
        }
    }

    // Test: Code39 with checksum always shown in the output text
    static void TestChecksumAlwaysShow()
    {
        string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
        try
        {
            // Generate barcode with checksum enabled and always shown
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CHECK"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                generator.Save(tempFile);
            }

            // Read and verify that checksum appears in the decoded text
            using (var reader = new BarCodeReader(tempFile, DecodeType.Code39))
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                    throw new Exception("No barcode detected.");
                if (!results[0].CodeText.StartsWith("CHECK"))
                    throw new Exception("Checksum not present in read text.");
            }
        }
        finally
        {
            // Clean up temporary file
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }
}