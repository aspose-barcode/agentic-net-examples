// Title: AutoSizeMode.Nearest Barcode Image Dimension Verification
// Description: Demonstrates generating DataMatrix barcodes with AutoSizeMode.Nearest and validates the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to configure AutoSizeMode, image size, and X‑Dimension for DataMatrix barcodes. It uses BarcodeGenerator, GenerationParameters, and Aspose.Drawing to create PNG images, a common task for developers needing precise barcode sizing in automated tests or batch processing.
// Prompt: Write unit tests that compare expected and actual image dimensions after applying AutoSizeMode.Nearest with given parameters.
// Tags: datamatrix, autosizemode, nearest, image-dimensions, barcode-generation, aspose.barcode, png, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains a simple console‑based test harness that generates DataMatrix barcodes
/// with AutoSizeMode.Nearest and verifies the output image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Represents a single test case with input parameters and expected output dimensions.
    /// </summary>
    class TestCase
    {
        public string CodeText { get; set; }
        public int ImageWidthPixels { get; set; }
        public int ImageHeightPixels { get; set; }
        public int XDimensionPixels { get; set; }
        public int ExpectedWidth { get; set; }
        public int ExpectedHeight { get; set; }
    }

    /// <summary>
    /// Entry point. Generates barcodes for predefined test cases, saves them,
    /// reads back the image size, and reports pass/fail results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "AutoSizeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define test cases with expected dimensions.
        var tests = new[]
        {
            new TestCase
            {
                CodeText = "Test1",
                ImageWidthPixels = 300,
                ImageHeightPixels = 300,
                XDimensionPixels = 3,
                ExpectedWidth = 300,
                ExpectedHeight = 300
            },
            new TestCase
            {
                CodeText = "Sample2",
                ImageWidthPixels = 200,
                ImageHeightPixels = 100,
                XDimensionPixels = 2,
                ExpectedWidth = 200,
                ExpectedHeight = 100
            }
        };

        int passed = 0;
        int failed = 0;

        // Iterate over each test case, generate the barcode, and verify dimensions.
        foreach (var test in tests)
        {
            // Build a unique file path for the generated PNG image.
            string filePath = Path.Combine(tempDir, $"barcode_{Guid.NewGuid().ToString("N")}.png");

            // Configure the barcode generator with AutoSizeMode.Nearest and other parameters.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, test.CodeText))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.ImageWidth.Pixels = test.ImageWidthPixels;
                generator.Parameters.ImageHeight.Pixels = test.ImageHeightPixels;
                generator.Parameters.Barcode.XDimension.Pixels = test.XDimensionPixels;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Load the saved image to obtain actual dimensions.
            int actualWidth;
            int actualHeight;
            using (var bitmap = new Bitmap(filePath))
            {
                actualWidth = bitmap.Width;
                actualHeight = bitmap.Height;
            }

            // Compare actual dimensions with expected values.
            bool widthMatch = actualWidth == test.ExpectedWidth;
            bool heightMatch = actualHeight == test.ExpectedHeight;

            if (widthMatch && heightMatch)
            {
                Console.WriteLine($"PASS: {test.CodeText} => Width={actualWidth}, Height={actualHeight}");
                passed++;
            }
            else
            {
                Console.WriteLine($"FAIL: {test.CodeText} => Expected ({test.ExpectedWidth}x{test.ExpectedHeight}), Got ({actualWidth}x{actualHeight})");
                failed++;
            }
        }

        // Output a summary of test results.
        Console.WriteLine($"Test Summary: {passed} passed, {failed} failed.");
    }
}