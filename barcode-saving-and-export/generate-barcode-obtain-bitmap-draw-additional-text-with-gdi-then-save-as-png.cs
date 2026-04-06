using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 and set the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Generate the barcode image as a bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Draw additional text onto the bitmap using Aspose.Drawing
                using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                {
                    // Define font and brush for the extra text
                    using (Font font = new Font("Arial", 16f, FontStyle.Bold))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        // Position the text at the bottom-left corner
                        PointF textPosition = new PointF(10f, barcodeBitmap.Height - 30f);
                        graphics.DrawString("Additional Info", font, brush, textPosition);
                    }
                }

                // Save the final image as PNG
                barcodeBitmap.Save("barcode_with_text.png", ImageFormat.Png);
            }
        }
    }
}