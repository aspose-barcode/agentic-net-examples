using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom vertical gradient background
/// and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, applies a gradient background,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Output file path and barcode content
        const string outputPath = "gradient_barcode.png";
        const string codeText = "1234567890";

        // Initialize the barcode generator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the background to transparent so the custom gradient will be visible
            generator.Parameters.BackColor = Color.Transparent;

            // Generate the barcode image as a bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                int width = barcodeBitmap.Width;
                int height = barcodeBitmap.Height;

                // Create a new bitmap that will hold the gradient background and the barcode
                using (var finalBitmap = new Bitmap(width, height))
                {
                    // Obtain a graphics object for drawing onto the final bitmap
                    using (var graphics = Graphics.FromImage(finalBitmap))
                    {
                        // Define the start and end colors of the vertical gradient
                        Color startColor = Color.LightBlue;
                        Color endColor = Color.LightGreen;

                        // Fill the background with a vertical gradient, one pixel row at a time
                        for (int y = 0; y < height; y++)
                        {
                            // Compute interpolation ratio based on current row
                            float ratio = (float)y / (height - 1);

                            // Interpolate each RGB component separately
                            int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
                            int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
                            int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);

                            // Create a solid brush with the interpolated color
                            using (var brush = new SolidBrush(Color.FromArgb(r, g, b)))
                            {
                                // Draw a one‑pixel‑high rectangle across the entire width
                                graphics.FillRectangle(brush, 0, y, width, 1);
                            }
                        }

                        // Draw the generated barcode on top of the gradient background
                        graphics.DrawImage(barcodeBitmap, 0, 0, width, height);
                    }

                    // Save the composed image as a PNG file
                    finalBitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode with gradient background saved to: {outputPath}");
    }
}