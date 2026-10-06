// Title: Generate QR Code with Horizontal Gradient Background
// Description: Creates a QR barcode and applies a horizontal gradient background using two colors, then saves the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with graphics operations. It uses the BarcodeGenerator class to create a QR code, Aspose.Drawing Bitmap and Graphics to draw a color gradient, and overlays the barcode onto the gradient. Developers often need to enhance visual appeal of barcodes for marketing materials, packaging, or UI displays, and this pattern shows a typical workflow for such customizations.
// Prompt: Apply a gradient background using two colors to create a visually appealing barcode.
// Tags: qr code, gradient background, barcode generation, aspose.barcode, image processing, png output, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR barcode with a custom horizontal gradient background and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates the gradient, overlays the barcode, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_gradient.png");

        // Define the start and end colors for the horizontal gradient (blue to cyan).
        Color startColor = Color.FromArgb(255, 0, 0, 255); // Blue
        Color endColor = Color.FromArgb(255, 0, 255, 255); // Cyan

        // Initialize the barcode generator for a QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose Gradient"))
        {
            // Set the barcode's foreground color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Generate the barcode image as a bitmap.
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                int width = barcodeBmp.Width;
                int height = barcodeBmp.Height;

                // Create a new bitmap that will hold the gradient background.
                using (Bitmap gradientBmp = new Bitmap(width, height))
                {
                    // Obtain a graphics object to draw on the gradient bitmap.
                    using (Graphics gfx = Graphics.FromImage(gradientBmp))
                    {
                        // Draw a horizontal gradient by iterating over each column.
                        for (int x = 0; x < width; x++)
                        {
                            // Calculate the interpolation ratio for the current column.
                            float ratio = (float)x / (width - 1);

                            // Interpolate each RGB component between the start and end colors.
                            int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
                            int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
                            int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);
                            Color col = Color.FromArgb(255, r, g, b);

                            // Draw a vertical line with the interpolated color.
                            using (Pen pen = new Pen(col))
                            {
                                gfx.DrawLine(pen, x, 0, x, height);
                            }
                        }

                        // Overlay the generated barcode onto the gradient background.
                        gfx.DrawImage(barcodeBmp, 0, 0, width, height);
                    }

                    // Save the final image as a PNG file.
                    gradientBmp.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode with gradient saved to: {outputPath}");
    }
}