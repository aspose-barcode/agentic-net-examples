// Title: Verify MaxiCode Mode 2 Codetext Generation with Aspose.BarCode
// Description: Demonstrates how to generate a MaxiCode Mode 2 barcode, decode it, and assert that the codetext matches the expected formatted string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and BarCodeReader to create, save, and validate barcodes, a common task for developers implementing shipping or logistics solutions that require precise MaxiCode data encoding.
// Prompt: Create a unit test that verifies the generated MaxiCode Mode 2 codetext matches the expected formatted string.
// Tags: barcode, maxicode, mode2, unit-test, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains the entry point that generates a MaxiCode Mode 2 barcode,
/// decodes it, and validates the codetext against the expected value.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a temporary MaxiCode image, reads it back, and checks that
    /// the decoded codetext and mode are as expected.
    /// </summary>
    static void Main()
    {
        // Prepare test data for MaxiCode Mode 2
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit postal code
            CountryCode = 56,           // 3‑digit country code (leading zeros are optional)
            ServiceCategory = 999       // 3‑digit service category
        };

        // Optional: add a standard second message
        var secondMessage = new MaxiCodeStandardSecondMessage { Message = "Test message" };
        maxiCodeData.SecondMessage = secondMessage;

        // Expected codetext constructed by the complex barcode object itself
        string expectedCodetext = maxiCodeData.GetConstructedCodetext();

        // Generate the barcode image to a temporary file
        string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Enable strict validation of codetext
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
            generator.GenerateBarCodeImage();
            generator.Save(tempImagePath);
        }

        // Read and decode the generated barcode
        bool testPassed = false;
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.MaxiCode))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                // Verify that a codetext was decoded
                if (string.IsNullOrEmpty(result.CodeText))
                {
                    Console.WriteLine("FAILED: Decoded CodeText is null or empty.");
                    break;
                }

                // Verify that the decoded codetext matches the expected value
                if (!result.CodeText.Equals(expectedCodetext, StringComparison.Ordinal))
                {
                    Console.WriteLine($"FAILED: Expected CodeText '{expectedCodetext}' but got '{result.CodeText}'.");
                    break;
                }

                // Verify that the decoded mode is Mode2
                if (result.Extended.MaxiCode.Mode != MaxiCodeMode.Mode2)
                {
                    Console.WriteLine($"FAILED: Expected MaxiCode mode 'Mode2' but got '{result.Extended.MaxiCode.Mode}'.");
                    break;
                }

                // All checks passed
                testPassed = true;
                break; // only need first barcode
            }
        }

        // Clean up temporary file
        if (File.Exists(tempImagePath))
        {
            try { File.Delete(tempImagePath); } catch { /* ignore cleanup errors */ }
        }

        // Report result
        if (testPassed)
        {
            Console.WriteLine("PASSED: MaxiCode Mode 2 codetext matches expected formatted string.");
        }
        else
        {
            Console.WriteLine("FAILED: One or more verification steps did not succeed.");
        }
    }
}