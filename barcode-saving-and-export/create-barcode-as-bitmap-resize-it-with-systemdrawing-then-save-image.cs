using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, resizing it, and saving as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, doubles its dimensions, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Path where the resized barcode image will be saved.
        string outputPath = "barcode_resized.png";

        // Define barcode type and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123456";

        // Create a barcode generator with the specified type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Generate the original barcode image as a bitmap.
            using (var originalBitmap = generator.GenerateBarCodeImage())
            {
                // Calculate new dimensions (double the original width and height).
                int newWidth = originalBitmap.Width * 2;
                int newHeight = originalBitmap.Height * 2;

                // Create a new bitmap with the target size.
                using (var resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Obtain a graphics object to draw onto the resized bitmap.
                    using (var graphics = Graphics.FromImage(resizedBitmap))
                    {
                        // Render the original barcode onto the resized bitmap, scaling it.
                        graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
                    }

                    // Save the resized bitmap as a PNG file.
                    resizedBitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Output the full path of the saved image for verification.
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}