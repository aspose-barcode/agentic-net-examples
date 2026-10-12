// Title: Verify resolution scaling of barcode image dimensions
// Description: Demonstrates how setting the barcode generator resolution to 300 dpi proportionally scales the resulting image width and height compared to a lower resolution.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters.Resolution property, and the resulting Bitmap dimensions. Developers often need to ensure that changing DPI settings scales images correctly for printing or display, making this a common validation scenario.
// Prompt: Create unit test confirming setting resolution to 300 dpi scales width and height pixel values proportionally.
// Tags: datamatrix, resolution, scaling, unit-test, aspose.barcode, image-generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point for the resolution scaling verification example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcode images at two different resolutions and verifies that the dimensions scale proportionally.
    /// </summary>
    static void Main()
    {
        // Define the barcode text and the two resolutions to compare.
        const string codeText = "ASPOSE";
        const float resolutionLow = 96f;
        const float resolutionHigh = 300f;

        // Generate image dimensions for low and high resolution settings.
        (int widthLow, int heightLow) = GenerateDimensions(codeText, resolutionLow);
        (int widthHigh, int heightHigh) = GenerateDimensions(codeText, resolutionHigh);

        // Calculate the expected scaling factor based on DPI values.
        double expectedFactor = resolutionHigh / resolutionLow;

        // Determine the actual scaling factors observed in width and height.
        double actualFactorW = (double)widthHigh / widthLow;
        double actualFactorH = (double)heightHigh / heightLow;

        // Allow a small tolerance (5%) for rounding differences.
        double tolerance = 0.05; // 5%

        // Verify that both width and height factors are within the tolerance.
        bool widthMatches = Math.Abs(actualFactorW - expectedFactor) / expectedFactor < tolerance;
        bool heightMatches = Math.Abs(actualFactorH - expectedFactor) / expectedFactor < tolerance;
        bool testPassed = widthMatches && heightMatches;

        // Output the results.
        Console.WriteLine($"Low resolution (96 dpi) size: {widthLow}x{heightLow}");
        Console.WriteLine($"High resolution (300 dpi) size: {widthHigh}x{heightHigh}");
        Console.WriteLine($"Expected scaling factor: {expectedFactor:F3}");
        Console.WriteLine($"Actual width factor: {actualFactorW:F3}");
        Console.WriteLine($"Actual height factor: {actualFactorH:F3}");
        Console.WriteLine(testPassed ? "TEST PASSED" : "TEST FAILED");
    }

    /// <summary>
    /// Generates a barcode image for the specified text and resolution, returning its pixel dimensions.
    /// </summary>
    /// <param name="text">The data to encode in the barcode.</param>
    /// <param name="resolution">The desired image resolution (DPI).</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    private static (int width, int height) GenerateDimensions(string text, float resolution)
    {
        // Initialize the barcode generator with DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
        {
            // Set a fixed X-dimension (module size) in millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 1f;

            // Apply the requested resolution.
            generator.Parameters.Resolution = resolution;

            // Generate the bitmap and retrieve its dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }
}