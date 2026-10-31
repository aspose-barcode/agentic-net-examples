// Title: Verify proportional scaling of barcode image dimensions when changing resolution
// Description: This example generates a DataMatrix barcode at two different DPI settings and checks that the image width and height scale proportionally.
// Category-Description: Demonstrates Aspose.BarCode resolution handling, part of the barcode generation and image rendering category. It uses BarcodeGenerator, its Parameters (Resolution, XDimension), and Aspose.Drawing.Bitmap to produce images. Developers often need to ensure that changing DPI preserves visual size ratios, especially for printing or high‑resolution output scenarios.
// Prompt: Create unit test confirming setting resolution to 300 dpi scales width and height pixel values proportionally.
// Tags: datamatrix, resolution, scaling, unit-test, aspose.barcode, image-generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a simple verification that changing the barcode generator resolution
/// scales the resulting image dimensions proportionally.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images at 96 dpi and 300 dpi, compares their dimensions,
    /// and reports whether the scaling matches the expected factor within a tolerance.
    /// </summary>
    static void Main()
    {
        // Define low and high DPI values and an acceptable tolerance (5%).
        const float lowResolution = 96f;
        const float highResolution = 300f;
        const float tolerance = 0.05f; // 5%

        // Generate barcode images at the two resolutions and obtain their pixel sizes.
        var lowSize = GetBarcodeSize(lowResolution);
        var highSize = GetBarcodeSize(highResolution);

        // Expected scaling factor based on DPI change.
        double expectedFactor = highResolution / lowResolution;

        // Actual scaling factors for width and height.
        double widthFactor = (double)highSize.width / lowSize.width;
        double heightFactor = (double)highSize.height / lowSize.height;

        // Verify that actual factors are within the tolerance of the expected factor.
        bool widthOk = Math.Abs(widthFactor - expectedFactor) / expectedFactor <= tolerance;
        bool heightOk = Math.Abs(heightFactor - expectedFactor) / expectedFactor <= tolerance;

        // Output test result.
        if (widthOk && heightOk)
        {
            Console.WriteLine("TEST PASSED: Resolution scaling is proportional.");
        }
        else
        {
            Console.WriteLine("TEST FAILED:");
            Console.WriteLine($"Low resolution size:  {lowSize.width}x{lowSize.height}");
            Console.WriteLine($"High resolution size: {highSize.width}x{highSize.height}");
            Console.WriteLine($"Expected factor: {expectedFactor:F3}");
            Console.WriteLine($"Actual width factor: {widthFactor:F3} (within tolerance: {widthOk})");
            Console.WriteLine($"Actual height factor: {heightFactor:F3} (within tolerance: {heightOk})");
        }
    }

    /// <summary>
    /// Generates a DataMatrix barcode image at the specified resolution and returns its pixel dimensions.
    /// </summary>
    /// <param name="resolution">The DPI resolution to apply to the barcode generator.</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    private static (int width, int height) GetBarcodeSize(float resolution)
    {
        // Create a barcode generator for DataMatrix with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Set module size (X dimension) to 1 mm for consistency.
            generator.Parameters.Barcode.XDimension.Millimeters = 1;
            // Apply the desired resolution.
            generator.Parameters.Resolution = resolution;

            // Generate the barcode image and retrieve its dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }
}