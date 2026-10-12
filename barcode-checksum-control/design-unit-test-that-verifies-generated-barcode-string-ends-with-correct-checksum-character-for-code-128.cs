// Title: Code128 Barcode Generation with Checksum Verification
// Description: Demonstrates creating a Code 128 barcode that always shows its checksum, reading it back, and confirming the checksum character matches the expected value.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to produce a barcode, display the checksum, decode the image, and validate the result—common tasks for developers implementing automated barcode validation or unit testing in .NET applications.
// Prompt: Design a unit test that verifies the generated barcode string ends with the correct checksum character for Code 128.
// Tags: code128, checksum, barcode, generation, recognition, unit-test, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with an always‑displayed checksum,
/// reads the barcode back, and verifies the checksum character matches the expected value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, decoding, and checksum validation.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode image.
        // ----------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "Code128ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "code128.png");

        // ----------------------------------------------------------------------
        // Input data (without checksum) that will be encoded.
        // ----------------------------------------------------------------------
        string data = "A";

        // ----------------------------------------------------------------------
        // Generate a Code128 barcode and force the checksum character to be shown.
        // ----------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, data))
        {
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ----------------------------------------------------------------------
        // Decode the barcode image to retrieve the full CodeText (data + checksum).
        // ----------------------------------------------------------------------
        string readCodeText = null;
        BaseDecodeType decodeType = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                readCodeText = result.CodeText;
                break; // Only need the first result.
            }
        }

        // ----------------------------------------------------------------------
        // Verify that the checksum character matches the expected value.
        // ----------------------------------------------------------------------
        bool testPassed = false;
        if (!string.IsNullOrEmpty(readCodeText) && readCodeText.Length == data.Length + 1)
        {
            char expectedChecksumChar = ComputeCode128ChecksumChar(data);
            char actualChecksumChar = readCodeText[readCodeText.Length - 1];
            testPassed = expectedChecksumChar == actualChecksumChar;

            Console.WriteLine($"Data: {data}");
            Console.WriteLine($"Read CodeText (with checksum): {readCodeText}");
            Console.WriteLine($"Expected checksum char: {expectedChecksumChar}");
            Console.WriteLine($"Actual checksum char:   {actualChecksumChar}");
        }
        else
        {
            Console.WriteLine("Failed to read barcode or unexpected CodeText length.");
        }

        Console.WriteLine(testPassed ? "TEST PASSED: Checksum character is correct." : "TEST FAILED: Checksum character is incorrect.");

        // ----------------------------------------------------------------------
        // Cleanup temporary files and directories.
        // ----------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }

    /// <summary>
    /// Computes the checksum character for a Code128 barcode using Code Set B (default).
    /// </summary>
    /// <param name="data">The data string to encode (without checksum).</param>
    /// <returns>The checksum character that should appear at the end of the encoded string.</returns>
    static char ComputeCode128ChecksumChar(string data)
    {
        const int startCodeB = 104;
        int checksum = startCodeB;

        // Calculate weighted sum of character values.
        for (int i = 0; i < data.Length; i++)
        {
            int charValue = data[i] - 32; // Code Set B mapping.
            checksum += charValue * (i + 1);
        }

        int checkValue = checksum % 103;

        // Map check value back to a character for Code Set B (0‑94).
        if (checkValue >= 0 && checkValue <= 94)
        {
            return (char)(checkValue + 32);
        }

        // For values 95‑102 (FNC codes), return a placeholder.
        return '?';
    }
}