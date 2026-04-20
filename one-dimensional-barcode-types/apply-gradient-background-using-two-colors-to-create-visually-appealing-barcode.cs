using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

namespace GradientBarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Make the barcode background transparent so the gradient shows through
                generator.Parameters.BackColor = Color.Transparent;

                // Generate the barcode image (transparent background)
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Create a new bitmap to hold the gradient background
                    using (Bitmap gradientBitmap = new Bitmap(barcodeImage.Width, barcodeImage.Height))
                    {
                        // Prepare graphics object for drawing
                        using (Graphics graphics = Graphics.FromImage(gradientBitmap))
                        {
                            // Define a linear gradient from top-left to bottom-right
                            using (LinearGradientBrush brush = new LinearGradientBrush(
                                new Rectangle(0, 0, gradientBitmap.Width, gradientBitmap.Height),
                                Color.LightBlue,   // Start color
                                Color.LightGreen,  // End color
                                LinearGradientMode.ForwardDiagonal))
                            {
                                // Fill the entire bitmap with the gradient
                                graphics.FillRectangle(brush, 0, 0, gradientBitmap.Width, gradientBitmap.Height);
                            }

                            // Draw the barcode image on top of the gradient background
                            graphics.DrawImage(barcodeImage, 0, 0, barcodeImage.Width, barcodeImage.Height);
                        }

                        // Save the final image with gradient background
                        gradientBitmap.Save("gradient_barcode.png", ImageFormat.Png);
                    }
                }
            }

            // Indicate completion (optional)
            Console.WriteLine("Barcode with gradient background saved as 'gradient_barcode.png'.");
        }
    }
}