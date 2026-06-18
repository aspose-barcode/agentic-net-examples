using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes with different colors and verifying that saved images retain their original colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary file paths for the two barcode images.
        // --------------------------------------------------------------------
        string tempDir = Path.GetTempPath();
        string file1 = Path.Combine(tempDir, "barcode1.png");
        string file2 = Path.Combine(tempDir, "barcode2.png");

        // --------------------------------------------------------------------
        // Remove any existing files with the same names to ensure a clean run.
        // --------------------------------------------------------------------
        if (File.Exists(file1)) File.Delete(file1);
        if (File.Exists(file2)) File.Delete(file2);

        // --------------------------------------------------------------------
        // Generate a barcode, save it with an initial color scheme, then
        // change the colors and save a second image.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            // Set initial colors: blue bars on a white background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the first image using the initial colors.
            generator.Save(file1, BarCodeImageFormat.Png);

            // Change colors after the first save: red bars on a yellow background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the second image using the new colors.
            generator.Save(file2, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Load both images and sample a pixel to verify that the first image
        // retained its original colors despite later changes.
        // --------------------------------------------------------------------
        using (var bmp1 = new Bitmap(file1))
        using (var bmp2 = new Bitmap(file2))
        {
            // Choose a pixel location that is likely part of the barcode.
            int x = 10;
            int y = 10;

            // Ensure the coordinates are within the bounds of each bitmap.
            if (x >= bmp1.Width)  x = bmp1.Width / 2;
            if (y >= bmp1.Height) y = bmp1.Height / 2;
            if (x >= bmp2.Width)  x = bmp2.Width / 2;
            if (y >= bmp2.Height) y = bmp2.Height / 2;

            // Retrieve the color of the sampled pixel from each image.
            Color color1 = bmp1.GetPixel(x, y);
            Color color2 = bmp2.GetPixel(x, y);

            // Output the ARGB values of the sampled pixels.
            Console.WriteLine($"Pixel at ({x},{y}) in first image: ARGB = {color1.ToArgb():X8}");
            Console.WriteLine($"Pixel at ({x},{y}) in second image: ARGB = {color2.ToArgb():X8}");

            // Simple verification: the colors should differ, confirming that
            // changes after the first Save do not affect the already saved file.
            if (color1.ToArgb() != color2.ToArgb())
                Console.WriteLine("Colors differ as expected – the first saved image remains unchanged.");
            else
                Console.WriteLine("Colors are the same – unexpected behavior.");
        }
    }
}