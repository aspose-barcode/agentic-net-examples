using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

namespace BarcodeResizeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 and set the text to encode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "1234567890";

                // Generate the barcode as a bitmap
                using (var original = generator.GenerateBarCodeImage())
                {
                    // Define new dimensions (e.g., double the size)
                    int newWidth = original.Width * 2;
                    int newHeight = original.Height * 2;

                    // Create a new bitmap with the desired size
                    using (var resized = new Bitmap(newWidth, newHeight))
                    {
                        // Draw the original image onto the new bitmap with scaling
                        using (var graphics = Graphics.FromImage(resized))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(
                                original,
                                new Rectangle(0, 0, newWidth, newHeight));
                        }

                        // Save the resized barcode image as PNG
                        resized.Save("barcode_resized.png", ImageFormat.Png);
                    }
                }
            }
        }
    }
}