using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Drawing2D;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 and set the text to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Generate the barcode image as a bitmap
            using (Bitmap original = generator.GenerateBarCodeImage())
            {
                // Preserve the original DPI
                float dpiX = original.HorizontalResolution;
                float dpiY = original.VerticalResolution;

                // Calculate new dimensions (double the pixel size)
                int newWidth = original.Width * 2;
                int newHeight = original.Height * 2;

                // Create a new bitmap with the doubled dimensions
                using (Bitmap resized = new Bitmap(newWidth, newHeight))
                {
                    // Apply the original DPI to the new bitmap
                    resized.SetResolution(dpiX, dpiY);

                    // Draw the original image onto the new bitmap using high‑quality interpolation
                    using (Graphics g = Graphics.FromImage(resized))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(original, new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Save both the original and the resized images
                    original.Save("barcode_original.png", ImageFormat.Png);
                    resized.Save("barcode_resized.png", ImageFormat.Png);
                }
            }
        }
    }
}