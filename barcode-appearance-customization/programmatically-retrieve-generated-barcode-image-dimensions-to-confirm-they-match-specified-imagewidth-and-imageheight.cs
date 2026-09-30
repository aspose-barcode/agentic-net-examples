// Title: Retrieve and verify barcode image dimensions
// Description: Demonstrates how to generate a barcode with specific image width and height, then programmatically retrieve the bitmap dimensions to confirm they match the settings.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, ImageWidth, ImageHeight, and AutoSizeMode to control output size. Developers often need to ensure generated barcode images meet exact pixel dimensions for UI layout, printing, or integration with other graphics pipelines. The snippet shows how to access the resulting Bitmap size and compare it against the configured parameters.
// Prompt: Programmatically retrieve the generated barcode image dimensions to confirm they match the specified ImageWidth and ImageHeight.
// Tags: barcode, code128, image dimensions, autosize mode, bitmap, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with fixed dimensions and verifying the resulting image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, checks dimensions, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        string codeText = "1234567890";
        float expectedWidthPixels = 300f;
        float expectedHeightPixels = 150f;

        // Create a temporary file path for the barcode image (optional, not required for dimension check)
        string tempPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Generate the barcode with fixed image dimensions
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set fixed image size and enforce it
            generator.Parameters.ImageWidth.Pixels = expectedWidthPixels;
            generator.Parameters.ImageHeight.Pixels = expectedHeightPixels;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Generate the bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve actual dimensions
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Compare with expected dimensions (rounded to nearest integer)
                int expectedWidth = (int)Math.Round(expectedWidthPixels);
                int expectedHeight = (int)Math.Round(expectedHeightPixels);

                Console.WriteLine($"Expected Width: {expectedWidth}px, Actual Width: {actualWidth}px");
                Console.WriteLine($"Expected Height: {expectedHeight}px, Actual Height: {actualHeight}px");

                if (actualWidth == expectedWidth && actualHeight == expectedHeight)
                {
                    Console.WriteLine("Dimensions match the specified ImageWidth and ImageHeight.");
                }
                else
                {
                    Console.WriteLine("Dimensions do NOT match the specified ImageWidth and ImageHeight.");
                }

                // Optionally save the image to verify visually
                bitmap.Save(tempPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {tempPath}");
            }
        }
    }
}