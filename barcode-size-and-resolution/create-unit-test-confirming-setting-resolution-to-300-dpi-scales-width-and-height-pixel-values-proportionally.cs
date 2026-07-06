// Title: Verify barcode image resolution scaling
// Description: Demonstrates that setting the barcode generator resolution to 300 dpi scales the image width and height proportionally compared to the default 96 dpi.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how the Resolution property and AutoSizeMode.Interpolation affect pixel dimensions. It shows typical usage of BarcodeGenerator, its Parameters, and the Aspose.Drawing.Bitmap class for creating barcode images at different DPI settings, a common requirement for high‑resolution printing and scanning scenarios.
// Prompt: Create unit test confirming setting resolution to 300 dpi scales width and height pixel values proportionally.
// Tags: barcode, code128, resolution, dpi, interpolation, image generation, aspose.barcode, unit test

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that verifies the effect of changing the barcode image resolution
/// on the generated bitmap's pixel dimensions. It compares a 96 dpi image with a
/// 300 dpi image to ensure proportional scaling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images at two different DPI
    /// settings and checks that width and height scale proportionally.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the value "Test"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test"))
        {
            // Enable interpolation mode so that resolution changes affect pixel size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a base image size in pixels (width = 200, height = 100)
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Generate image at the default resolution of 96 dpi
            generator.Parameters.Resolution = 96f;
            using (Bitmap bmp96 = generator.GenerateBarCodeImage())
            {
                int width96 = bmp96.Width;
                int height96 = bmp96.Height;

                // Generate image at a higher resolution of 300 dpi
                generator.Parameters.Resolution = 300f;
                using (Bitmap bmp300 = generator.GenerateBarCodeImage())
                {
                    int width300 = bmp300.Width;
                    int height300 = bmp300.Height;

                    // Expected scaling factor based on DPI change
                    float factor = 300f / 96f;

                    // Allow a tolerance of 1 pixel due to rounding differences
                    bool widthOk = Math.Abs(width300 - (int)Math.Round(width96 * factor)) <= 1;
                    bool heightOk = Math.Abs(height300 - (int)Math.Round(height96 * factor)) <= 1;

                    // Output test result
                    if (widthOk && heightOk)
                        Console.WriteLine("PASSED");
                    else
                        Console.WriteLine($"FAILED: Expected width≈{width96 * factor}, got {width300}; height≈{height96 * factor}, got {height300}");
                }
            }
        }
    }
}