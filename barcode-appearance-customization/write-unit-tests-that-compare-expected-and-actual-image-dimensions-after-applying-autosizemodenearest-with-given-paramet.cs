// Title: AutoSizeMode.Nearest Barcode Image Dimension Verification
// Description: Demonstrates generating DataMatrix barcodes with Aspose.BarCode using AutoSizeMode.Nearest and verifies that the resulting image dimensions match the specified width and height.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to control barcode image size with AutoSizeMode, set pixel dimensions, and validate output. It uses BarcodeGenerator, AutoSizeMode, and image handling classes, typical for developers needing precise barcode sizing for UI or printing.
// Prompt: Write unit tests that compare expected and actual image dimensions after applying AutoSizeMode.Nearest with given parameters.
// Tags: datamatrix, autosizemode, image-dimensions, png, aspose.barcode, generation, testing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates DataMatrix barcodes with specific pixel dimensions
/// using <c>AutoSizeMode.Nearest</c> and validates the actual image size against the expected values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes a series of dimension verification tests.
    /// </summary>
    static void Main()
    {
        // Define test cases: expected width, expected height, XDimension in pixels
        RunTest("Test1", 300, 300, 3);
        RunTest("Test2", 200, 150, 2);
        RunTest("Test3", 500, 400, 5);
    }

    /// <summary>
    /// Generates a barcode image with the specified parameters, saves it temporarily,
    /// and checks whether the actual bitmap dimensions match the expected ones.
    /// </summary>
    /// <param name="testName">Identifier for the test case.</param>
    /// <param name="expectedWidth">Desired image width in pixels.</param>
    /// <param name="expectedHeight">Desired image height in pixels.</param>
    /// <param name="xDimensionPixels">X-dimension (module size) in pixels.</param>
    static void RunTest(string testName, int expectedWidth, int expectedHeight, int xDimensionPixels)
    {
        // Create a unique temporary folder for this test
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, testName + ".png");

        try
        {
            // Initialize the barcode generator for DataMatrix symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
            {
                // Configure auto‑size mode and explicit pixel dimensions
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.ImageWidth.Pixels = expectedWidth;
                generator.Parameters.ImageHeight.Pixels = expectedHeight;
                generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

                // Generate the barcode bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the bitmap to disk for optional visual verification
                    using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fs, ImageFormat.Png);
                    }

                    // Retrieve actual dimensions from the generated bitmap
                    int actualWidth = bitmap.Width;
                    int actualHeight = bitmap.Height;

                    // Compare actual dimensions with expected values
                    bool widthMatch = actualWidth == expectedWidth;
                    bool heightMatch = actualHeight == expectedHeight;

                    if (widthMatch && heightMatch)
                    {
                        Console.WriteLine($"{testName}: PASS (Width={actualWidth}, Height={actualHeight})");
                    }
                    else
                    {
                        Console.WriteLine($"{testName}: FAIL (Expected Width={expectedWidth}, Height={expectedHeight}; Actual Width={actualWidth}, Height={actualHeight})");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected exceptions during generation or validation
            Console.WriteLine($"{testName}: EXCEPTION - {ex.Message}");
        }
        finally
        {
            // Cleanup temporary files and folder
            try
            {
                if (File.Exists(barcodePath))
                {
                    File.Delete(barcodePath);
                }
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Ignored cleanup errors
            }
        }
    }
}