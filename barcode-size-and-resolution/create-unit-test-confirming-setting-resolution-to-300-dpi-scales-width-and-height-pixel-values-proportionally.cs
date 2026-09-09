// Title: Verify barcode image scaling with resolution change
// Description: Demonstrates how changing the resolution of a generated barcode image scales its pixel dimensions proportionally.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, setting physical module size via XDimension, and adjusting image resolution. Developers often need to control image size for printing or display, and must ensure that resolution changes affect width and height consistently. The snippet shows typical steps for creating a barcode, configuring parameters, and validating scaling behavior.
// Prompt: Create unit test confirming setting resolution to 300 dpi scales width and height pixel values proportionally.
// Tags: barcode, datamatrix, resolution, scaling, aspose.barcode, generation, unit-test, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that verifies barcode image dimensions scale proportionally when the resolution is changed.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image at the specified resolution and returns its pixel dimensions.
    /// </summary>
    /// <param name="resolution">Desired image resolution in DPI.</param>
    /// <returns>Tuple containing the image width and height in pixels.</returns>
    static (int Width, int Height) GenerateSize(float resolution)
    {
        // Create a barcode generator for DataMatrix with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Set a fixed physical module size (1 mm per module).
            generator.Parameters.Barcode.XDimension.Millimeters = 1f;

            // Apply the requested resolution (DPI) to the generator.
            generator.Parameters.Resolution = resolution;

            // Generate the barcode image in memory.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Return the image dimensions.
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point that compares low‑ and high‑resolution barcode images to confirm proportional scaling.
    /// </summary>
    static void Main()
    {
        const float lowRes = 96f;   // Baseline resolution (dpi)
        const float highRes = 300f; // Target resolution (dpi)
        const float tolerance = 0.01f; // Acceptable deviation (1%)

        // Generate image sizes for both resolutions.
        var lowSize = GenerateSize(lowRes);
        var highSize = GenerateSize(highRes);

        // Expected scaling factor based on resolution ratio.
        float expectedScale = highRes / lowRes;

        // Actual scaling observed in width and height.
        float actualScaleW = (float)highSize.Width / lowSize.Width;
        float actualScaleH = (float)highSize.Height / lowSize.Height;

        // Verify that the observed scaling matches the expected factor within tolerance.
        bool widthOk = Math.Abs(actualScaleW - expectedScale) <= tolerance;
        bool heightOk = Math.Abs(actualScaleH - expectedScale) <= tolerance;

        // Output diagnostic information.
        Console.WriteLine($"Low resolution ({lowRes} dpi) size:  {lowSize.Width}x{lowSize.Height} pixels");
        Console.WriteLine($"High resolution ({highRes} dpi) size: {highSize.Width}x{highSize.Height} pixels");
        Console.WriteLine($"Expected scale factor: {expectedScale:F4}");
        Console.WriteLine($"Actual width scale:    {actualScaleW:F4}");
        Console.WriteLine($"Actual height scale:   {actualScaleH:F4}");

        // Report test result.
        if (widthOk && heightOk)
        {
            Console.WriteLine("PASSED: Width and height scale proportionally with resolution.");
        }
        else
        {
            Console.WriteLine("FAILED: Scaling does not match expected proportion.");
        }
    }
}