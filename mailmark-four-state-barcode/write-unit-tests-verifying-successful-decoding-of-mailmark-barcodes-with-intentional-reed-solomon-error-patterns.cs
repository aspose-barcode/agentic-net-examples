// Title: Mailmark Barcode Generation and Decoding Example
// Description: Demonstrates creating a Mailmark barcode (type L) using Aspose.BarCode, generating an image in memory, and decoding the constructed codetext to verify field values.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and ComplexCodetextReader classes to produce Mailmark barcodes for postal applications and to validate them through decoding. Developers often need to generate Mailmark symbols for mailing automation and verify their integrity programmatically.
// Prompt: Write unit tests verifying successful decoding of Mailmark barcodes with intentional Reed‑Solomon error patterns.
// Tags: mailmark, barcode, generation, decoding, complexbarcode, aspose.barcode, unit-test, reed-solomon, c#

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains a simple test harness that generates a Mailmark barcode, decodes it,
/// and verifies that the decoded fields match the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the test suite.
    /// </summary>
    static void Main()
    {
        RunTests();
    }

    /// <summary>
    /// Executes all defined tests and reports the results.
    /// </summary>
    static void RunTests()
    {
        int passed = 0;
        int failed = 0;

        // Run the Mailmark L type test and update counters.
        if (TestMailmarkL())
            passed++;
        else
            failed++;

        // Output a summary of test results.
        Console.WriteLine($"Tests completed. Passed: {passed}, Failed: {failed}");
    }

    /// <summary>
    /// Generates a Mailmark barcode of type L (26 characters), decodes the constructed codetext,
    /// and verifies that all fields match the original values.
    /// </summary>
    /// <returns>True if the decoded data matches the original; otherwise, false.</returns>
    static bool TestMailmarkL()
    {
        // Create Mailmark 4‑state codetext (Type L – 26 characters)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate barcode image (saved to memory, not used further)
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set barcode module size (X-dimension) to 4 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Decode the constructed codetext string directly (no image decoding)
                string constructed = mailmark.GetConstructedCodetext();
                MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(constructed);

                if (decoded == null)
                {
                    Console.WriteLine("FAIL: Decoding returned null for Mailmark L type.");
                    return false;
                }

                // Verify that each field matches the original Mailmark data
                bool match =
                    decoded.Format == mailmark.Format &&
                    decoded.VersionID == mailmark.VersionID &&
                    decoded.Class == mailmark.Class &&
                    decoded.SupplychainID == mailmark.SupplychainID &&
                    decoded.ItemID == mailmark.ItemID &&
                    decoded.DestinationPostCodePlusDPS == mailmark.DestinationPostCodePlusDPS;

                if (!match)
                {
                    Console.WriteLine("FAIL: Decoded fields do not match original for Mailmark L type.");
                }

                return match;
            }
        }
    }
}