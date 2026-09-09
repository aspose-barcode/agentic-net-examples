// Title: Validate barcode image dimensions at 96 dpi for a 20 mm wide Code128 barcode
// Description: This example generates a Code128 barcode with a target width of 20 mm at 96 dpi and verifies that the resulting bitmap width matches the expected pixel count.
// Category-Description: Demonstrates Aspose.BarCode image generation and resolution handling. It uses BarcodeGenerator, EncodeTypes, and image parameters such as Resolution, AutoSizeMode, and size units (millimeters). Typical use cases include ensuring barcode images meet precise physical dimensions for printing or scanning requirements. Developers often need to validate pixel dimensions when integrating barcode generation into automated workflows.
/// Prompt: Validate barcode generated at 96 dpi matches expected pixel dimensions for 20 mm width.
// Tags: code128, barcode, dimension validation, image generation, resolution, aspose.barcode, bitmap

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode of a specific physical width and validating its pixel dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, computes expected pixel width, and compares with actual bitmap width.
    /// </summary>
    static void Main()
    {
        // Define the target physical width (in millimeters) and the desired resolution (dots per inch).
        const float targetWidthMm = 20f;
        const float dpi = 96f;

        // Calculate the expected pixel width using the conversion: inches = mm / 25.4, then multiply by DPI.
        int expectedPixels = (int)Math.Round(targetWidthMm / 25.4f * dpi);

        // Create a barcode generator for Code128 with the data "A".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "A"))
        {
            // Set the image resolution.
            generator.Parameters.Resolution = dpi;

            // Choose the auto‑size mode that selects the nearest size that satisfies the dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Specify the desired image width and an arbitrary height in millimeters.
            generator.Parameters.ImageWidth.Millimeters = targetWidthMm;
            generator.Parameters.ImageHeight.Millimeters = 10f;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve the actual bitmap width in pixels.
                int actualWidth = bitmap.Width;

                // Output the expected and actual widths for verification.
                Console.WriteLine($"Expected width (pixels): {expectedPixels}");
                Console.WriteLine($"Actual width (pixels): {actualWidth}");

                // Compare and report the validation result.
                if (actualWidth == expectedPixels)
                    Console.WriteLine("Validation passed: dimensions match.");
                else
                    Console.WriteLine("Validation failed: dimensions do not match.");
            }
        }
    }
}