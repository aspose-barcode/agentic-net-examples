// Title: Barcode AutoSizeMode Selection Based on Image Dimensions
// Description: Demonstrates how to choose the AutoSizeMode (Interpolation or Nearest) for a barcode image according to its width and height, then generate and save the barcode.
// Prompt: Develop a function that switches AutoSizeMode between Interpolation and Nearest based on user‑selected image dimensions.
// Tags: barcode, autosizemode, interpolation, nearest, image dimensions, aspose.barcode, c#
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that selects an appropriate <see cref="AutoSizeMode"/> for a barcode
/// based on the provided image dimensions and generates the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Determines <see cref="AutoSizeMode"/> based on image dimensions.
    /// If width is greater than or equal to height, returns <see cref="AutoSizeMode.Interpolation"/>;
    /// otherwise, returns <see cref="AutoSizeMode.Nearest"/>.
    /// </summary>
    /// <param name="width">Image width in pixels.</param>
    /// <param name="height">Image height in pixels.</param>
    /// <returns>Chosen <see cref="AutoSizeMode"/>.</returns>
    static AutoSizeMode DetermineAutoSizeMode(int width, int height)
    {
        // Use Interpolation when the image is landscape or square; otherwise use Nearest.
        return width >= height ? AutoSizeMode.Interpolation : AutoSizeMode.Nearest;
    }

    /// <summary>
    /// Entry point. Generates a barcode using the selected <see cref="AutoSizeMode"/>
    /// and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample image dimensions (replace with actual user input as needed).
        int imageWidth = 300;   // pixels
        int imageHeight = 150;  // pixels

        // Choose the appropriate AutoSizeMode based on dimensions.
        AutoSizeMode mode = DetermineAutoSizeMode(imageWidth, imageHeight);

        // Create a barcode generator for Code128 with sample codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply the selected AutoSizeMode.
            generator.Parameters.AutoSizeMode = mode;

            // Set the target image size using point units.
            generator.Parameters.ImageWidth.Point = (float)imageWidth;
            generator.Parameters.ImageHeight.Point = (float)imageHeight;

            // Optional visual settings.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Resolution = 96; // DPI

            // Determine output file name based on the chosen mode.
            string fileName = mode == AutoSizeMode.Interpolation
                ? "barcode_interpolation.png"
                : "barcode_nearest.png";

            // Save the barcode image to disk.
            generator.Save(fileName);
            Console.WriteLine($"Barcode saved as {fileName} with AutoSizeMode {mode}");
        }
    }
}