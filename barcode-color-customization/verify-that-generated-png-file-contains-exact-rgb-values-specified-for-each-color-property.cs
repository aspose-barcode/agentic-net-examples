using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors,
/// saving it as a PNG, and verifying that only the expected colors are present.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, saves it, and validates its pixel colors.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define output file path and expected colors for the barcode and background.
        // --------------------------------------------------------------------
        string outputPath = "barcode.png";

        // Expected bar (foreground) color: solid red.
        Color expectedBarColor = Color.FromArgb(255, 0, 0);
        // Expected background color: solid white.
        Color expectedBackColor = Color.FromArgb(255, 255, 255);

        // --------------------------------------------------------------------
        // Generate the barcode with the specified colors and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply the custom colors to the generator parameters.
            generator.Parameters.Barcode.BarColor = expectedBarColor;
            generator.Parameters.BackColor = expectedBackColor;

            // Save the generated barcode image to the defined path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Load the image and inspect each pixel to ensure only the expected colors appear.
        // --------------------------------------------------------------------
        bool barColorFound = false;      // Tracks if at least one bar pixel is found.
        bool backColorFound = false;     // Tracks if at least one background pixel is found.
        bool mismatchFound = false;      // Tracks if any unexpected color is encountered.

        using (var bitmap = new Bitmap(outputPath))
        {
            // Iterate over every pixel in the bitmap.
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // Retrieve the color of the current pixel.
                    Color pixel = bitmap.GetPixel(x, y);

                    // Compare the pixel's ARGB value with the expected colors.
                    if (pixel.ToArgb() == expectedBarColor.ToArgb())
                    {
                        barColorFound = true;
                    }
                    else if (pixel.ToArgb() == expectedBackColor.ToArgb())
                    {
                        backColorFound = true;
                    }
                    else
                    {
                        // An unexpected color was found; record the mismatch and exit loops.
                        mismatchFound = true;
                        Console.WriteLine($"Pixel at ({x},{y}) has unexpected color ARGB={pixel.ToArgb():X8}.");
                        break;
                    }
                }

                // Break outer loop if a mismatch was already detected.
                if (mismatchFound) break;
            }
        }

        // --------------------------------------------------------------------
        // Output verification results based on the flags set during pixel inspection.
        // --------------------------------------------------------------------
        if (mismatchFound)
        {
            Console.WriteLine("Verification failed: unexpected colors detected.");
        }
        else if (!barColorFound)
        {
            Console.WriteLine("Verification failed: bar color not found in the image.");
        }
        else if (!backColorFound)
        {
            Console.WriteLine("Verification failed: background color not found in the image.");
        }
        else
        {
            Console.WriteLine("Verification passed: image contains only the specified bar and background colors.");
        }
    }
}