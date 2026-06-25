using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to composite a barcode image onto a background image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a background image if missing,
    /// generates a barcode with a transparent background, composites the barcode
    /// onto the background, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the background and the final composited image.
        string backgroundPath = "background.png";
        string outputPath = "barcode_composite.png";

        // If the background image does not exist, create a simple placeholder.
        if (!File.Exists(backgroundPath))
        {
            const int bgWidth = 400;   // Width of the placeholder background.
            const int bgHeight = 200;  // Height of the placeholder background.

            // Create a bitmap, fill it with a light gray color, and save as PNG.
            using (var bgBitmap = new Bitmap(bgWidth, bgHeight))
            {
                using (var g = Graphics.FromImage(bgBitmap))
                {
                    g.Clear(Color.LightGray);
                }
                bgBitmap.Save(backgroundPath, ImageFormat.Png);
            }
        }

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode background to be fully transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Generate the barcode image as a bitmap.
            using (var barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Load the background image from disk.
                using (var backgroundBitmap = new Bitmap(backgroundPath))
                {
                    // Create a new bitmap that will hold the final composited image.
                    using (var resultBitmap = new Bitmap(backgroundBitmap.Width, backgroundBitmap.Height))
                    {
                        // Obtain a graphics object to draw onto the result bitmap.
                        using (var graphics = Graphics.FromImage(resultBitmap))
                        {
                            // Draw the background image onto the result bitmap.
                            graphics.DrawImage(backgroundBitmap, 0, 0, backgroundBitmap.Width, backgroundBitmap.Height);

                            // Calculate coordinates to center the barcode on the background.
                            int x = (backgroundBitmap.Width - barcodeBitmap.Width) / 2;
                            int y = (backgroundBitmap.Height - barcodeBitmap.Height) / 2;

                            // Draw the barcode image onto the result bitmap at the calculated position.
                            graphics.DrawImage(barcodeBitmap, x, y, barcodeBitmap.Width, barcodeBitmap.Height);
                        }

                        // Save the composited image to the specified output path in PNG format.
                        resultBitmap.Save(outputPath, ImageFormat.Png);
                    }
                }
            }
        }

        // Output the full path of the saved composited image to the console.
        Console.WriteLine($"Composite image saved to: {Path.GetFullPath(outputPath)}");
    }
}