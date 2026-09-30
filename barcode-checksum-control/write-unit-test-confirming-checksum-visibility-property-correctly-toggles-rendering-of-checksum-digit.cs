// Title: Checksum Visibility Toggle Test for Code39 Barcodes
// Description: Demonstrates how to generate a Code39 barcode with the checksum digit either shown or hidden in the human‑readable text and verifies the result.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to enable checksum calculation, control the ChecksumAlwaysShow property, and then read the barcode with BarCodeReader. Developers working with barcode validation, quality assurance, or unit testing often need to confirm that checksum visibility settings affect the rendered text as expected.
// Prompt: Write a unit test confirming the checksum visibility property correctly toggles rendering of the checksum digit.
// Tags: code39, checksum, visibility, barcode generation, barcode recognition, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains a simple console‑based test that verifies the effect of the
/// <c>ChecksumAlwaysShow</c> property on the rendered human‑readable text of a
/// Code39 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes with checksum visible
    /// and hidden, reads them back, and reports pass/fail results.
    /// </summary>
    static void Main()
    {
        // Test data and expected outcome when checksum is visible
        const string data = "12345";
        const string expectedWithChecksum = "12345F"; // Checksum for Code39 "12345" is 'F'

        // Generate barcode with checksum visible and read the decoded text
        string resultVisible = GenerateAndRead(data, showChecksum: true);
        // Generate barcode with checksum hidden and read the decoded text
        string resultHidden = GenerateAndRead(data, showChecksum: false);

        // Track overall test status
        bool passed = true;

        // Validate visible‑checksum case
        if (resultVisible != expectedWithChecksum)
        {
            Console.WriteLine($"FAILED: Expected visible checksum '{expectedWithChecksum}', got '{resultVisible}'.");
            passed = false;
        }
        else
        {
            Console.WriteLine("PASSED: Checksum visible correctly rendered.");
        }

        // Validate hidden‑checksum case
        if (resultHidden != data)
        {
            Console.WriteLine($"FAILED: Expected hidden checksum '{data}', got '{resultHidden}'.");
            passed = false;
        }
        else
        {
            Console.WriteLine("PASSED: Checksum hidden correctly rendered.");
        }

        // Summarize test results
        if (passed)
        {
            Console.WriteLine("All checksum visibility tests passed.");
        }
    }

    /// <summary>
    /// Generates a Code39 barcode for the supplied text, optionally shows the
    /// checksum digit, saves it to a memory stream, and then reads the barcode
    /// back to obtain the decoded <c>CodeText</c>.
    /// </summary>
    /// <param name="codeText">The data to encode.</param>
    /// <param name="showChecksum">If true, the checksum digit is included in the human‑readable text.</param>
    /// <returns>The decoded text from the generated barcode, or an empty string if reading fails.</returns>
    private static string GenerateAndRead(string codeText, bool showChecksum)
    {
        // Create a barcode generator for Code39 with the provided data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Enable checksum calculation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Control whether the checksum digit appears in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = showChecksum;

            // Save the generated barcode image to a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader to decode the image from the stream
                using (var reader = new BarCodeReader(ms, DecodeType.Code39))
                {
                    // Return the first decoded result's text
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        return result.CodeText;
                    }
                }
            }
        }

        // Return empty string if no barcode was successfully read
        return string.Empty;
    }
}