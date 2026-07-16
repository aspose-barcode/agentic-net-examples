// Title: Verify barcode image respects maximum dimensions
// Description: Demonstrates how to configure maximum width and height for a generated barcode and validates the output size.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image dimension parameters. Developers often need to ensure generated barcodes fit within layout constraints for printing or UI display, making size validation a common requirement.
// Prompt: Create a test ensuring barcode image size respects maximum dimensions set in configuration.
// Tags: barcode, code128, autosizemode, dimensions, validation, imagegeneration, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, limits its size,
/// and verifies that the resulting image does not exceed the configured dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures maximum image dimensions,
    /// generates the barcode, validates its size, and optionally saves the image.
    /// </summary>
    static void Main()
    {
        // Define maximum dimensions (in points) for the barcode image.
        float maxWidth = 200f;
        float maxHeight = 100f;

        // Initialize a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode.
            generator.CodeText = "Test123";

            // Use Interpolation auto-size mode so explicit dimensions are respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Apply the maximum width and height constraints.
            generator.Parameters.ImageWidth.Point = maxWidth;
            generator.Parameters.ImageHeight.Point = maxHeight;

            // Generate the barcode image as a bitmap.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Validate that the generated image dimensions are within the limits.
                bool widthOk = bitmap.Width <= (int)maxWidth;
                bool heightOk = bitmap.Height <= (int)maxHeight;

                if (widthOk && heightOk)
                {
                    Console.WriteLine("Test passed: Image dimensions are within the configured limits.");
                }
                else
                {
                    Console.WriteLine("Test failed: Image dimensions exceed the configured limits.");
                    Console.WriteLine($"Actual size: {bitmap.Width}x{bitmap.Height} pixels, " +
                                      $"Maximum allowed: {maxWidth}x{maxHeight} points.");
                }

                // Optionally save the image for visual inspection.
                try
                {
                    bitmap.Save("barcode.png", ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save image: {ex.Message}");
                }
            }
        }
    }
}