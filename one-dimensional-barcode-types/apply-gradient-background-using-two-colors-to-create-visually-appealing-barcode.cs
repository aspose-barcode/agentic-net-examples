// Title: Apply a gradient background to a Code128 barcode image
// Description: Demonstrates generating a Code128 barcode and overlaying it on a vertical gradient background, then saving as PNG.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, showcasing how to combine barcode generation (BarcodeGenerator) with custom graphics (Bitmap, Graphics) to create visually enhanced barcodes. Typical use cases include branding, marketing materials, and UI elements where a plain barcode needs a styled background. Developers often need to render barcodes onto custom canvases, apply gradients, and export to common image formats.
// Prompt: Apply a gradient background using two colors to create a visually appealing barcode.
// Tags: code128, gradient-background, png, barcodelibrary, bitmap, graphics

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode and places it on a vertical gradient background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, draws a gradient, composites the images, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        const string codeText = "Gradient123";
        var encodeType = EncodeTypes.Code128;

        // Create a barcode generator with the specified symbology and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Generate the barcode image as a bitmap
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                int width = barcodeImage.Width;
                int height = barcodeImage.Height;

                // Create a new bitmap that will hold the gradient background
                using (var gradientBitmap = new Bitmap(width, height))
                {
                    // Define start and end colors for the vertical gradient
                    var startColor = Color.LightBlue;
                    var endColor = Color.LightGreen;

                    // Obtain a Graphics object to draw on the gradient bitmap
                    using (var graphics = Graphics.FromImage(gradientBitmap))
                    {
                        // Fill the bitmap line by line to create a smooth vertical gradient
                        for (int y = 0; y < height; y++)
                        {
                            float ratio = (float)y / (height - 1);
                            int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
                            int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
                            int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);
                            var lineColor = Color.FromArgb(r, g, b);
                            var rect = new Rectangle(0, y, width, 1);
                            using (var brush = new SolidBrush(lineColor))
                            {
                                graphics.FillRectangle(brush, rect);
                            }
                        }

                        // Draw the generated barcode on top of the gradient background
                        graphics.DrawImage(barcodeImage, 0, 0);
                    }

                    // Save the composited image to a PNG file
                    const string outputPath = "gradient_barcode.png";
                    gradientBitmap.Save(outputPath, ImageFormat.Png);
                    Console.WriteLine($"Barcode with gradient background saved to: {outputPath}");
                }
            }
        }
    }
}