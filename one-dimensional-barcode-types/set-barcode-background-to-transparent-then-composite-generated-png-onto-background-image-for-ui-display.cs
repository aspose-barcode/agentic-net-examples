// Title: Generate Transparent Barcode and Composite onto Background Image
// Description: Demonstrates creating a Code128 barcode with a transparent background, then compositing it onto a solid‑color canvas and saving as PNG for UI display.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to use BarcodeGenerator, set visual parameters such as BackColor, and combine the generated bitmap with a custom background using Aspose.Drawing. Typical use cases include preparing barcode graphics for UI components, reports, or web pages where a transparent barcode must be overlaid on a design element. Developers often need to control colors, image formats, and compositing to integrate barcodes seamlessly into applications.
// Prompt: Set barcode background to transparent, then composite the generated PNG onto a background image for UI display.
// Tags: code128, barcode, transparent background, composite image, png, aspose.barcode, aspose.drawing, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with a transparent background,
/// composites it onto a light‑gray canvas, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, composites it, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode background to transparent and the bar color to black.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Generate the barcode image as a bitmap.
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Determine the size of the final canvas, ensuring a minimum size.
                int canvasWidth = Math.Max(barcodeBmp.Width + 20, 300);
                int canvasHeight = Math.Max(barcodeBmp.Height + 20, 200);

                // Create a new bitmap that will serve as the background canvas.
                using (var backgroundBmp = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
                {
                    // Obtain a graphics object to draw on the background bitmap.
                    using (var graphics = Graphics.FromImage(backgroundBmp))
                    {
                        // Fill the canvas with a light‑gray solid color.
                        graphics.Clear(Aspose.Drawing.Color.LightGray);

                        // Calculate coordinates to center the barcode on the canvas.
                        int x = (canvasWidth - barcodeBmp.Width) / 2;
                        int y = (canvasHeight - barcodeBmp.Height) / 2;

                        // Draw the barcode bitmap onto the background at the calculated position.
                        graphics.DrawImage(barcodeBmp, new Point(x, y));
                    }

                    // Build the full output path for the composite image.
                    string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "CompositeBarcode.png");

                    // Save the composite bitmap as a PNG file.
                    backgroundBmp.Save(outputPath, ImageFormat.Png);

                    // Inform the user where the file was saved.
                    Console.WriteLine($"Composite barcode saved to {outputPath}");
                }
            }
        }
    }
}