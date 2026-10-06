// Title: Generate Barcode with Transparent Background for Image Overlay
// Description: Creates a Code128 barcode with a transparent background and overlays it onto a base image, then saves the result as a PNG.
// Category-Description: This example demonstrates Aspose.BarCode image generation and manipulation, focusing on setting a transparent background for barcodes and compositing them onto existing graphics. It uses BarcodeGenerator, BarcodeParameters, and Aspose.Drawing classes such as Bitmap, Graphics, and ImageFormat. Typical use cases include adding barcodes to photos, PDFs, or UI elements without obscuring the underlying content. Developers often need to control barcode colors, background transparency, and positioning when integrating barcodes into custom visuals.
// Prompt: Configure barcode to use a transparent background for overlay on existing images.
// Tags: code128, transparent background, image overlay, png, aspose.barcode, aspose.drawing, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with a transparent background and overlaying it onto a base image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the base image, generates the barcode, overlays it, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final PNG image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_overlay.png");

        // Create a base bitmap (400x300) with 32bpp ARGB pixel format to support transparency.
        using (var baseBitmap = new Bitmap(400, 300, PixelFormat.Format32bppArgb))
        {
            // Fill the base image with a solid gray background.
            using (var graphicsBase = Graphics.FromImage(baseBitmap))
            {
                graphicsBase.Clear(Color.FromArgb(200, 200, 200));
            }

            // Initialize the barcode generator for Code128 with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set the barcode background to transparent and the bar color to black.
                generator.Parameters.BackColor = Color.Transparent;
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Generate the barcode image as a bitmap.
                using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
                {
                    // Overlay the barcode bitmap onto the base image at position (50,50).
                    using (var graphicsOverlay = Graphics.FromImage(baseBitmap))
                    {
                        graphicsOverlay.DrawImage(
                            barcodeBitmap,
                            new Rectangle(50, 50, barcodeBitmap.Width, barcodeBitmap.Height));
                    }
                }
            }

            // Save the combined image as a PNG file, preserving transparency.
            baseBitmap.Save(outputPath, ImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode overlay image saved to: {outputPath}");
    }
}