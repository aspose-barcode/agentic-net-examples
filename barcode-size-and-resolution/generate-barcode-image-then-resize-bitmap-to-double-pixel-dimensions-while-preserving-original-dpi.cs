// Title: Generate and Resize Barcode Image with Preserved DPI
// Description: Creates a Code128 barcode, saves it as PNG, then doubles its pixel dimensions while keeping the original DPI.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation combined with Aspose.Drawing image manipulation. It shows how to use BarcodeGenerator (EncodeTypes) to produce a barcode, then employ Bitmap, Graphics, and SetResolution to resize the image without altering its DPI. Typical scenarios include preparing high‑resolution barcodes for print media, scaling graphics for different device densities, and preserving print quality across size changes.
// Prompt: Generate barcode image, then resize bitmap to double pixel dimensions while preserving original DPI.
// Tags: barcode, code128, resize, dpi, aspose.barcode, aspose.drawing, png, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode image and resizing it while preserving DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves the original image,
    /// creates a resized version with double the pixel dimensions, and saves it.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the original and resized barcode images
        string originalPath = Path.Combine(outputDir, "barcode_original.png");
        string resizedPath = Path.Combine(outputDir, "barcode_resized.png");

        // Initialize the barcode generator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the resolution (DPI) for the generated image
            generator.Parameters.Resolution = 300f;

            // Generate the barcode as a bitmap
            using (var originalBitmap = generator.GenerateBarCodeImage())
            {
                // Save the original barcode image to PNG
                originalBitmap.Save(originalPath, ImageFormat.Png);

                // Calculate new dimensions (double the width and height)
                int newWidth = originalBitmap.Width * 2;
                int newHeight = originalBitmap.Height * 2;

                // Create a new bitmap with the enlarged dimensions
                using (var resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Preserve the original DPI on the resized bitmap
                    resizedBitmap.SetResolution(originalBitmap.HorizontalResolution, originalBitmap.VerticalResolution);

                    // Draw the original image onto the resized bitmap, scaling it to fit
                    using (var graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.DrawImage(originalBitmap, new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Save the resized barcode image to PNG
                    resizedBitmap.Save(resizedPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the saved images
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Resized barcode saved to: {resizedPath}");
    }
}