// Title: Checksum Validation Example for EAN13 Barcodes
// Description: Demonstrates generating EAN13 barcodes with and without checksum and reading them with checksum validation toggled.
// Category-Description: This example belongs to the Aspose.BarCode checksum handling category. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings such as EnableChecksum and ChecksumValidation. Typical use cases include validating data integrity during barcode generation and recognition, especially for retail and logistics applications where EAN13 is common. Developers often need to enable or disable checksum generation and validation to meet specific business rules or legacy system requirements.
/// Prompt: Design a CI pipeline step that runs all checksum‑related unit tests and fails the build on any exception.
/// Tags: ean13, checksum, barcode generation, barcode reading, aspose.barcode, validation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains a simple console program that runs a series of checksum‑related barcode tests
/// using Aspose.BarCode. The program generates barcodes, reads them with different
/// checksum validation settings, and reports any failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Executes three checksum tests and prints a summary.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for test images
        string testDir = Path.Combine(Path.GetTempPath(), "ChecksumTests");
        Directory.CreateDirectory(testDir);

        // Collect any test failures for later reporting
        var failures = new List<string>();

        // ------------------------------------------------------------
        // Test 1: Generate EAN13 barcode with checksum (default) and read with validation OFF
        // ------------------------------------------------------------
        try
        {
            string ean13Path = Path.Combine(testDir, "ean13.png");

            // Generate barcode; checksum is automatically added for EAN13
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
            {
                generator.Save(ean13Path);
            }

            // Read the barcode with checksum validation disabled
            using (var reader = new BarCodeReader(ean13Path, DecodeType.EAN13))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                bool found = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    found = true;
                    if (result.CodeText != "1234567890128")
                    {
                        failures.Add("Test1: CodeText mismatch when checksum validation is Off.");
                    }
                }

                if (!found)
                {
                    failures.Add("Test1: No barcode detected when checksum validation is Off.");
                }
            }
        }
        catch (Exception ex)
        {
            failures.Add($"Test1: Exception occurred - {ex.Message}");
        }

        // ------------------------------------------------------------
        // Test 2: Read the same barcode with checksum validation ON
        // ------------------------------------------------------------
        try
        {
            string ean13Path = Path.Combine(testDir, "ean13.png");

            using (var reader = new BarCodeReader(ean13Path, DecodeType.EAN13))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                bool found = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    found = true;
                    if (result.CodeText != "1234567890128")
                    {
                        failures.Add("Test2: CodeText mismatch when checksum validation is On.");
                    }
                }

                if (!found)
                {
                    failures.Add("Test2: No barcode detected when checksum validation is On.");
                }
            }
        }
        catch (Exception ex)
        {
            failures.Add($"Test2: Exception occurred - {ex.Message}");
        }

        // ------------------------------------------------------------
        // Test 3: Generate barcode with checksum disabled and verify reading with validation OFF
        // ------------------------------------------------------------
        try
        {
            string ean13NoChecksumPath = Path.Combine(testDir, "ean13_nochecksum.png");

            // Generate barcode without checksum (EnableChecksum.No)
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                generator.Save(ean13NoChecksumPath);
            }

            // Read the barcode with checksum validation disabled (should succeed)
            using (var reader = new BarCodeReader(ean13NoChecksumPath, DecodeType.EAN13))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                bool found = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    found = true;
                    // When checksum is disabled, the code text should match the 12‑digit input
                    if (result.CodeText != "123456789012")
                    {
                        failures.Add("Test3: CodeText mismatch for barcode generated without checksum.");
                    }
                }

                if (!found)
                {
                    failures.Add("Test3: No barcode detected for image without checksum.");
                }
            }
        }
        catch (Exception ex)
        {
            failures.Add($"Test3: Exception occurred - {ex.Message}");
        }

        // ------------------------------------------------------------
        // Summary output
        // ------------------------------------------------------------
        if (failures.Count > 0)
        {
            Console.WriteLine($"FAILED: {failures.Count} tests failed.");
            foreach (var msg in failures)
            {
                Console.WriteLine(msg);
            }
        }
        else
        {
            Console.WriteLine("PASSED: All checksum tests passed.");
        }

        // ------------------------------------------------------------
        // Clean up test files (optional)
        // ------------------------------------------------------------
        try
        {
            if (Directory.Exists(testDir))
            {
                Directory.Delete(testDir, true);
            }
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}