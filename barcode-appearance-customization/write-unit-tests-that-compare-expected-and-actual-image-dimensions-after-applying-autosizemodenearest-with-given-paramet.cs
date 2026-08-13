// Title: AutoSizeMode.Nearest Barcode Image Dimension Test
// Description: Demonstrates how to generate a barcode image with AutoSizeMode.Nearest and verifies that the resulting image dimensions stay within the specified target size while preserving aspect ratio.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control barcode image sizing. Developers often need to generate barcodes that fit within predefined dimensions for UI layouts, reports, or printing, and this snippet shows how to test that the AutoSizeMode.Nearest setting produces expected dimensions.
// Prompt: Write unit tests that compare expected and actual image dimensions after applying AutoSizeMode.Nearest with given parameters.
// Tags: barcode symbology, autosize, image generation, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Contains a simple test harness that generates Code128 barcodes using AutoSizeMode.Nearest
/// and validates that the produced image dimensions respect the target size and aspect ratio.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes a series of dimension validation tests.
    /// </summary>
    static void Main()
    {
        int totalTests = 0;
        int failedTests = 0;

        // Test 1: Target size 200x100, Code128 barcode
        RunTest(
            testName: "Test1",
            expectedWidth: 200,
            expectedHeight: 100,
            targetWidth: 200,
            targetHeight: 100,
            ref totalTests,
            ref failedTests);

        // Test 2: Target size 300x150, Code128 barcode
        RunTest(
            testName: "Test2",
            expectedWidth: 300,
            expectedHeight: 150,
            targetWidth: 300,
            targetHeight: 150,
            ref totalTests,
            ref failedTests);

        // Test 3: Target size 250x250 (square), Code128 barcode
        RunTest(
            testName: "Test3",
            expectedWidth: 250,
            expectedHeight: 250,
            targetWidth: 250,
            targetHeight: 250,
            ref totalTests,
            ref failedTests);

        // Summary of test results
        Console.WriteLine($"TOTAL: {totalTests} tests, FAILED: {failedTests} tests.");
    }

    /// <summary>
    /// Generates a barcode image with the specified target dimensions and checks that the actual
    /// image size does not exceed the target while preserving the aspect ratio.
    /// </summary>
    /// <param name="testName">Identifier for the test case.</param>
    /// <param name="expectedWidth">Expected maximum width (not used directly, kept for compatibility).</param>
    /// <param name="expectedHeight">Expected maximum height (not used directly, kept for compatibility).</param>
    /// <param name="targetWidth">Desired width for the generated image.</param>
    /// <param name="targetHeight">Desired height for the generated image.</param>
    /// <param name="totalTests">Reference to the total test counter.</param>
    /// <param name="failedTests">Reference to the failed test counter.</param>
    static void RunTest(
        string testName,
        int expectedWidth,
        int expectedHeight,
        int targetWidth,
        int targetHeight,
        ref int totalTests,
        ref int failedTests)
    {
        totalTests++;

        // Initialize a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure AutoSizeMode to Nearest and set the target dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Point = (float)targetWidth;
            generator.Parameters.ImageHeight.Point = (float)targetHeight;

            // Generate the barcode image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Validate that the actual dimensions are within the target bounds.
                bool sizeOk = actualWidth <= targetWidth && actualHeight <= targetHeight;

                // Validate that the aspect ratio is preserved within a small tolerance.
                bool aspectOk = Math.Abs((float)actualWidth / actualHeight - (float)targetWidth / targetHeight) < 0.01f;

                if (sizeOk && aspectOk)
                {
                    Console.WriteLine($"{testName}: PASS (Actual: {actualWidth}x{actualHeight})");
                }
                else
                {
                    failedTests++;
                    Console.WriteLine($"{testName}: FAIL");
                    Console.WriteLine($"  Expected max size: {targetWidth}x{targetHeight}");
                    Console.WriteLine($"  Actual size: {actualWidth}x{actualHeight}");
                    Console.WriteLine($"  Size OK: {sizeOk}, Aspect OK: {aspectOk}");
                }
            }
        }
    }
}