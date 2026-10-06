// Title: Retrieve and Verify Barcode Image Dimensions
// Description: Demonstrates how to generate a barcode image with specific dimensions and programmatically verify that the actual image size matches the requested width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, its Parameters, and image handling classes such as Bitmap and ImageFormat. Developers often need to control output image size for UI layout, printing, or further processing, and must confirm that the generated image respects the specified ImageWidth and ImageHeight settings.
// Prompt: Programmatically retrieve the generated barcode image dimensions to confirm they match the specified ImageWidth and ImageHeight.
// Tags: barcode, code128, image size, verification, aspose.barcode, generation, bitmap, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with size verification using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves it, and checks that the bitmap dimensions match the requested size.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology.
        string codeText = "ASPOSE";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Expected image dimensions in pixels.
        int expectedWidth = 300;
        int expectedHeight = 200;

        // Create a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Initialize the barcode generator with the chosen symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure the generator to use the nearest auto‑size mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set the desired image width and height.
            generator.Parameters.ImageWidth.Pixels = expectedWidth;
            generator.Parameters.ImageHeight.Pixels = expectedHeight;

            // Generate the barcode as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a file for optional visual verification.
                using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(stream, ImageFormat.Png);
                }

                // Retrieve the actual dimensions of the generated bitmap.
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Output the expected vs. actual dimensions.
                Console.WriteLine($"Expected Width: {expectedWidth}px, Actual Width: {actualWidth}px");
                Console.WriteLine($"Expected Height: {expectedHeight}px, Actual Height: {actualHeight}px");

                // Verify that the dimensions match the specified values.
                if (actualWidth == expectedWidth && actualHeight == expectedHeight)
                {
                    Console.WriteLine("Image dimensions match the specified ImageWidth and ImageHeight.");
                }
                else
                {
                    Console.WriteLine("Image dimensions do NOT match the specified values.");
                }
            }
        }
    }
}