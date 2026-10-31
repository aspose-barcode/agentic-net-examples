// Title: Swiss Post Parcel Barcode Generation and Verification Example
// Description: Demonstrates generating Swiss Post Parcel barcodes, saving them as PNG, and reading them back to verify the routing and service code values.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator with EncodeTypes.SwissPostParcel and BarCodeReader with DecodeType.SwissPostParcel to create and validate barcodes. Developers working with postal services often need to generate barcodes containing routing and service codes and then confirm their correctness programmatically. The code illustrates typical steps: setting dimensions, saving images, reading back the code text, and performing simple assertions.
// Prompt: Write unit tests that verify generated barcodes contain the exact routing and service code values.
// Tags: barcode, swisspost, parcel, generation, recognition, png, aspose.barcode, unit-test

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Swiss Post Parcel barcodes, reads them back,
/// and verifies that the encoded routing and service codes match the expected values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a set of barcodes, decodes them,
    /// and reports pass/fail results based on exact code text matching.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test images
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases: each entry is the expected code text
        var testCases = new List<string>
        {
            "RM999605013CH", // valid international mail with correct checksum
            "RM999605017CH", // valid international mail with erroneous checksum (still generated)
            "RM99960501CH"   // without checksum
        };

        int passed = 0;
        int failed = 0;
        int index = 0;

        // Iterate over each test case, generate barcode, read it back, and verify
        foreach (var codeText in testCases)
        {
            index++;
            string filePath = Path.Combine(tempFolder, $"barcode_{index}.png");

            // Generate barcode image with specific dimensions
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Prepare to read the generated barcode
            BaseDecodeType decodeType = DecodeType.SwissPostParcel;
            string readText = null;
            bool readSuccess = false;

            // Decode the barcode from the saved image
            using (var reader = new BarCodeReader(filePath, decodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                {
                    readText = results[0].CodeText;
                    readSuccess = true;
                }
            }

            // Verify that the decoded text matches the original expected text
            if (readSuccess && readText == codeText)
            {
                Console.WriteLine($"Test {index}: PASS (CodeText = \"{codeText}\")");
                passed++;
            }
            else
            {
                Console.WriteLine($"Test {index}: FAIL (Expected \"{codeText}\", Got \"{readText ?? "null"}\")");
                failed++;
            }
        }

        // Output summary of test results
        Console.WriteLine($"Summary: {passed} passed, {failed} failed.");

        // Cleanup temporary files and folder
        try
        {
            foreach (var file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}