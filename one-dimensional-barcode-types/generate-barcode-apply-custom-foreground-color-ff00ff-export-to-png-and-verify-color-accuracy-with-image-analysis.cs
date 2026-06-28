using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom magenta foreground color,
/// saving it as a PNG file, and verifying the bar color in the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, saves it, and validates the foreground color.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with the data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the bar (foreground) color to magenta (#FF00FF).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.FromArgb(255, 255, 0, 255);

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Load the saved PNG image for pixel-level analysis.
        using (var bitmap = new Bitmap(outputPath))
        {
            // Define the expected bar color (magenta) for comparison.
            Aspose.Drawing.Color expectedBarColor = Aspose.Drawing.Color.FromArgb(255, 255, 0, 255);
            bool foundBarPixel = false;   // Tracks if any non‑background pixel is found.
            bool colorMatches = true;     // Tracks if all bar pixels match the expected color.

            // Iterate over each pixel in the image until a mismatch is found.
            for (int y = 0; y < bitmap.Height && colorMatches; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Aspose.Drawing.Color pixelColor = bitmap.GetPixel(x, y);

                    // Skip white background pixels.
                    if (pixelColor.ToArgb() != Aspose.Drawing.Color.White.ToArgb())
                    {
                        foundBarPixel = true;

                        // If a bar pixel does not match the expected magenta, mark mismatch.
                        if (pixelColor.ToArgb() != expectedBarColor.ToArgb())
                        {
                            colorMatches = false;
                            break;
                        }
                    }
                }
            }

            // Output verification results based on the analysis.
            if (!foundBarPixel)
            {
                Console.WriteLine("No barcode bars detected in the image.");
            }
            else if (colorMatches)
            {
                Console.WriteLine("Foreground color verification succeeded: bars are magenta.");
            }
            else
            {
                Console.WriteLine("Foreground color verification failed: bar color does not match expected magenta.");
            }
        }
    }
}