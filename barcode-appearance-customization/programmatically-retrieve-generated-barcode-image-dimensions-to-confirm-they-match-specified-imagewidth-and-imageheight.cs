// Title: Retrieve Barcode Image Dimensions and Verify Size
// Description: Demonstrates how to generate a barcode image with specific width and height, then programmatically retrieve its actual dimensions to confirm they match the requested size.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image parameter settings (ImageWidth, ImageHeight). Developers often need to control barcode image dimensions for UI layout, printing, or PDF embedding, and must verify that the generated image respects the specified size constraints.
// Prompt: Programmatically retrieve the generated barcode image dimensions to confirm they match the specified ImageWidth and ImageHeight.
// Tags: barcode, code128, image generation, dimensions, autosizemode, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with specified dimensions and verifying the output size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and checks actual image dimensions against expected values.
    /// </summary>
    static void Main()
    {
        // Define expected dimensions in pixels
        float expectedWidth = 300f;
        float expectedHeight = 150f;

        // Determine a temporary file path for the generated barcode image
        string outputPath = Path.Combine(Path.GetTempPath(), "generated_barcode.png");

        // Create a barcode generator for Code128 with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the generator to use a sizing mode that respects ImageWidth/ImageHeight
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = expectedWidth;
            generator.Parameters.ImageHeight.Pixels = expectedHeight;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to the specified file in PNG format
                bitmap.Save(outputPath, ImageFormat.Png);

                // Retrieve the actual dimensions of the generated bitmap
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Output the expected vs. actual dimensions for verification
                Console.WriteLine($"Expected Width: {expectedWidth} px, Actual Width: {actualWidth} px");
                Console.WriteLine($"Expected Height: {expectedHeight} px, Actual Height: {actualHeight} px");

                // Determine whether the dimensions match within a small tolerance
                bool match = Math.Abs(actualWidth - expectedWidth) < 0.1 && Math.Abs(actualHeight - expectedHeight) < 0.1;
                Console.WriteLine($"Dimensions match: {match}");
            }
        }
    }
}