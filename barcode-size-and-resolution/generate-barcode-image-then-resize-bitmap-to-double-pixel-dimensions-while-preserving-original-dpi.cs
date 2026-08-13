// Title: Generate and resize a barcode image while preserving DPI
// Description: This example creates a Code128 barcode, saves it, then doubles its pixel dimensions without altering the original DPI.
// Category-Description: Demonstrates Aspose.BarCode image generation and manipulation using Aspose.Drawing. It covers barcode generation (BarcodeGenerator), bitmap handling (Bitmap), and DPI preservation—common tasks for developers needing high‑resolution barcode graphics for printing or UI scaling. Suitable for searches about barcode image resizing with Aspose.
// Prompt: Generate barcode image, then resize bitmap to double pixel dimensions while preserving original DPI.
// Tags: barcode, code128, image-resize, dpi-preservation, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, saves the original image,
/// then creates a resized version with double the pixel dimensions while keeping the original DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the original image,
    /// resizes it, and saves the resized version.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original and resized barcode images.
        string originalPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_original.png");
        string resizedPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_resized.png");

        // Initialize a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure image size using interpolation mode for smoother scaling.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Generate the barcode image as a bitmap.
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Save the original bitmap to PNG.
                originalBitmap.Save(originalPath, ImageFormat.Png);

                // Compute new dimensions: double the width and height in pixels.
                int newWidth = originalBitmap.Width * 2;
                int newHeight = originalBitmap.Height * 2;

                // Create a new bitmap with the doubled dimensions.
                using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Preserve the original DPI (resolution) on the new bitmap.
                    resizedBitmap.SetResolution(originalBitmap.HorizontalResolution, originalBitmap.VerticalResolution);

                    // Draw the original image onto the new bitmap, scaling it to the new size.
                    using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
                    }

                    // Save the resized bitmap to PNG.
                    resizedBitmap.Save(resizedPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the saved images.
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Resized barcode saved to: {resizedPath}");
    }
}