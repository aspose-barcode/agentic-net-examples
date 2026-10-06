// Title: MaxiCode Aspect Ratio Impact on Barcode Dimensions
// Description: Demonstrates how changing the AspectRatio property of a MaxiCode barcode influences the generated image's width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and the MaxiCode aspect ratio settings to control image dimensions. Developers often need to adjust aspect ratios for layout constraints or visual consistency, making this a common scenario when integrating barcode generation into applications.
// Prompt: Write unit tests to verify aspect ratio adjustments affect MaxiCode barcode dimensions as expected.
// Tags: maxicode, aspectratio, barcode, generation, dimensions, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates MaxiCode barcodes with different aspect ratios
/// and validates the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, prints dimensions, and runs simple validation checks.
    /// </summary>
    static void Main()
    {
        // Generate barcodes with two different aspect ratios: 1.0 (square) and 0.5 (tall)
        using (Bitmap bmpAspect1 = GenerateMaxiCode(1.0f))
        using (Bitmap bmpAspectHalf = GenerateMaxiCode(0.5f))
        {
            // Output the dimensions for manual inspection
            Console.WriteLine($"AspectRatio 1.0 -> Width: {bmpAspect1.Width}, Height: {bmpAspect1.Height}");
            Console.WriteLine($"AspectRatio 0.5 -> Width: {bmpAspectHalf.Width}, Height: {bmpAspectHalf.Height}");

            // Test 1: AspectRatio 1.0 should produce roughly square dimensions (width ≈ height)
            bool testSquare = Math.Abs(bmpAspect1.Width - bmpAspect1.Height) <= 2;
            Console.WriteLine($"Test Square (AspectRatio 1.0): {(testSquare ? "PASS" : "FAIL")}");

            // Test 2: AspectRatio 0.5 should produce a height roughly double the width
            bool testHalf = Math.Abs(bmpAspectHalf.Height - 2 * bmpAspectHalf.Width) <= 2;
            Console.WriteLine($"Test Height≈2×Width (AspectRatio 0.5): {(testHalf ? "PASS" : "FAIL")}");

            // Summarize overall test result
            if (testSquare && testHalf)
                Console.WriteLine("All aspect ratio tests passed.");
            else
                Console.WriteLine("One or more aspect ratio tests failed.");
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode image with the specified aspect ratio.
    /// </summary>
    /// <param name="aspectRatio">Desired aspect ratio (e.g., 1.0 for square, 0.5 for tall).</param>
    /// <returns>A Bitmap containing the generated barcode.</returns>
    static Bitmap GenerateMaxiCode(float aspectRatio)
    {
        // Create a temporary folder for intermediate output (not required for the test logic)
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempPath);
        string dummyFile = Path.Combine(tempPath, "dummy.png");

        // Configure the barcode generator for MaxiCode with the given aspect ratio
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.MaxiCode, "AspectTest"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 15;
            gen.Parameters.Barcode.MaxiCode.AspectRatio = aspectRatio;

            // Save to a dummy file to ensure generation works (file not used later)
            gen.Save(dummyFile, BarCodeImageFormat.Png);

            // Generate and return the bitmap for dimension inspection
            Bitmap bitmap = gen.GenerateBarCodeImage();
            return bitmap;
        }
    }
}