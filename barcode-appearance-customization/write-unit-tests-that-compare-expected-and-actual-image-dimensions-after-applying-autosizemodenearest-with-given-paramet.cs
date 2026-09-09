// Title: Demonstrate AutoSizeMode.Nearest barcode image dimension verification
// Description: Shows how to generate Code128 barcodes with specific image dimensions and X‑dimension, then verifies that the resulting bitmap matches expected width and height.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image size parameters. Developers often need to ensure that generated barcode images meet exact size requirements for downstream processing or UI layout, and this snippet demonstrates a simple test harness for that purpose.
// Prompt: Write unit tests that compare expected and actual image dimensions after applying AutoSizeMode.Nearest with given parameters.
// Tags: code128, autosizemode, nearest, image-dimensions, barcode-generation, aspose.barcode, testing

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains a simple test harness that generates barcodes and validates image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Represents a single test case with input parameters and expected output dimensions.
    /// </summary>
    class TestCase
    {
        public string CodeText { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public float XDimensionPixels { get; set; }
        public int ExpectedWidth { get; set; }
        public int ExpectedHeight { get; set; }
    }

    /// <summary>
    /// Executes a series of test cases, generating barcodes with specified parameters and checking that the output image size matches expectations.
    /// </summary>
    static void Main()
    {
        // Define a collection of test scenarios with expected dimensions.
        var tests = new List<TestCase>
        {
            new TestCase
            {
                CodeText = "12345",
                ImageWidth = 300,
                ImageHeight = 150,
                XDimensionPixels = 3f,
                ExpectedWidth = 300,
                ExpectedHeight = 150
            },
            new TestCase
            {
                CodeText = "ABCDE",
                ImageWidth = 400,
                ImageHeight = 200,
                XDimensionPixels = 2.5f,
                ExpectedWidth = 400,
                ExpectedHeight = 200
            },
            new TestCase
            {
                CodeText = "XYZ",
                ImageWidth = 250,
                ImageHeight = 250,
                XDimensionPixels = 4f,
                ExpectedWidth = 250,
                ExpectedHeight = 250
            }
        };

        int passed = 0;   // Counter for successful tests
        int failed = 0;   // Counter for failed tests
        int index = 1;    // Sequential test identifier

        // Iterate over each test case, generate the barcode, and verify dimensions.
        foreach (var test in tests)
        {
            try
            {
                // Initialize the barcode generator with Code128 symbology and the test's code text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, test.CodeText))
                {
                    // Apply AutoSizeMode.Nearest and set image size and X-dimension parameters.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                    generator.Parameters.ImageWidth.Pixels = test.ImageWidth;
                    generator.Parameters.ImageHeight.Pixels = test.ImageHeight;
                    generator.Parameters.Barcode.XDimension.Pixels = test.XDimensionPixels;

                    // Generate the barcode image.
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        int actualWidth = bitmap.Width;
                        int actualHeight = bitmap.Height;

                        // Compare actual dimensions with expected values.
                        bool ok = actualWidth == test.ExpectedWidth && actualHeight == test.ExpectedHeight;
                        if (ok) passed++; else failed++;

                        Console.WriteLine($"Test {index}: {(ok ? "PASS" : "FAIL")} - Expected ({test.ExpectedWidth}x{test.ExpectedHeight}), Actual ({actualWidth}x{actualHeight})");
                    }
                }
            }
            catch (Exception ex)
            {
                // Record any exceptions as failures.
                failed++;
                Console.WriteLine($"Test {index}: EXCEPTION - {ex.Message}");
            }

            index++;
        }

        // Output a summary of test results.
        Console.WriteLine($"Summary: {passed} passed, {failed} failed.");
    }
}