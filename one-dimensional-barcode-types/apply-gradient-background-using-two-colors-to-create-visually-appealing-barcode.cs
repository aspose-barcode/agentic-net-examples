// Title: Generate QR Code with Gradient Background
// Description: Demonstrates how to apply a vertical gradient background to a QR code barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class together with System.Drawing (Aspose.Drawing) objects. Typical use cases include branding, visual enhancement, and integrating barcodes into UI designs where background styling is required. Developers often need to combine barcode generation with graphic manipulation to meet design specifications.
// Prompt: Apply a gradient background using two colors to create a visually appealing barcode.
// Tags: qr code, gradient background, barcode generation, aspose.barcode, png output, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR code barcode with a vertical gradient background
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies a gradient, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "GradientBarcode.png");

        // Create a barcode generator for a QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "GradientDemo"))
        {
            // Set the barcode background to transparent so the gradient will be visible.
            generator.Parameters.BackColor = Color.Transparent;

            // Generate the barcode image as a bitmap.
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap that will hold the gradient and the barcode.
                using (Bitmap finalBitmap = new Bitmap(barcodeBitmap.Width, barcodeBitmap.Height))
                {
                    // Obtain a graphics object for drawing on the final bitmap.
                    using (Graphics graphics = Graphics.FromImage(finalBitmap))
                    {
                        // Define the two colors for the vertical gradient.
                        Color topColor = Color.FromArgb(255, 255, 200, 200);   // Light red
                        Color bottomColor = Color.FromArgb(255, 200, 200, 255); // Light blue

                        int height = finalBitmap.Height;
                        int width = finalBitmap.Width;

                        // Draw the gradient line by line.
                        for (int y = 0; y < height; y++)
                        {
                            // Calculate the interpolation ratio for the current line.
                            float ratio = (float)y / (height - 1);

                            // Interpolate each RGB component between the top and bottom colors.
                            int r = (int)(topColor.R + (bottomColor.R - topColor.R) * ratio);
                            int g = (int)(topColor.G + (bottomColor.G - topColor.G) * ratio);
                            int b = (int)(topColor.B + (bottomColor.B - topColor.B) * ratio);
                            Color lineColor = Color.FromArgb(255, r, g, b);

                            // Fill a one‑pixel‑high rectangle with the interpolated color.
                            using (SolidBrush brush = new SolidBrush(lineColor))
                            {
                                graphics.FillRectangle(brush, 0, y, width, 1);
                            }
                        }

                        // Draw the generated barcode on top of the gradient background.
                        graphics.DrawImage(barcodeBitmap, 0, 0, width, height);
                    }

                    // Save the composed image as a PNG file.
                    finalBitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Gradient barcode saved to: {outputPath}");
    }
}