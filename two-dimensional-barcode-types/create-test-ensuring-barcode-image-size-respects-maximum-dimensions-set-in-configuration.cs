// Title: Verify barcode image respects configured maximum dimensions
// Description: Demonstrates generating a Code128 barcode with specified maximum width and height, then checks that the resulting image does not exceed those limits.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure image size constraints using BarcodeGenerator, AutoSizeMode, and image parameters. Developers often need to ensure generated barcodes fit within layout specifications for reports, labels, or UI components. The snippet shows retrieving actual dimensions and validating against configured limits, a common task when integrating barcode generation into automated tests.
// Prompt: Create a test ensuring barcode image size respects maximum dimensions set in configuration.
// Tags: barcode, code128, image size, max dimensions, autosizemode, generation, testing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode and verifying its image dimensions against configured maximum limits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, enforces maximum width/height, validates size, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define maximum allowed dimensions for the barcode image.
        const int maxWidth = 300;
        const int maxHeight = 200;
        const string codeText = "Test123";

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the maximum width and height constraints.
            generator.Parameters.ImageWidth.Pixels = maxWidth;
            generator.Parameters.ImageHeight.Pixels = maxHeight;

            // Set AutoSizeMode to Nearest so the generator respects the size limits.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve the actual dimensions of the generated image.
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                Console.WriteLine($"Generated barcode size: {actualWidth}x{actualHeight} pixels");

                // Verify that the image dimensions are within the specified limits.
                bool withinLimits = actualWidth <= maxWidth && actualHeight <= maxHeight;
                Console.WriteLine(withinLimits
                    ? "PASS: Image size respects maximum dimensions."
                    : "FAIL: Image size exceeds maximum dimensions.");

                // Save the barcode image to a temporary file for inspection.
                string outPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    File.WriteAllBytes(outPath, stream.ToArray());
                    Console.WriteLine($"Barcode image saved to: {outPath}");
                }
            }
        }
    }
}