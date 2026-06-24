using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with specific pixel dimensions
/// and verifies that the generated image matches the expected size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, checks its dimensions, and optionally saves it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the desired barcode image size in pixels.
        const float targetWidth = 300f;
        const float targetHeight = 100f;

        // Build a temporary file path for optional visual inspection.
        string tempPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");

        // Create a barcode generator for Code128 with the sample data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the generator to use interpolation mode so that
            // the ImageWidth and ImageHeight settings are respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Pixels = targetWidth;
            generator.Parameters.ImageHeight.Pixels = targetHeight;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the memory stream to verify its actual dimensions.
                using (var bitmap = new Bitmap(ms))
                {
                    int actualWidth = bitmap.Width;
                    int actualHeight = bitmap.Height;

                    // Output expected vs. actual dimensions.
                    Console.WriteLine($"Expected Width: {targetWidth}, Actual Width: {actualWidth}");
                    Console.WriteLine($"Expected Height: {targetHeight}, Actual Height: {actualHeight}");

                    // Determine whether the generated image matches the target size.
                    bool widthMatches = actualWidth == (int)targetWidth;
                    bool heightMatches = actualHeight == (int)targetHeight;

                    if (widthMatches && heightMatches)
                    {
                        Console.WriteLine("PASS: Barcode dimensions match the specified pixel values.");
                    }
                    else
                    {
                        Console.WriteLine("FAIL: Barcode dimensions do not match the specified pixel values.");
                    }

                    // Optionally save the bitmap to a file for visual inspection.
                    bitmap.Save(tempPath, ImageFormat.Png);
                    Console.WriteLine($"Barcode image saved to: {tempPath}");
                }
            }
        }
    }
}