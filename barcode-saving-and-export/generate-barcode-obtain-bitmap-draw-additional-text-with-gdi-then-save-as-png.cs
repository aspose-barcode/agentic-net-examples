using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Generate the barcode image as a Bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Draw additional text onto the barcode image using GDI+
                using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                {
                    // Define font and brush for the custom text
                    using (Font font = new Font("Arial", 12f))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        // Position the text at the bottom-left corner
                        PointF textPosition = new PointF(5f, barcodeBitmap.Height - 20f);
                        graphics.DrawString("Sample Text", font, brush, textPosition);
                    }
                }

                // Save the final image as PNG
                barcodeBitmap.Save("barcode_with_text.png", ImageFormat.Png);
            }
        }
    }
}