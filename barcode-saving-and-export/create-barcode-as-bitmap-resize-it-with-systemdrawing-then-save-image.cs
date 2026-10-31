// Title: Generate and Resize a Barcode Image using Aspose.BarCode and System.Drawing
// Description: This example creates a Code128 barcode, saves it as a PNG bitmap, enlarges it using System.Drawing, and writes both original and resized images to disk.
// Category-Description: Demonstrates Aspose.BarCode image generation and manipulation. The sample uses BarcodeGenerator to produce a bitmap, then employs Aspose.Drawing (System.Drawing compatible) classes such as Bitmap, Graphics, and ImageFormat to resize and save the image. Typical use cases include creating printable barcodes, adjusting image dimensions for UI display, or preparing assets for further processing. Developers working with barcode generation and .NET image handling will find this pattern useful.
// Prompt: Create a barcode as a Bitmap, resize it with System.Drawing, then save the image.
// Tags: code128, barcode generation, image resize, bitmap, aspose.barcode, aspose.drawing, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode, resize it using System.Drawing, and save both versions as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves the original bitmap,
    /// creates a resized version, and writes both images to the output folder.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Paths for the original and resized barcode images
        string originalPath = Path.Combine(outputDir, "barcode_original.png");
        string resizedPath = Path.Combine(outputDir, "barcode_resized.png");

        // Generate barcode as Aspose.Drawing.Bitmap
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Save the original barcode image
                originalBitmap.Save(originalPath, ImageFormat.Png);

                // Calculate new dimensions (double the size)
                int newWidth = originalBitmap.Width * 2;
                int newHeight = originalBitmap.Height * 2;

                // Create a new bitmap with the desired size
                using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Draw the original bitmap onto the resized canvas
                    using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.DrawImage(
                            originalBitmap,
                            new Aspose.Drawing.Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Save the resized barcode image
                    resizedBitmap.Save(resizedPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the saved files
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Resized barcode saved to: {resizedPath}");
    }
}