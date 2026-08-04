// Title: Verify barcode pixel width using BarCodeWidth property
// Description: Demonstrates setting the barcode image width in pixels and validates that the generated image matches the expected width.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to control barcode dimensions via the AutoSizeMode and ImageWidth properties. It uses BarcodeGenerator, EncodeTypes, and related parameter classes to produce barcodes of specific sizes, a common requirement for UI layout, printing, and automated testing scenarios.
// Prompt: Design unit test verifying BarCodeWidth set in Pixels yields correct pixel width after generation.
// Tags: barcode, code128, imagewidth, pixels, autosize, unit-test, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a specific pixel width
/// and validates that the resulting image matches the expected dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets up the barcode generator, defines the desired width,
    /// creates the barcode image, verifies its width, and saves the result to a temporary file.
    /// </summary>
    static void Main()
    {
        // Desired barcode image width in pixels
        const int expectedWidth = 300;

        // Initialize a barcode generator for the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Assign the text to be encoded in the barcode
            generator.CodeText = "Test123";

            // Configure the generator to use interpolation for sizing
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image width in pixels
            generator.Parameters.ImageWidth.Pixels = expectedWidth;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Validate that the generated bitmap width matches the expected pixel width
                if (bitmap.Width != expectedWidth)
                {
                    throw new InvalidOperationException(
                        $"Barcode width mismatch. Expected: {expectedWidth}px, Actual: {bitmap.Width}px");
                }

                // Save the bitmap to a temporary PNG file for optional visual verification
                string outputPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");
                bitmap.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully with width {bitmap.Width}px. Saved to {outputPath}");
            }
        }
    }
}