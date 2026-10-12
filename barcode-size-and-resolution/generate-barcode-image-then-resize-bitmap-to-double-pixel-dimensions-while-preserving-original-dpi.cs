// Title: Generate Code128 barcode and double its pixel dimensions while preserving DPI
// Description: Creates a Code128 barcode image, saves it, then produces a resized version with twice the pixel width and height without changing the original DPI.
// Category-Description: This example belongs to the Aspose.BarCode generation and image processing category. It demonstrates using BarcodeGenerator (Aspose.BarCode.Generation) to create a barcode, and Aspose.Drawing (Bitmap, Graphics) to manipulate the resulting image. Typical scenarios include preparing barcodes for high‑resolution printing or scaling them for different display requirements, where developers need to maintain DPI consistency while adjusting pixel size.
// Prompt: Generate barcode image, then resize bitmap to double pixel dimensions while preserving original DPI.
// Tags: code128, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving the original image,
/// resizing it to double the pixel dimensions while preserving DPI,
/// and saving the resized image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation and image resizing.
    /// </summary>
    static void Main()
    {
        // Define output directory and file paths for original and resized images
        string outputDir = Directory.GetCurrentDirectory();
        string originalPath = Path.Combine(outputDir, "original.png");
        string resizedPath = Path.Combine(outputDir, "resized.png");

        // Initialize the barcode generator with Code128 symbology and data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a high resolution (300 DPI) for better image quality
            generator.Parameters.Resolution = 300f;

            // Generate the barcode as a bitmap image
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Save the original barcode image to PNG format
                originalBitmap.Save(originalPath, ImageFormat.Png);

                // Capture original dimensions and DPI values
                int originalWidth = originalBitmap.Width;
                int originalHeight = originalBitmap.Height;
                float dpiX = originalBitmap.HorizontalResolution;
                float dpiY = originalBitmap.VerticalResolution;
                PixelFormat pixelFormat = originalBitmap.PixelFormat;

                // Calculate new dimensions (double the width and height)
                int newWidth = originalWidth * 2;
                int newHeight = originalHeight * 2;

                // Create a new bitmap with the enlarged dimensions and same pixel format
                using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight, pixelFormat))
                {
                    // Preserve the original DPI on the resized bitmap
                    resizedBitmap.SetResolution(dpiX, dpiY);

                    // Draw the original image onto the resized bitmap, scaling it to fit
                    using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.DrawImage(
                            originalBitmap,
                            new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Save the resized barcode image to PNG format
                    resizedBitmap.Save(resizedPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the saved images
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Resized barcode saved to: {resizedPath}");
    }
}