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

                // Use AutoSizeMode.Nearest to let the generator resize to the nearest size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

                // Specify desired image dimensions (in points)
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.ImageWidth.Point = 300f;

                // Generate the barcode image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image as PNG using Aspose.Drawing.Imaging.ImageFormat
                    bitmap.Save("barcode.png", ImageFormat.Png);
                }
            }

            Console.WriteLine("Barcode image generated: barcode.png");
        }
    }
}