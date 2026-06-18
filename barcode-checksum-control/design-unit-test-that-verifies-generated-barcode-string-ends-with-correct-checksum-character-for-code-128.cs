using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, reading its checksum, and verifying it against a computed value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads its checksum, compares it with an expected value, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Sample data for Code128 (Code Set B)
        const string data = "123456";

        // Temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "code128_test.png");

        // -------------------------------------------------
        // Generate the barcode image and save it to disk
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, data))
        {
            // Enable checksum generation (required for Code128)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the barcode image to the temporary file
            generator.Save(tempPath);
        }

        // -------------------------------------------------
        // Read the barcode back from the image and obtain the checksum character
        // -------------------------------------------------
        string recognizedChecksum;
        using (var reader = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            // Read the first (and only) barcode found in the image
            var result = reader.ReadBarCodes().FirstOrDefault();

            // If no barcode was read, report the failure and exit
            if (result == null)
            {
                Console.WriteLine("Failed to read the barcode.");
                return;
            }

            // Extract the checksum character from the recognized result
            recognizedChecksum = result.Extended.OneD.CheckSum;
        }

        // -------------------------------------------------
        // Compute the expected checksum character using Code Set B algorithm
        // -------------------------------------------------
        string expectedChecksum = ComputeCode128Checksum(data);

        // -------------------------------------------------
        // Verify that the recognized checksum matches the expected one
        // -------------------------------------------------
        if (recognizedChecksum == expectedChecksum)
        {
            Console.WriteLine("Test passed: checksum character matches expected value.");
        }
        else
        {
            Console.WriteLine($"Test failed: expected '{expectedChecksum}', but got '{recognizedChecksum}'.");
        }

        // -------------------------------------------------
        // Clean up the temporary image file
        // -------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignored: if deletion fails, there's nothing critical to report
        }
    }

    /// <summary>
    /// Computes the Code128 checksum character for the given data using Code Set B.
    /// </summary>
    /// <param name="data">The string data to encode (Code Set B characters).</param>
    /// <returns>The checksum character as a string.</returns>
    private static string ComputeCode128Checksum(string data)
    {
        // Start Code B value is 104
        int sum = 104;

        // Iterate over each character to calculate weighted sum
        for (int i = 0; i < data.Length; i++)
        {
            // Code Set B: character value = ASCII code - 32
            int charValue = data[i] - 32;

            // Position index starts at 1 (per specification)
            int position = i + 1;

            // Add weighted value to the running sum
            sum += charValue * position;
        }

        // Modulo 103 yields the checksum value
        int checksumValue = sum % 103;

        // Map checksum value back to a printable character in Code Set B
        // Values 0-95 correspond to ASCII 32-127.
        // For values 96-102, special (non‑printable) characters are used;
        // this test only deals with the printable range.
        char checksumChar = (char)(checksumValue + 32);
        return checksumChar.ToString();
    }
}