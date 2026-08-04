// Title: Verify barcode image resolution scaling
// Description: Demonstrates how setting the barcode generator resolution to 300 dpi scales the resulting image dimensions proportionally compared to the default 96 dpi.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters.ImageWidth/Height, and Parameters.Resolution properties. Developers often need to control output resolution for high‑quality printing or screen rendering, and this snippet shows the typical workflow of generating, saving, and measuring barcode images at different DPI settings.
// Prompt: Create unit test confirming setting resolution to 300 dpi scales width and height pixel values proportionally.
// Tags: barcode symbology, resolution, png, barcodegenerator, image

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates resolution scaling of barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates barcode images at 96 dpi and 300 dpi, then verifies proportional scaling of pixel dimensions.
    /// </summary>
    static void Main()
    {
        // Define logical size in points (1 point = 1/72 inch)
        const float logicalWidthPoints = 200f;
        const float logicalHeightPoints = 100f;

        // Generate first image with default resolution (96 dpi)
        var size96 = GenerateBarcodeImage(logicalWidthPoints, logicalHeightPoints, 96f);

        // Generate second image with higher resolution (300 dpi)
        var size300 = GenerateBarcodeImage(logicalWidthPoints, logicalHeightPoints, 300f);

        // Expected scaling factor based on DPI change
        float expectedFactor = 300f / 96f;

        // Verify that width and height are scaled proportionally within a small tolerance
        bool widthMatches = Math.Abs((float)size300.width / size96.width - expectedFactor) < 0.01f;
        bool heightMatches = Math.Abs((float)size300.height / size96.height - expectedFactor) < 0.01f;

        if (widthMatches && heightMatches)
        {
            Console.WriteLine("PASSED: Resolution scaling works as expected.");
        }
        else
        {
            Console.WriteLine("FAILED: Resolution scaling mismatch.");
            Console.WriteLine($"96dpi size:  {size96.width}x{size96.height}");
            Console.WriteLine($"300dpi size: {size300.width}x{size300.height}");
        }
    }

    // Generates a barcode image with the specified logical size and resolution,
    // then returns the pixel dimensions of the saved image.
    static (int width, int height) GenerateBarcodeImage(float widthPoints, float heightPoints, float resolutionDpi)
    {
        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test"))
        {
            // Set logical image size in points
            generator.Parameters.ImageWidth.Point = widthPoints;
            generator.Parameters.ImageHeight.Point = heightPoints;

            // Apply the desired resolution (DPI)
            generator.Parameters.Resolution = resolutionDpi;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Load the image from the stream to read its pixel dimensions
                using (var bitmap = (Bitmap)Image.FromStream(ms))
                {
                    return (bitmap.Width, bitmap.Height);
                }
            }
        }
    }
}