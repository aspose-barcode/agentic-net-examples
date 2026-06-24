using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a specific physical width
/// and validates the resulting pixel dimensions against an expected value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a memory stream, and checks its pixel width.
    /// </summary>
    static void Main()
    {
        // Desired barcode width in millimeters.
        const float targetWidthMm = 20f;
        // Desired image resolution in dots per inch.
        const float resolutionDpi = 96f;

        // Convert target width from millimeters to inches.
        double inches = targetWidthMm / 25.4;
        // Compute expected pixel width based on resolution, rounding to nearest integer.
        int expectedPixels = (int)Math.Round(inches * resolutionDpi);

        // Create a barcode generator for Code128 with the data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply the desired resolution (DPI) to the generator.
            generator.Parameters.Resolution = resolutionDpi;
            // Set the image width to the target width in millimeters; height is auto‑calculated.
            generator.Parameters.ImageWidth.Millimeters = targetWidthMm;
            // Disable automatic sizing so the explicit width is respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                ms.Position = 0;

                // Load the image from the memory stream to inspect its dimensions.
                using (var image = Image.FromStream(ms))
                {
                    int actualWidth = image.Width;   // Width in pixels.
                    int actualHeight = image.Height; // Height in pixels.

                    // Output expected and actual dimensions.
                    Console.WriteLine($"Expected width (pixels): {expectedPixels}");
                    Console.WriteLine($"Actual width (pixels):   {actualWidth}");
                    Console.WriteLine($"Actual height (pixels):  {actualHeight}");

                    // Validate whether the actual width matches the expected pixel width.
                    if (actualWidth == expectedPixels)
                    {
                        Console.WriteLine("Validation succeeded: pixel width matches expected value.");
                    }
                    else
                    {
                        Console.WriteLine("Validation failed: pixel width does not match expected value.");
                    }
                }
            }
        }
    }
}