// Title: Code128 Barcode Generation with Checksum Verification
// Description: Demonstrates generating a Code 128 barcode, displaying its checksum character, and verifying the checksum via decoding.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator (EncodeTypes.Code128) to create a barcode, enable ChecksumAlwaysShow, and then use BarCodeReader (DecodeType.Code128) to read the barcode back. Developers often need to validate checksum characters when implementing unit tests or data integrity checks for barcode workflows.
// Prompt: Design a unit test that verifies the generated barcode string ends with the correct checksum character for Code 128.
// Tags: code128, checksum, barcode, generation, recognition, unit-test, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code 128 barcode, forces the checksum character to be displayed,
/// decodes the barcode, and verifies that the decoded text ends with the correct checksum character.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary files, generates the barcode, decodes it, and checks the checksum.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "Code128ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the output path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // Data to encode (without checksum)
        string data = "Aspose1234";

        // Compute the expected checksum character for Code128 (using Code Set B)
        char expectedChecksumChar = ComputeCode128ChecksumChar(data);
        string expectedFullText = data + expectedChecksumChar;

        // Generate the barcode image with the checksum character always shown
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, data))
        {
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Decode the generated barcode to retrieve the full text (including checksum)
        string decodedText = null;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                decodedText = result.CodeText;
                break; // Only need the first result
            }
        }

        // Verify that the decoded text ends with the expected checksum character
        bool passed = false;
        if (decodedText != null && decodedText.Length == expectedFullText.Length)
        {
            if (decodedText[decodedText.Length - 1] == expectedChecksumChar)
                passed = true;
        }

        // Output the verification result
        Console.WriteLine(passed ? "PASSED: Checksum character matches." : "FAILED: Checksum character does not match.");

        // Cleanup temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }

    /// <summary>
    /// Computes the Code 128 checksum character for the given data using Code Set B.
    /// </summary>
    /// <param name="data">The data string to encode (without checksum).</param>
    /// <returns>The checksum character that should appear at the end of the encoded barcode.</returns>
    static char ComputeCode128ChecksumChar(string data)
    {
        // Start Code B value (104) as per Code 128 specification
        int startCode = 104;
        int sum = startCode;

        // Calculate weighted sum of character values
        for (int i = 0; i < data.Length; i++)
        {
            int charValue = data[i] - 32; // Code Set B mapping (ASCII 32‑127)
            sum += charValue * (i + 1);
        }

        // Modulo 103 yields the checksum value
        int checksum = sum % 103;

        // Map checksum value back to a printable character (if within printable range)
        if (checksum < 96)
            return (char)(checksum + 32);

        // For function codes (96‑102) return a placeholder character
        return '?';
    }
}