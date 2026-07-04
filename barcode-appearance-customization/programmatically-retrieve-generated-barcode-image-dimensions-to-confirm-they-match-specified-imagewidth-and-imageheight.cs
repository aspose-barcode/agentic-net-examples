// Title: Retrieve and verify barcode image dimensions
// Description: Demonstrates generating a Code128 barcode with specific dimensions and programmatically confirming the generated image size matches the requested width and height.
// Prompt: Programmatically retrieve the generated barcode image dimensions to confirm they match the specified ImageWidth and ImageHeight.
// Tags: barcode, code128, dimensions, verification, aspose.barcode, image

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with specified dimensions and verifies the output size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, checks dimensions, and saves the image.
    /// </summary>
    static void Main()
    {
        // Desired dimensions in points (1 point = 1/72 inch)
        float desiredWidth = 300f;
        float desiredHeight = 150f;

        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Use interpolation mode so ImageWidth/ImageHeight are respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Assign the requested image size using the Point unit
            generator.Parameters.ImageWidth.Point = desiredWidth;
            generator.Parameters.ImageHeight.Point = desiredHeight;

            // Generate the barcode image as a bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Actual pixel dimensions of the generated image
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Output expected dimensions (points) and actual dimensions (pixels)
                Console.WriteLine($"Expected width (points): {desiredWidth}");
                Console.WriteLine($"Expected height (points): {desiredHeight}");
                Console.WriteLine($"Actual image width (pixels): {actualWidth}");
                Console.WriteLine($"Actual image height (pixels): {actualHeight}");

                // Convert expected points to pixels using the generator's resolution (dpi)
                float resolution = generator.Parameters.Resolution; // default 96 dpi
                int expectedPixelWidth = (int)Math.Round(desiredWidth * resolution / 72f);
                int expectedPixelHeight = (int)Math.Round(desiredHeight * resolution / 72f);

                // Verify whether the actual pixel dimensions match the expected values
                bool widthMatches = actualWidth == expectedPixelWidth;
                bool heightMatches = actualHeight == expectedPixelHeight;

                Console.WriteLine($"Width matches expected pixels: {widthMatches}");
                Console.WriteLine($"Height matches expected pixels: {heightMatches}");

                // Save the barcode image to a file (optional)
                generator.Save("barcode.png");
            }
        }
    }
}