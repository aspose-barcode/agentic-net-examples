// Title: Generate Postal Planet Barcode with Custom Padding and Verify Image Size
// Description: Demonstrates creating a Planet postal barcode, applying custom margin (padding) settings, and confirming that the resulting image dimensions reflect the added padding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the use of BarcodeGenerator, setting XDimension, BarHeight, and Padding properties, and saving the output as PNG. Developers working with postal barcodes often need to adjust margins for layout requirements and verify image dimensions for downstream processing.
// Prompt: Generate a postal barcode with custom margin settings and verify image dimensions match expectations.
// Tags: postal barcode, padding, image dimensions, aspose.barcode, barcodegenerator, png, verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Planet postal barcode with and without custom padding,
/// saving the images, and verifying that the padding affects image dimensions as expected.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, applies custom padding,
    /// saves them to a temporary folder, and writes verification results to the console.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "PostalBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Common barcode settings
        string codeText = "123456";
        BaseEncodeType encodeType = EncodeTypes.Planet;
        float xDim = 4f;          // Width of the smallest bar (X-dimension) in pixels
        float barHeight = 50f;    // Height of the barcode bars in pixels

        // ------------------------------------------------------------
        // Generate barcode without custom padding
        // ------------------------------------------------------------
        int widthNoPad, heightNoPad;
        string pathNoPad = Path.Combine(outputDir, "PostalPlanet_NoPadding.png");
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set basic barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = xDim;
            generator.Parameters.Barcode.BarHeight.Pixels = barHeight;

            // Render the barcode image
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                widthNoPad = bmp.Width;
                heightNoPad = bmp.Height;

                // Save the image as PNG
                bmp.Save(pathNoPad, ImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Generate barcode with custom padding (margin)
        // ------------------------------------------------------------
        int widthPad, heightPad;
        string pathPad = Path.Combine(outputDir, "PostalPlanet_WithPadding.png");

        // Padding values in pixels
        float padLeft = 10f, padRight = 10f, padTop = 20f, padBottom = 20f;

        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set basic barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = xDim;
            generator.Parameters.Barcode.BarHeight.Pixels = barHeight;

            // Apply custom padding to each side
            generator.Parameters.Barcode.Padding.Left.Pixels = padLeft;
            generator.Parameters.Barcode.Padding.Right.Pixels = padRight;
            generator.Parameters.Barcode.Padding.Top.Pixels = padTop;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = padBottom;

            // Render the barcode image with padding
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                widthPad = bmp.Width;
                heightPad = bmp.Height;

                // Save the padded image as PNG
                bmp.Save(pathPad, ImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Verify that the padded image dimensions increased as expected
        // ------------------------------------------------------------
        bool widthMatches = widthPad == widthNoPad + (int)(padLeft + padRight);
        bool heightMatches = heightPad == heightNoPad + (int)(padTop + padBottom);

        Console.WriteLine($"No padding image size:  {widthNoPad}x{heightNoPad}");
        Console.WriteLine($"With padding image size: {widthPad}x{heightPad}");
        Console.WriteLine($"Expected width increase:  {padLeft + padRight} pixels");
        Console.WriteLine($"Expected height increase: {padTop + padBottom} pixels");
        Console.WriteLine($"Width verification: {(widthMatches ? "PASS" : "FAIL")}");
        Console.WriteLine($"Height verification: {(heightMatches ? "PASS" : "FAIL")}");
        Console.WriteLine($"Images saved to: {outputDir}");
    }
}