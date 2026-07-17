// Title: Barcode Generation and Validation Test Suite
// Description: Generates various barcode types, saves them as PNG files, and validates them by decoding the images.
// Category-Description: This example demonstrates Aspose.BarCode's generation and recognition APIs, covering classes such as BarcodeGenerator, BarCodeReader, and related parameter objects. It shows typical use cases like creating barcodes for different symbologies, customizing visual settings, and programmatically verifying output across .NET Framework, .NET Core, and .NET 6 runtimes. Developers often need such end‑to‑end tests to ensure consistent barcode quality in CI pipelines.
// Prompt: Create a test suite that validates barcode generation across .NET Framework, .NET Core, and .NET 6 runtimes.
// Tags: barcode symbology, generation, recognition, png, aspose.barcode, .net framework, .net core, .net 6

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple test harness that generates a set of barcodes,
/// saves them as PNG files, and validates each by decoding the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Simple data holder for each test case, containing the symbology,
    /// the text to encode, and the output file name.
    /// </summary>
    private class TestCase
    {
        public BaseEncodeType EncodeType { get; set; }
        public string CodeText { get; set; }
        public string FileName { get; set; }
    }

    /// <summary>
    /// Entry point of the test suite. Generates barcodes, saves them,
    /// and verifies that the decoded text matches the original input.
    /// </summary>
    static void Main()
    {
        // Define the collection of barcode test scenarios.
        var tests = new List<TestCase>
        {
            new TestCase { EncodeType = EncodeTypes.Code128, CodeText = "123ABC", FileName = "code128.png" },
            new TestCase { EncodeType = EncodeTypes.QR, CodeText = "https://example.com", FileName = "qr.png" },
            new TestCase { EncodeType = EncodeTypes.DataMatrix, CodeText = "DM12345", FileName = "datamatrix.png" },
            new TestCase { EncodeType = EncodeTypes.Aztec, CodeText = "AZTEC", FileName = "aztec.png" },
            new TestCase { EncodeType = EncodeTypes.ITF14, CodeText = "12345678901231", FileName = "itf14.png" },
            new TestCase { EncodeType = EncodeTypes.AustraliaPost, CodeText = "1100000000", FileName = "auspost.png" }
        };

        int passed = 0;
        int failed = 0;

        // Iterate through each test case, generating and validating the barcode.
        foreach (var test in tests)
        {
            try
            {
                // Determine the full path for the output image.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), test.FileName);

                // Generate the barcode with common visual settings.
                using (var generator = new BarcodeGenerator(test.EncodeType, test.CodeText))
                {
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;
                    generator.Parameters.Barcode.FilledBars = false;
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                    generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Apply special configuration for AustraliaPost symbology.
                    if (test.EncodeType == EncodeTypes.AustraliaPost)
                    {
                        generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                    }

                    // Save the generated barcode as a PNG file.
                    generator.Save(outputPath);
                }

                // Verify that the image file was successfully created.
                if (!File.Exists(outputPath))
                {
                    Console.WriteLine($"[ERROR] File not created: {outputPath}");
                    failed++;
                    continue;
                }

                // Decode the saved barcode image and compare the result with the original text.
                BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;
                using (var reader = new BarCodeReader(outputPath, decodeAll))
                {
                    bool matchFound = false;
                    foreach (var result in reader.ReadBarCodes())
                    {
                        if (result.CodeText == test.CodeText)
                        {
                            matchFound = true;
                            break;
                        }
                    }

                    if (matchFound)
                    {
                        Console.WriteLine($"[PASS] {test.FileName} - decoded correctly.");
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine($"[FAIL] {test.FileName} - decoded text does not match.");
                        failed++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION] {test.FileName} - {ex.Message}");
                failed++;
            }
        }

        // Output a summary of the test results.
        Console.WriteLine();
        Console.WriteLine($"Test summary: Passed = {passed}, Failed = {failed}");
    }
}