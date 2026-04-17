using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Generate the barcode image
            using (var originalBitmap = generator.GenerateBarCodeImage())
            {
                // Preserve original DPI
                float dpiX = originalBitmap.HorizontalResolution;
                float dpiY = originalBitmap.VerticalResolution;

                // Calculate new dimensions (double the size)
                int newWidth = originalBitmap.Width * 2;
                int newHeight = originalBitmap.Height * 2;

                // Create a new bitmap with the new dimensions
                using (var resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Set the same DPI as the original image
                    resizedBitmap.SetResolution(dpiX, dpiY);

                    // Draw the original bitmap onto the new bitmap with high-quality scaling
                    using (var graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(
                            originalBitmap,
                            new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Save the resized barcode image to a file
                    resizedBitmap.Save("resized_barcode.png", ImageFormat.Png);
                }
            }
        }
    }
}