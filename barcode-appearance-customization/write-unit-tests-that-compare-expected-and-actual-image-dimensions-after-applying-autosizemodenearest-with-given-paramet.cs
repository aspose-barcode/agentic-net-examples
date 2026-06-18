using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using System.IO;

/// <summary>
/// Demonstrates how to generate a barcode with target dimensions and verify the resulting image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes a series of test cases with different target dimensions.
    /// </summary>
    static void Main()
    {
        // Test case 1: target size 200x100 points
        RunTest(200f, 100f);
        // Test case 2: target size 300x150 points
        RunTest(300f, 150f);
        // Test case 3: target size 250x120 points
        RunTest(250f, 120f);
    }

    /// <summary>
    /// Generates a barcode with the specified target width and height (in points),
    /// saves it to a memory stream, loads it back, and prints the actual pixel dimensions.
    /// </summary>
    /// <param name="targetWidthPoints">Desired image width in points.</param>
    /// <param name="targetHeightPoints">Desired image height in points.</param>
    static void RunTest(float targetWidthPoints, float targetHeightPoints)
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Configure the generator to choose the nearest size that fits the target dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Apply the target dimensions (points) to the generator parameters.
            generator.Parameters.ImageWidth.Point = targetWidthPoints;
            generator.Parameters.ImageHeight.Point = targetHeightPoints;

            // Save the generated barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the memory stream to inspect its actual pixel size.
                using (var image = (Bitmap)Image.FromStream(ms))
                {
                    int actualWidth = image.Width;   // Width in pixels.
                    int actualHeight = image.Height; // Height in pixels.

                    // Verify that the actual dimensions do not exceed the target dimensions.
                    bool widthOk = actualWidth <= (int)targetWidthPoints;
                    bool heightOk = actualHeight <= (int)targetHeightPoints;

                    // Output the comparison results to the console.
                    Console.WriteLine(
                        $"Target (pt): {targetWidthPoints}x{targetHeightPoints} | " +
                        $"Actual (px): {actualWidth}x{actualHeight} | " +
                        $"Width OK: {widthOk} | Height OK: {heightOk}");
                }
            }
        }
    }
}