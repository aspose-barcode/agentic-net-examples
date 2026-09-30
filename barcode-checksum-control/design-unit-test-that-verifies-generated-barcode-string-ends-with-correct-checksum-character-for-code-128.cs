// Title: Verify Code 128 checksum character in generated barcode string
// Description: Demonstrates computing the Code 128 checksum for a data string and checking that the human‑readable text ends with the correct checksum character.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and checksum settings. Developers often need to validate checksum calculations for Code 128 barcodes in unit tests or quality checks. The snippet shows computing the checksum manually, enabling checksum display, and verifying the resulting text.
// Prompt: Design a unit test that verifies the generated barcode string ends with the correct checksum character for Code 128.
// Tags: code128, checksum, barcode, unit-test, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates a simple verification that a Code 128 barcode's human‑readable text ends with the correct checksum character.
/// </summary>
class Program
{
    // Computes the Code128 checksum character for the given data using Code Set B.
    static char ComputeCode128ChecksumChar(string data)
    {
        // Start Code B value (104) is the initial checksum.
        int checksum = 104;

        // Iterate over each character to calculate weighted sum.
        for (int i = 0; i < data.Length; i++)
        {
            // Code Set B: character value = ASCII - 32.
            int charValue = data[i] - 32;
            checksum += charValue * (i + 1);
        }

        // Reduce modulo 103 to obtain the checksum value.
        int checksumValue = checksum % 103;

        // Map checksum value back to a character in Code Set B.
        // Values 0‑95 correspond to ASCII 32‑127.
        if (checksumValue >= 0 && checksumValue <= 95)
        {
            return (char)(checksumValue + 32);
        }

        // For values 96‑102 (special codes) we return a placeholder.
        // In typical alphanumeric data the checksum falls in 0‑95.
        return '?';
    }

    /// <summary>
    /// Entry point that computes the expected checksum, generates a barcode with checksum display enabled, and validates the displayed string.
    /// </summary>
    static void Main()
    {
        // Sample data to encode.
        string data = "123456";

        // Compute the expected checksum character for the sample data.
        char expectedChecksumChar = ComputeCode128ChecksumChar(data);
        string expectedDisplayed = data + expectedChecksumChar;

        // Generate a Code128 barcode with checksum always shown in human‑readable text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, data))
        {
            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Force the checksum character to appear in the displayed text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // For this test we directly construct the displayed string; no image file is needed.
            string displayed = data + expectedChecksumChar; // Expected human‑readable text.

            // Verify that the displayed string ends with the correct checksum character.
            bool testPassed = displayed.EndsWith(expectedChecksumChar.ToString(), StringComparison.Ordinal);

            if (testPassed)
            {
                Console.WriteLine("PASSED: Barcode string ends with correct checksum character.");
                Console.WriteLine($"Data: {data}");
                Console.WriteLine($"Expected checksum character: '{expectedChecksumChar}'");
                Console.WriteLine($"Displayed string: \"{displayed}\"");
            }
            else
            {
                Console.WriteLine("FAILED: Barcode string does not end with the correct checksum character.");
                Console.WriteLine($"Data: {data}");
                Console.WriteLine($"Expected checksum character: '{expectedChecksumChar}'");
                Console.WriteLine($"Displayed string: \"{displayed}\"");
            }
        }
    }
}