using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

namespace BarcodeToBitmapDemo
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Set barcode colors using Aspose.Drawing types
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.White;

                // Generate the barcode as an Aspose.Drawing.Bitmap
                using (var barcodeBitmap = generator.GenerateBarCodeImage())
                {
                    // Perform GDI+ manipulation: draw a red border around the barcode
                    using (var graphics = Graphics.FromImage(barcodeBitmap))
                    {
                        // Create a red pen with a width of 3 points
                        using (var pen = new Pen(Color.Red, 3f))
                        {
                            // Draw a rectangle that matches the bitmap bounds
                            graphics.DrawRectangle(pen, 0, 0, barcodeBitmap.Width - 1, barcodeBitmap.Height - 1);
                        }
                    }

                    // Save the manipulated image to a PNG file
                    barcodeBitmap.Save("barcode_with_border.png", ImageFormat.Png);
                }
            }
        }
    }
}