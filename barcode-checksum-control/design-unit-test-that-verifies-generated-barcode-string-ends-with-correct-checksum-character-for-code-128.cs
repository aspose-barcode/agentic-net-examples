// Title: Code128 Barcode Generation and Checksum Verification
// Description: Generates a Code 128 barcode, reads it back, and verifies that the decoded text ends with the correct checksum character.
// Prompt: Design a unit test that verifies the generated barcode string ends with the correct checksum character for Code 128.
// Tags: barcode, code128, checksum, unit-test, aspose, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a Code 128 barcode, read it back, and validate that the decoded
/// string ends with the correct checksum character. This serves as a simple unit‑test example.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it, and checks the checksum.
    /// </summary>
    static void Main()
    {
        // Sample data without checksum; Aspose will calculate it automatically.
        const string data = "ABC123";

        // Prepare a memory stream to hold the generated barcode image.
        using (var imageStream = new MemoryStream())
        {
            // Create a Code128 barcode generator with the sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, data))
            {
                // Enable checksum generation (default for Code128) and show it in the human‑readable text.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the barcode image to the memory stream in PNG format.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading the image.
            imageStream.Position = 0;

            int failures = 0;
            int totalTests = 0;

            // Read the barcode back from the image using a Code128 decoder.
            using (var reader = new BarCodeReader(imageStream, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    totalTests++;

                    // Full decoded text (including checksum if displayed) and the separate checksum value.
                    string fullCodeText = result.CodeText;
                    string checksum = result.Extended.OneD.CheckSum;

                    // Verify that the full code text ends with the checksum character.
                    if (!fullCodeText.EndsWith(checksum))
                    {
                        Console.WriteLine($"FAILED: Expected checksum '{checksum}' at the end of '{fullCodeText}'.");
                        failures++;
                    }
                    else
                    {
                        Console.WriteLine($"PASSED: CodeText '{fullCodeText}' ends with checksum '{checksum}'.");
                    }
                }
            }

            // Output a summary of the test results.
            if (failures == 0 && totalTests > 0)
            {
                Console.WriteLine("All tests passed.");
            }
            else if (totalTests == 0)
            {
                Console.WriteLine("No barcodes were read; test could not be performed.");
            }
            else
            {
                Console.WriteLine($"FAILED: {failures} out of {totalTests} tests failed.");
            }
        }
    }
}