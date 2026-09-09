// Title: Verify barcode pixel width using BarCodeWidth property
// Description: Demonstrates setting the barcode image width in pixels and checking that the generated image matches the expected width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control image dimensions via the Parameters.ImageWidth property. It shows typical usage of BarcodeGenerator, EncodeTypes, and AutoSizeMode for precise sizing, a common requirement when integrating barcodes into fixed-layout documents or UI components. Developers often need to validate that the generated barcode meets exact pixel specifications for printing or display.
// Prompt: Design unit test verifying BarCodeWidth set in Pixels yields correct pixel width after generation.
// Tags: barcode, code128, image width, pixels, autosizemode, generation, aspose.barcode, unit test

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that sets barcode image width in pixels and verifies the generated width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with a specific pixel width and checks the result.
    /// </summary>
    static void Main()
    {
        // Desired barcode width in pixels.
        const float targetWidth = 300f;

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image width in pixels.
            generator.Parameters.ImageWidth.Pixels = targetWidth;

            // Use nearest auto-size mode to match the requested width as closely as possible.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Generate the barcode image.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Actual width of the generated bitmap.
                int actualWidth = bitmap.Width;

                // Expected width cast to integer (pixel precision).
                int expectedWidth = (int)targetWidth;

                // Compare actual and expected widths and output the result.
                if (actualWidth == expectedWidth)
                {
                    Console.WriteLine("PASSED: Barcode width matches expected pixel width.");
                }
                else
                {
                    Console.WriteLine($"FAILED: Expected width {expectedWidth}px, but got {actualWidth}px.");
                }
            }
        }
    }
}