// Title: Verify barcode image width when setting BarCodeWidth in pixels
// Description: Demonstrates how to set the barcode image width in pixels using Aspose.BarCode and validates the generated image matches the expected width.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and ImageWidth properties to control output dimensions. Developers often need to produce barcodes with exact pixel sizes for UI layout or printing requirements; this snippet shows how to configure and verify those settings.
// Prompt: Design unit test verifying BarCodeWidth set in Pixels yields correct pixel width after generation.
// Tags: code128, barcode width, pixel, image generation, aspose.barcode, aspose.drawing, unit test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the barcode width verification example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with a specified pixel width and validates the resulting image dimensions.
    /// </summary>
    static void Main()
    {
        // Expected barcode image width in pixels
        int expectedWidth = 300;

        // Initialize a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode in the barcode
            generator.CodeText = "Test123";

            // Enable interpolation mode so the ImageWidth setting is applied accurately
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the desired image width in pixels
            generator.Parameters.ImageWidth.Pixels = expectedWidth;

            // Generate the barcode image as a bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Capture the actual width of the generated bitmap
                int actualWidth = bitmap.Width;

                // Output expected and actual widths for diagnostic purposes
                Console.WriteLine($"Expected width: {expectedWidth} px");
                Console.WriteLine($"Actual width:   {actualWidth} px");

                // Simple verification: compare actual width to expected width
                if (actualWidth == expectedWidth)
                {
                    Console.WriteLine("Test passed: BarCodeWidth set in pixels yields correct image width.");
                }
                else
                {
                    Console.WriteLine("Test failed: Image width does not match the expected value.");
                }
            }
        }
    }
}