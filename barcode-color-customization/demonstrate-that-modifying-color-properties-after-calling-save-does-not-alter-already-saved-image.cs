// Title: Demonstrate color change after saving barcode does not affect saved image
// Description: Shows that modifying the barcode bar color after calling Save creates a new image file with the new color while the previously saved file remains unchanged.
// Prompt: Demonstrate that modifying color properties after calling Save does not alter the already saved image.
// Tags: barcode, color, save, aspose.barcode, png, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates that changing barcode color after saving does not modify the already saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates two barcode images with different colors and verifies they differ.
    /// </summary>
    static void Main()
    {
        // Define file paths for the two output images
        const string firstImagePath = "barcode_red.png";
        const string secondImagePath = "barcode_blue.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            // Set the barcode bar color to Red and save the first image
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
            generator.Save(firstImagePath);

            // Change the bar color to Blue after the first save and save the second image
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Save(secondImagePath);
        }

        // Load both saved images to verify they are different
        using (var bmpRed = new Bitmap(firstImagePath))
        using (var bmpBlue = new Bitmap(secondImagePath))
        {
            // Compare a pixel that is likely part of the barcode (e.g., at (10,10))
            Color pixelRed = bmpRed.GetPixel(10, 10);
            Color pixelBlue = bmpBlue.GetPixel(10, 10);

            // Determine if the colors differ
            bool colorsAreDifferent = !pixelRed.Equals(pixelBlue);

            // Output pixel color information and comparison result
            Console.WriteLine($"Pixel at (10,10) in first image:  R={pixelRed.R}, G={pixelRed.G}, B={pixelRed.B}");
            Console.WriteLine($"Pixel at (10,10) in second image: R={pixelBlue.R}, G={pixelBlue.G}, B={pixelBlue.B}");
            Console.WriteLine($"Images differ after color change: {colorsAreDifferent}");
        }
    }
}