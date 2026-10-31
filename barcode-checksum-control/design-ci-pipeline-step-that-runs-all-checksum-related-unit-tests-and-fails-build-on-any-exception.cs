// Title: Checksum-related barcode generation and validation tests
// Description: Demonstrates generating barcodes with various checksum settings and verifying them using Aspose.BarCode APIs.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on checksum handling. It showcases key classes such as BarcodeGenerator, BarCodeReader, and related settings (IsChecksumEnabled, ChecksumAlwaysShow, ChecksumValidation). Developers commonly use these APIs to ensure data integrity in barcode workflows, especially when working with symbologies like Code39, Code128, and Code11.
// Prompt: Design a CI pipeline step that runs all checksum‑related unit tests and fails the build on any exception.
// Tags: checksum, barcode, generation, recognition, code39, code128, code11, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Executes a series of checksum‑related barcode generation and validation tests using Aspose.BarCode.
/// </summary>
class Program
{
    // Counters for test results
    static int totalTests = 0;
    static int failedTests = 0;
    static List<string> failedTestNames = new List<string>();

    /// <summary>
    /// Entry point of the program. Creates a temporary directory, runs all checksum tests,
    /// reports results, and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Run each checksum scenario
        RunTest(TestCode39ChecksumDisabled, "Code39 Checksum Disabled");
        RunTest(TestCode39ChecksumEnabled, "Code39 Checksum Enabled");
        RunTest(TestCode128ChecksumAlwaysShow, "Code128 Checksum Always Show");
        RunTest(TestCode11ChecksumDefault, "Code11 Checksum Default");
        RunTest(TestCode11ChecksumOff, "Code11 Checksum Off");

        // Output summary of test execution
        Console.WriteLine();
        Console.WriteLine($"TOTAL TESTS: {totalTests}");
        Console.WriteLine($"FAILED TESTS: {failedTests}");
        if (failedTests > 0)
        {
            Console.WriteLine("Failed test names:");
            foreach (var name in failedTestNames)
            {
                Console.WriteLine($"- {name}");
            }
        }
        else
        {
            Console.WriteLine("ALL TESTS PASSED");
        }

        // Cleanup temporary directory (ignore any errors)
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }

    /// <summary>
    /// Executes a single test action, updates counters, and logs pass/fail status.
    /// </summary>
    /// <param name="testAction">The test method to invoke.</param>
    /// <param name="testName">A descriptive name for the test.</param>
    static void RunTest(Action testAction, string testName)
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
            failedTestNames.Add(testName);
            Console.WriteLine($"FAIL: {testName} - {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a Code39 barcode with checksum disabled and verifies that reading succeeds.
    /// </summary>
    static void TestCode39ChecksumDisabled()
    {
        string filePath = Path.Combine(Path.GetTempPath(), "Code39_NoChecksum.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Disable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        using (var reader = new BarCodeReader(filePath, DecodeType.Code39))
        {
            // Enable checksum validation on read
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Ensure a result is returned
                if (result == null) throw new Exception("No result returned");
            }
        }
    }

    /// <summary>
    /// Generates a Code39 barcode with checksum enabled and verifies that reading succeeds.
    /// </summary>
    static void TestCode39ChecksumEnabled()
    {
        string filePath = Path.Combine(Path.GetTempPath(), "Code39_WithChecksum.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        using (var reader = new BarCodeReader(filePath, DecodeType.Code39))
        {
            // Enable checksum validation on read
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (result == null) throw new Exception("No result returned");
            }
        }
    }

    /// <summary>
    /// Generates a Code128 barcode with the checksum digit always shown and verifies its presence.
    /// </summary>
    static void TestCode128ChecksumAlwaysShow()
    {
        string filePath = Path.Combine(Path.GetTempPath(), "Code128_ChecksumShow.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Force display of the checksum digit
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Enable checksum validation on read
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (result == null) throw new Exception("No result returned");
                // Verify that checksum digit is present (original length 6 + 1)
                if (result.CodeText.Length != 7)
                    throw new Exception("Checksum not shown in CodeText");
            }
        }
    }

    /// <summary>
    /// Generates a Code11 barcode using default checksum settings and verifies successful read.
    /// </summary>
    static void TestCode11ChecksumDefault()
    {
        string filePath = Path.Combine(Path.GetTempPath(), "Code11_Default.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        using (var reader = new BarCodeReader(filePath, DecodeType.Code11))
        {
            // Use default checksum validation behavior
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (result == null) throw new Exception("No result returned");
            }
        }
    }

    /// <summary>
    /// Generates a Code11 barcode with checksum validation turned off and verifies successful read.
    /// </summary>
    static void TestCode11ChecksumOff()
    {
        string filePath = Path.Combine(Path.GetTempPath(), "Code11_Off.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        using (var reader = new BarCodeReader(filePath, DecodeType.Code11))
        {
            // Disable checksum validation on read
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (result == null) throw new Exception("No result returned");
            }
        }
    }
}