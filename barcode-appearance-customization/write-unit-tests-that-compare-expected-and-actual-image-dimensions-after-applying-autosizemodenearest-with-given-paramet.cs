// Title: Barcode AutoSizeMode.Nearest dimension verification example
// Description: Demonstrates generating Code128 barcodes with specific pixel dimensions using AutoSizeMode.Nearest and validates that the produced image matches the expected width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode image size with the AutoSizeMode property. It uses BarcodeGenerator, AutoSizeMode, and image dimension properties, common tasks for developers needing precise barcode rendering for UI or printing scenarios. Suitable for search queries about barcode image sizing and verification.
// Prompt: Write unit tests that compare expected and actual image dimensions after applying AutoSizeMode.Nearest with given parameters.
// Tags: barcode, code128, autosizemode, image-dimensions, aspose.barcode, generation, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates barcodes with AutoSizeMode.Nearest and verifies image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes two dimension verification tests.
    /// </summary>
    static void Main()
    {
        // Test 1: Verify a 300x100 pixel barcode
        RunTest(
            testName: "Test1",
            codeText: "12345",
            encodeType: EncodeTypes.Code128,
            widthPixels: 300f,
            heightPixels: 100f,
            expectedWidth: 300,
            expectedHeight: 100);

        // Test 2: Verify a 250x150 pixel barcode
        RunTest(
            testName: "Test2",
            codeText: "ABCDE",
            encodeType: EncodeTypes.Code128,
            widthPixels: 250f,
            heightPixels: 150f,
            expectedWidth: 250,
            expectedHeight: 150);
    }

    /// <summary>
    /// Generates a barcode with the specified parameters, applies AutoSizeMode.Nearest,
    /// and compares the actual image dimensions to the expected values.
    /// </summary>
    /// <param name="testName">Identifier for the test case.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="encodeType">Symbology type (e.g., Code128).</param>
    /// <param name="widthPixels">Target image width in pixels.</param>
    /// <param name="heightPixels">Target image height in pixels.</param>
    /// <param name="expectedWidth">Expected width of the generated image.</param>
    /// <param name="expectedHeight">Expected height of the generated image.</param>
    static void RunTest(string testName, string codeText, BaseEncodeType encodeType, float widthPixels, float heightPixels, int expectedWidth, int expectedHeight)
    {
        try
        {
            // Initialize the barcode generator with the chosen symbology and data
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure AutoSizeMode to automatically adjust to the nearest size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

                // Set the desired image dimensions (in pixels)
                generator.Parameters.ImageWidth.Pixels = widthPixels;
                generator.Parameters.ImageHeight.Pixels = heightPixels;

                // Generate the barcode image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    int actualWidth = bitmap.Width;
                    int actualHeight = bitmap.Height;

                    bool widthMatch = actualWidth == expectedWidth;
                    bool heightMatch = actualHeight == expectedHeight;

                    if (widthMatch && heightMatch)
                    {
                        Console.WriteLine($"{testName}: PASSED (Width={actualWidth}, Height={actualHeight})");
                    }
                    else
                    {
                        Console.WriteLine($"{testName}: FAILED");
                        Console.WriteLine($"  Expected Width={expectedWidth}, Height={expectedHeight}");
                        Console.WriteLine($"  Actual   Width={actualWidth}, Height={actualHeight}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{testName}: EXCEPTION - {ex.Message}");
        }
    }
}