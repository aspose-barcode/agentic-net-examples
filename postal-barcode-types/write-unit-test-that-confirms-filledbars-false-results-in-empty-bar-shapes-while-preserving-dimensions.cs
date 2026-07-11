// Title: Demonstrate FilledBars property effect on barcode image
// Description: Shows how setting FilledBars to false creates empty bar shapes while keeping image dimensions unchanged.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters, and the FilledBars property to control bar rendering. Developers often need to generate barcodes with transparent or outline-only bars for design or overlay purposes; this snippet demonstrates measuring pixel counts to verify the effect.
// Prompt: Write a unit test that confirms FilledBars false results in empty bar shapes while preserving dimensions.
// Tags: code128, filledbars, image, barcodegenerator, bitmap, pixelcount, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates two Code128 barcodes—one with filled bars and one with empty bars—
/// and compares their dimensions and pixel counts to verify the FilledBars property behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Counts the number of pixels in the bitmap that match the specified target color.
    /// </summary>
    /// <param name="bitmap">The bitmap to scan.</param>
    /// <param name="targetColor">The color to count.</param>
    /// <returns>The total count of matching pixels.</returns>
    static int CountColorPixels(Bitmap bitmap, Color targetColor)
    {
        int count = 0;
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                // Compare ARGB values for exact color match
                if (bitmap.GetPixel(x, y).ToArgb() == targetColor.ToArgb())
                    count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Generates two barcode images (filled and empty bars), compares their dimensions,
    /// and validates that the empty‑bars image contains fewer black pixels while preserving size.
    /// </summary>
    static void Main()
    {
        // Common barcode configuration
        const string codeText = "1234567890";
        const int imageWidth = 300;
        const int imageHeight = 150;
        const float barHeight = 50f;

        // ------------------------------------------------------------
        // Generate barcode with filled bars (default behavior)
        // ------------------------------------------------------------
        using (var generatorFilled = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generatorFilled.Parameters.AutoSizeMode = AutoSizeMode.None;
            generatorFilled.Parameters.ImageWidth.Point = imageWidth;
            generatorFilled.Parameters.ImageHeight.Point = imageHeight;
            generatorFilled.Parameters.Barcode.BarHeight.Point = barHeight;
            generatorFilled.Parameters.Barcode.FilledBars = true; // explicit for clarity

            using (Bitmap bitmapFilled = generatorFilled.GenerateBarCodeImage())
            {
                // Capture dimensions of the filled‑bars image
                int widthFilled = bitmapFilled.Width;
                int heightFilled = bitmapFilled.Height;

                // Count black pixels representing the filled bars
                int blackPixelsFilled = CountColorPixels(bitmapFilled, Aspose.Drawing.Color.Black);

                // ------------------------------------------------------------
                // Generate barcode with empty bars (FilledBars = false)
                // ------------------------------------------------------------
                using (var generatorEmpty = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    generatorEmpty.Parameters.AutoSizeMode = AutoSizeMode.None;
                    generatorEmpty.Parameters.ImageWidth.Point = imageWidth;
                    generatorEmpty.Parameters.ImageHeight.Point = imageHeight;
                    generatorEmpty.Parameters.Barcode.BarHeight.Point = barHeight;
                    generatorEmpty.Parameters.Barcode.FilledBars = false;

                    using (Bitmap bitmapEmpty = generatorEmpty.GenerateBarCodeImage())
                    {
                        // Capture dimensions of the empty‑bars image
                        int widthEmpty = bitmapEmpty.Width;
                        int heightEmpty = bitmapEmpty.Height;

                        // Count black pixels representing the outline of the bars
                        int blackPixelsEmpty = CountColorPixels(bitmapEmpty, Aspose.Drawing.Color.Black);

                        // Verify that both images share the same dimensions
                        bool dimensionsMatch = widthFilled == widthEmpty && heightFilled == heightEmpty;

                        // Verify that the empty‑bars image has fewer (or zero) black pixels
                        bool fewerBlackPixels = blackPixelsEmpty < blackPixelsFilled;

                        // Output diagnostic information
                        Console.WriteLine($"Filled bars image size: {widthFilled}x{heightFilled}, black pixels: {blackPixelsFilled}");
                        Console.WriteLine($"Empty bars image size: {widthEmpty}x{heightEmpty}, black pixels: {blackPixelsEmpty}");
                        Console.WriteLine($"Dimensions preserved: {(dimensionsMatch ? "PASS" : "FAIL")}");
                        Console.WriteLine($"Empty bars have fewer black pixels: {(fewerBlackPixels ? "PASS" : "FAIL")}");

                        // Final result message
                        if (!dimensionsMatch || !fewerBlackPixels)
                        {
                            Console.WriteLine("FAILED: FilledBars property did not behave as expected.");
                        }
                        else
                        {
                            Console.WriteLine("SUCCESS: FilledBars false results in empty bar shapes while preserving dimensions.");
                        }
                    }
                }
            }
        }
    }
}