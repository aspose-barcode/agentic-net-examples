// Title: Checksum Visibility Toggle Example
// Description: Demonstrates how to enable or disable rendering of the checksum digit in a Code128 barcode using Aspose.BarCode, and verifies the result by reading the generated images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator and BarCodeReader classes. It illustrates typical scenarios where developers need to control checksum display for compliance or readability, and how to validate the output programmatically.
// Prompt: Write a unit test confirming the checksum visibility property correctly toggles rendering of the checksum digit.
// Tags: code128, checksum, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Code128 barcodes with and without checksum visibility,
/// reads them back, and verifies that the checksum digit is rendered according to the setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, validates checksum visibility,
    /// prints test results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumVisibilityTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Base data for the barcode
        string codeText = "12345";

        // Paths for the two test images
        string falsePath = Path.Combine(tempDir, "code128_noChecksumShow.png");
        string truePath = Path.Combine(tempDir, "code128_showChecksum.png");

        // ------------------------------------------------------------
        // Generate barcode without showing the checksum digit
        // ------------------------------------------------------------
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;   // Enable checksum calculation
            gen.Parameters.Barcode.ChecksumAlwaysShow = false;               // Do NOT render the checksum digit
            gen.Save(falsePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Generate barcode with the checksum digit visible
        // ------------------------------------------------------------
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;   // Enable checksum calculation
            gen.Parameters.Barcode.ChecksumAlwaysShow = true;                // Render the checksum digit
            gen.Save(truePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the barcode without checksum visibility contains only the original data
        // ------------------------------------------------------------
        bool testNoShowPassed = false;
        if (File.Exists(falsePath))
        {
            using (BarCodeReader reader = new BarCodeReader(falsePath, DecodeType.Code128))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    testNoShowPassed = results[0].CodeText == codeText;
                }
            }
        }

        // ------------------------------------------------------------
        // Verify that the barcode with checksum visibility includes an extra checksum digit
        // ------------------------------------------------------------
        bool testShowPassed = false;
        if (File.Exists(truePath))
        {
            using (BarCodeReader reader = new BarCodeReader(truePath, DecodeType.Code128))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    string readText = results[0].CodeText;
                    testShowPassed = readText.StartsWith(codeText) && readText.Length == codeText.Length + 1;
                }
            }
        }

        // Output test results
        Console.WriteLine($"Checksum visibility OFF test: {(testNoShowPassed ? "PASSED" : "FAILED")}");
        Console.WriteLine($"Checksum visibility ON test: {(testShowPassed ? "PASSED" : "FAILED")}");

        // ------------------------------------------------------------
        // Cleanup temporary files and directory
        // ------------------------------------------------------------
        try { File.Delete(falsePath); } catch { }
        try { File.Delete(truePath); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}