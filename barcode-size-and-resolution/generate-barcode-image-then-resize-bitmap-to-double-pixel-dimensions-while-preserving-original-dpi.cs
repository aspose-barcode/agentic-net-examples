// Title: Generate and Resize Barcode Image
// Description: Creates a Code128 barcode, saves the original image, then doubles its pixel dimensions while preserving the original DPI.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Drawing image manipulation category. It demonstrates how to use BarcodeGenerator to produce a barcode, then employs Aspose.Drawing's Bitmap and Graphics classes to resize the image. Developers often need to adjust barcode image sizes for printing or UI display while maintaining DPI for accurate physical dimensions.
// Prompt: Generate barcode image, then resize bitmap to double pixel dimensions while preserving original DPI.
// Tags: barcode, code128, resize, bitmap, dpi, aspose.barcode, aspose.drawing, png

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

namespace BarcodeResizeDemo
{
    /// <summary>
    /// Demonstrates generating a barcode image and resizing it while preserving DPI.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates a Code128 barcode, saves the original,
        /// creates a double‑size bitmap, and saves the resized image.
        /// </summary>
        static void Main()
        {
            // Initialize a barcode generator for Code128 with sample text "123456"
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Generate the barcode as a Bitmap object
                using (Bitmap original = generator.GenerateBarCodeImage())
                {
                    // Store the original DPI values to apply them later
                    float dpiX = original.HorizontalResolution;
                    float dpiY = original.VerticalResolution;

                    // Compute new dimensions: double the width and height in pixels
                    int newWidth = original.Width * 2;
                    int newHeight = original.Height * 2;

                    // Create a new bitmap with the doubled size and the same pixel format as the original
                    using (Bitmap resized = new Bitmap(newWidth, newHeight, original.PixelFormat))
                    {
                        // Apply the original DPI to the resized bitmap to keep physical size consistent
                        resized.SetResolution(dpiX, dpiY);

                        // Use a Graphics object to draw the original image onto the resized bitmap with high‑quality scaling
                        using (Graphics graphics = Graphics.FromImage(resized))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(original, new Rectangle(0, 0, newWidth, newHeight));
                        }

                        // Save the resized bitmap as a PNG file
                        resized.Save("resized.png", ImageFormat.Png);
                    }

                    // Optionally, save the original barcode image for comparison
                    original.Save("original.png", ImageFormat.Png);
                }
            }
        }
    }
}