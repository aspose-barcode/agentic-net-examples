using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the entry point for the barcode generation and resizing example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode, saves the original image, creates a resized version,
    /// and saves both to disk.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original and resized barcode images.
        string originalPath = "barcode_original.png";
        string resizedPath = "barcode_resized.png";

        // Initialize a barcode generator for Code128 with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Generate the barcode as a bitmap image.
            using (var originalBitmap = generator.GenerateBarCodeImage())
            {
                // Save the original barcode image to the specified path in PNG format.
                originalBitmap.Save(originalPath, ImageFormat.Png);

                // Capture the original image dimensions and DPI settings.
                int originalWidth = originalBitmap.Width;
                int originalHeight = originalBitmap.Height;
                float dpiX = originalBitmap.HorizontalResolution;
                float dpiY = originalBitmap.VerticalResolution;

                // Compute new dimensions by doubling the pixel width and height.
                int newWidth = originalWidth * 2;
                int newHeight = originalHeight * 2;

                // Create a new bitmap with the doubled size, preserving the original pixel format.
                using (var resizedBitmap = new Bitmap(newWidth, newHeight, originalBitmap.PixelFormat))
                {
                    // Apply the original DPI to the resized bitmap to maintain resolution consistency.
                    resizedBitmap.SetResolution(dpiX, dpiY);

                    // Render the original bitmap onto the resized bitmap, scaling it up.
                    using (var graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
                    }

                    // Save the resized barcode image to the specified path in PNG format.
                    resizedBitmap.Save(resizedPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user that the process completed successfully.
        Console.WriteLine("Barcode generated and resized successfully.");
    }
}