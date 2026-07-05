// Title: Generate and Resize a Code128 Barcode Image
// Description: Demonstrates creating a Code128 barcode as a bitmap, enlarging it using System.Drawing, and saving both original and resized images as PNG files.
// Prompt: Create a barcode as a Bitmap, resize it with System.Drawing, then save the image.
// Tags: code128, barcode generation, resize, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, resizes it, and saves both versions as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, resizes it using System.Drawing, and writes the images to disk.
    /// </summary>
    static void Main()
    {
        // Paths for the original and resized barcode images
        string originalPath = "barcode_original.png";
        string resizedPath = "barcode_resized.png";

        // 1. Generate a Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // 2. Load the generated image using Aspose.Drawing
        using (var originalImage = Image.FromFile(originalPath) as Bitmap)
        {
            if (originalImage == null)
            {
                Console.WriteLine("Failed to load the generated barcode image.");
                return;
            }

            // Define new dimensions (e.g., double the size)
            int newWidth = originalImage.Width * 2;
            int newHeight = originalImage.Height * 2;

            // 3. Create a new bitmap with the desired size
            using (var resizedBitmap = new Bitmap(newWidth, newHeight))
            {
                // 4. Draw the original image onto the resized bitmap
                using (var graphics = Graphics.FromImage(resizedBitmap))
                {
                    graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                }

                // 5. Save the resized image as PNG
                resizedBitmap.Save(resizedPath, ImageFormat.Png);
            }
        }

        // Output the locations of the saved images
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Resized barcode saved to: {resizedPath}");
    }
}