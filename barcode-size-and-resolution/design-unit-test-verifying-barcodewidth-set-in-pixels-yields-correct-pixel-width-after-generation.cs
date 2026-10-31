// Title: Verify barcode image width when setting BarCodeWidth in pixels
// Description: This example sets the BarCodeWidth property in pixels for a Code128 barcode, generates the image, and checks that the resulting bitmap width matches the requested value.
// Category-Description: Demonstrates Aspose.BarCode image generation settings, focusing on the AutoSizeMode and ImageWidth properties. Useful for developers needing precise control over barcode dimensions in pixel units, such as when integrating barcodes into UI layouts or printed media. Covers key classes like BarcodeGenerator, EncodeTypes, and ImageFormat.
// Prompt: Design unit test verifying BarCodeWidth set in Pixels yields correct pixel width after generation.
// Tags: barcode symbology, code128, image generation, pixel width, autosizemode, aspose.barcode, aspose.drawing, unit test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to set a barcode's width in pixels, generate the image,
/// and verify that the resulting bitmap width matches the expected value.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with a specific pixel width and validates the resulting image width.
    /// </summary>
    static void Main()
    {
        // Desired barcode width in pixels.
        const float targetWidth = 300f;

        // Temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Configure the generator to size the image nearest to the requested dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set the image width in pixels.
            generator.Parameters.ImageWidth.Pixels = targetWidth;

            // Generate the barcode image.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to the temporary path as PNG.
                bitmap.Save(outputPath, ImageFormat.Png);

                // Retrieve the actual width of the generated bitmap.
                int actualWidth = bitmap.Width;

                // Compare the actual width with the expected target width and output the result.
                if (actualWidth == (int)targetWidth)
                {
                    Console.WriteLine($"PASSED: Width {actualWidth} matches expected {targetWidth}.");
                }
                else
                {
                    Console.WriteLine($"FAILED: Width {actualWidth} does not match expected {targetWidth}.");
                }
            }
        }
    }
}