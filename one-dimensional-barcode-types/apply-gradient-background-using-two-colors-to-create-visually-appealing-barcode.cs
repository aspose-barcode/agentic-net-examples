// Title: Gradient Background Barcode Example
// Description: Demonstrates applying a vertical gradient background to a Code128 barcode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to customize barcode appearance using Aspose.BarCode.Generation.BarcodeGenerator and Aspose.Drawing graphics. Typical use cases include branding, UI design, and creating visually appealing barcodes for marketing materials. Developers often need to modify background colors, apply gradients, or overlay images while preserving barcode readability.
// Prompt: Apply a gradient background using two colors to create a visually appealing barcode.
// Tags: barcode symbology, gradient background, png output, barcodegenerator, graphics

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with a vertical gradient background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, applies gradient, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Output file path
        const string outputPath = "gradient_barcode.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set barcode bar color
            generator.Parameters.Barcode.BarColor = Color.Black;
            // Make the generator background transparent so we can apply our own gradient
            generator.Parameters.BackColor = Color.Transparent;

            // Generate the barcode image
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap to hold the gradient background plus the barcode
                using (Bitmap finalBmp = new Bitmap(barcodeBmp.Width, barcodeBmp.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(finalBmp))
                    {
                        // Define two colors for the gradient (top to bottom)
                        Color topColor = Color.LightBlue;
                        Color bottomColor = Color.LightCoral;

                        int height = finalBmp.Height;
                        int width = finalBmp.Width;

                        // Draw a simple vertical gradient by interpolating each scan line
                        for (int y = 0; y < height; y++)
                        {
                            float ratio = (float)y / (height - 1);
                            int r = (int)(topColor.R + (bottomColor.R - topColor.R) * ratio);
                            int g = (int)(topColor.G + (bottomColor.G - topColor.G) * ratio);
                            int b = (int)(topColor.B + (bottomColor.B - topColor.B) * ratio);
                            Color lineColor = Color.FromArgb(r, g, b);
                            using (Pen pen = new Pen(lineColor))
                            {
                                graphics.DrawLine(pen, 0, y, width, y);
                            }
                        }

                        // Draw the barcode on top of the gradient background
                        graphics.DrawImage(barcodeBmp, 0, 0, barcodeBmp.Width, barcodeBmp.Height);
                    }

                    // Save the final image as PNG
                    finalBmp.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Barcode with gradient background saved to: {outputPath}");
    }
}