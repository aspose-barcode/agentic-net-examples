// Title: Checksum Visibility Toggle Example
// Description: Demonstrates how to generate a Code128 barcode with and without displaying the checksum digit and verifies the effect using barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to control checksum rendering via IsChecksumEnabled and ChecksumAlwaysShow, and BarCodeReader to decode the resulting images. Developers often need to validate that checksum visibility settings are applied correctly for compliance and readability in scanning systems.
// Prompt: Write a unit test confirming the checksum visibility property correctly toggles rendering of the checksum digit.
// Tags: code128, checksum, barcode generation, barcode recognition, aspose.barcode, unit test, image, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates Code128 barcodes with and without displaying the checksum digit,
/// then verifies that the checksum visibility setting affects the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, reads them back,
    /// and evaluates whether the checksum visibility property works as expected.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the two barcode images
        string pathWithout = Path.Combine(tempDir, "code128_no_checksum.png");
        string pathWith = Path.Combine(tempDir, "code128_with_checksum.png");

        // Generate a Code128 barcode with checksum enabled
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE"))
        {
            // Enable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save barcode without displaying the checksum digit
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;
            generator.Save(pathWithout, BarCodeImageFormat.Png);

            // Save barcode with the checksum digit displayed
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(pathWith, BarCodeImageFormat.Png);
        }

        // Decode the generated images to obtain the textual representation
        string codeTextWithout = ReadCodeText(pathWithout);
        string codeTextWith = ReadCodeText(pathWith);

        // Verify that the checksum digit is present only in the second image
        bool testPassed = !string.IsNullOrEmpty(codeTextWithout) &&
                          !string.IsNullOrEmpty(codeTextWith) &&
                          codeTextWithout.Length < codeTextWith.Length &&
                          codeTextWith.StartsWith(codeTextWithout, StringComparison.Ordinal);

        // Output the test result and the decoded texts
        Console.WriteLine(testPassed ? "Test Passed" : "Test Failed");
        Console.WriteLine($"Without checksum: {codeTextWithout}");
        Console.WriteLine($"With checksum:    {codeTextWith}");

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }

    /// <summary>
    /// Reads the first decoded barcode text from the specified image file.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <returns>The decoded barcode text, or an empty string if decoding fails.</returns>
    static string ReadCodeText(string imagePath)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return string.Empty;
        }

        // Use BarCodeReader to decode the barcode from the image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                return result.CodeText ?? string.Empty;
            }
        }

        // Return empty string if no barcode was found
        return string.Empty;
    }
}