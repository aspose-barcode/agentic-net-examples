using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 and set the text to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Generate the barcode image as a Bitmap
            using (var originalBitmap = generator.GenerateBarCodeImage())
            {
                // Desired size for the resized image
                int newWidth = 300;
                int newHeight = 150;

                // Create a new bitmap with the target dimensions using Aspose.Drawing
                using (var resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Draw the original barcode onto the new bitmap with high‑quality interpolation
                    using (var graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
                    }

                    // Save the resized barcode image as PNG
                    resizedBitmap.Save("barcode_resized.png", ImageFormat.Png);
                }
            }
        }
    }
}