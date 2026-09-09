// Title: Generate and Verify Swiss Post Parcel and Code128 Barcodes
// Description: Demonstrates creating barcodes for a Swiss Post routing code and a generic service code, then reading them back to confirm the encoded values.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator and BarCodeReader classes. Typical use cases include automated testing of barcode output, validation of routing and service codes, and integration testing for shipping and logistics applications. Developers often need quick unit‑style checks that the produced barcodes contain exact data strings.
// Prompt: Write unit tests that verify generated barcodes contain the exact routing and service code values.
// Tags: barcode, generation, recognition, swisspostparcel, code128, aspose.barcode, csharp, unit-test

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and verification for routing and service codes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs two barcode verification tests and reports the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Collect test outcomes
        var tests = new List<bool>();

        // Test 1: Swiss Post Parcel (routing code)
        string routingCode = "RM999605013CH";
        string routingFile = Path.Combine(tempFolder, "routing.png");
        tests.Add(TestBarcode(routingCode, EncodeTypes.SwissPostParcel, DecodeType.SwissPostParcel, routingFile));

        // Test 2: Service Code (additional service)
        string serviceCode = "0327";
        string serviceFile = Path.Combine(tempFolder, "service.png");
        tests.Add(TestBarcode(serviceCode, EncodeTypes.Code128, DecodeType.Code128, serviceFile));

        // Summarize results
        int passed = 0;
        int failed = 0;
        for (int i = 0; i < tests.Count; i++)
        {
            if (tests[i])
                passed++;
            else
                failed++;
        }

        Console.WriteLine($"Test Summary: {passed} passed, {failed} failed.");

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates a barcode, saves it to a file, reads it back, and verifies that the decoded text starts with the expected value.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use for encoding.</param>
    /// <param name="decodeType">The barcode symbology to use for decoding.</param>
    /// <param name="filePath">The full path where the barcode image will be saved.</param>
    /// <returns>True if the decoded text matches the expected start; otherwise, false.</returns>
    static bool TestBarcode(string codeText, BaseEncodeType encodeType, BaseDecodeType decodeType, string filePath)
    {
        // Generate barcode image
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Read and decode the generated barcode
        using (var reader = new BarCodeReader(filePath, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine($"FAIL: No barcode detected in {Path.GetFileName(filePath)}.");
                return false;
            }

            string readText = results[0].CodeText ?? string.Empty;
            if (readText.StartsWith(codeText, StringComparison.Ordinal))
            {
                Console.WriteLine($"PASS: {Path.GetFileName(filePath)} decoded correctly.");
                return true;
            }
            else
            {
                Console.WriteLine($"FAIL: {Path.GetFileName(filePath)} decoded text does not start with expected value.");
                Console.WriteLine($"  Expected start: {codeText}");
                Console.WriteLine($"  Actual text   : {readText}");
                return false;
            }
        }
    }
}