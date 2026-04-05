using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the data to encode
                generator.CodeText = "1234567890";

                // Configure automatic sizing to use interpolation
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Specify the desired image dimensions (in points)
                generator.Parameters.ImageWidth.Point = 600f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Set a high resolution (e.g., 300 DPI) for a high‑resolution PNG
                generator.Parameters.Resolution = 300f;

                // Generate the barcode image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image as a PNG file
                    bitmap.Save("highres_barcode.png", ImageFormat.Png);
                }
            }

            Console.WriteLine("Barcode image generated: highres_barcode.png");
        }
    }
}