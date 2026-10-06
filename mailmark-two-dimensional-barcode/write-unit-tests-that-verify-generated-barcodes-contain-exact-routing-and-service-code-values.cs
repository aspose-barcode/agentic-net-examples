// Title: Generate and Verify Swiss Post Parcel Barcodes
// Description: Demonstrates creating Swiss Post parcel barcodes containing routing and service codes, saving them as PNG images, and reading them back to ensure the encoded text matches the expected values.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for SwissPostParcel encoding and BarCodeReader for decoding. Developers often need to generate postal barcodes for mailing workflows and validate them programmatically; this snippet illustrates typical API classes and patterns for such scenarios.
// Prompt: Write unit tests that verify generated barcodes contain the exact routing and service code values.
// Tags: swisspost, barcode, generation, recognition, png, unit-test, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and verification of Swiss Post parcel barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for defined test cases, saves them, and validates the encoded text.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases: a description and the expected barcode text (routing + service code)
        var testCases = new List<(string Description, string CodeText)>
        {
            ("International Mail (routing and service code)", "RM999605013CH"),
            ("Additional Service Code", "0327")
        };

        int passed = 0;
        int failed = 0;

        // Execute each test case and track results
        foreach (var (description, codeText) in testCases)
        {
            string imagePath = Path.Combine(tempFolder, $"{Guid.NewGuid()}.png");
            bool testResult = RunSingleTest(codeText, imagePath);
            if (testResult)
            {
                Console.WriteLine($"PASS: {description}");
                passed++;
            }
            else
            {
                Console.WriteLine($"FAIL: {description}");
                failed++;
            }
        }

        // Output a summary of the test run
        Console.WriteLine($"Test Summary: {passed} passed, {failed} failed.");
    }

    /// <summary>
    /// Generates a Swiss Post parcel barcode, saves it, reads it back, and verifies the encoded text.
    /// </summary>
    /// <param name="codeText">The exact text to encode in the barcode.</param>
    /// <param name="imagePath">File path where the generated PNG image will be saved.</param>
    /// <returns>True if the read text matches the original; otherwise, false.</returns>
    static bool RunSingleTest(string codeText, string imagePath)
    {
        // ---------- Barcode Generation ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Configure visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the barcode image to the specified path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ---------- Barcode Recognition ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Generate an in-memory bitmap of the barcode
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Set the decode type to SwissPostParcel
                BaseDecodeType decodeType = DecodeType.SwissPostParcel;

                // Read the barcode from the bitmap
                using (var reader = new BarCodeReader(bitmap, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Ensure at least one barcode was detected
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected for code text '{codeText}'.");
                        return false;
                    }

                    // Compare the decoded text with the original input
                    string readText = results[0].CodeText;
                    return string.Equals(readText, codeText, StringComparison.Ordinal);
                }
            }
        }
    }
}