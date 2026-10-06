// Title: Checksum Validation Example for Aspose.BarCode
// Description: Demonstrates generating barcodes with checksum enabled or disabled and verifying the result using Aspose.BarCode APIs.
// Category-Description: This example belongs to the barcode generation and recognition category, focusing on checksum handling. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and the checksum validation settings available in the Aspose.BarCode library. Developers working with barcode symbologies often need to control checksum calculation and validation to meet specification requirements, making this a common scenario in automated testing and CI pipelines.
// Prompt: Design a CI pipeline step that runs all checksum‑related unit tests and fails the build on any exception.
// Tags: code39, code128, checksum, barcode generation, barcode recognition, aspose.barcode, png, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point and helper methods for running checksum validation tests
/// using Aspose.BarCode's generation and recognition APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Executes a series of barcode checksum tests and reports the overall result.
    /// </summary>
    static void Main()
    {
        int failures = 0;

        // Create a unique temporary directory for test output files.
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Test 1: Code39 with checksum enabled
        if (!RunTest(
            "Code39_Enabled",
            EncodeTypes.Code39,
            "ABC123",
            EnableChecksum.Yes,
            DecodeType.Code39,
            tempDir))
        {
            failures++;
        }

        // Test 2: Code39 with checksum disabled
        if (!RunTest(
            "Code39_Disabled",
            EncodeTypes.Code39,
            "XYZ789",
            EnableChecksum.No,
            DecodeType.Code39,
            tempDir))
        {
            failures++;
        }

        // Test 3: Code128 (checksum is mandatory and always enabled)
        if (!RunTest(
            "Code128_Default",
            EncodeTypes.Code128,
            "1234567890",
            EnableChecksum.Yes,
            DecodeType.Code128,
            tempDir))
        {
            failures++;
        }

        // Output the overall test result.
        Console.WriteLine(failures == 0
            ? "All checksum tests passed."
            : $"FAILED: {failures} test(s) failed.");
    }

    /// <summary>
    /// Generates a barcode, saves it to a PNG file, reads it back, and verifies that the decoded text matches the original.
    /// </summary>
    /// <param name="testName">Unique name for the test case (used for the output file).</param>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="checksumSetting">Whether to enable checksum calculation during generation.</param>
    /// <param name="decodeType">The symbology to use when decoding the barcode.</param>
    /// <param name="outputFolder">Folder where the generated PNG file will be saved.</param>
    /// <returns>True if the barcode was generated and decoded successfully; otherwise false.</returns>
    static bool RunTest(
        string testName,
        BaseEncodeType encodeType,
        string codeText,
        EnableChecksum checksumSetting,
        BaseDecodeType decodeType,
        string outputFolder)
    {
        // Build the full file path for the PNG image.
        string filePath = Path.Combine(outputFolder, testName + ".png");

        try
        {
            // ---------- Barcode Generation ----------
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply the requested checksum setting.
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // ---------- Barcode Recognition ----------
            using (BarCodeReader reader = new BarCodeReader(filePath, decodeType))
            {
                // Use the default checksum validation behavior.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                bool found = false;

                // Iterate through all detected barcodes in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result.CodeText == codeText)
                    {
                        found = true;
                        break;
                    }
                }

                // If the expected text was not found, report failure.
                if (!found)
                {
                    Console.WriteLine($"{testName}: Expected code text not found.");
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected exception and mark the test as failed.
            Console.WriteLine($"{testName}: Exception - {ex.Message}");
            return false;
        }

        // Test succeeded.
        return true;
    }
}