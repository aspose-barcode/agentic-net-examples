using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Desired dimensions in points
        float desiredWidth = 300f;
        float desiredHeight = 300f;

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set image dimensions (points)
            generator.Parameters.ImageWidth.Point = desiredWidth;
            generator.Parameters.ImageHeight.Point = desiredHeight;

            // Example code text
            generator.CodeText = "123456";

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve dimensions from generator (points)
                float widthFromParams = generator.Parameters.ImageWidth.Point;
                float heightFromParams = generator.Parameters.ImageHeight.Point;

                // Retrieve dimensions from bitmap (pixels)
                int pixelWidth = bitmap.Width;
                int pixelHeight = bitmap.Height;

                // Output the values
                Console.WriteLine($"Specified ImageWidth (points): {desiredWidth}");
                Console.WriteLine($"Generator ImageWidth (points): {widthFromParams}");
                Console.WriteLine($"Specified ImageHeight (points): {desiredHeight}");
                Console.WriteLine($"Generator ImageHeight (points): {heightFromParams}");
                Console.WriteLine($"Generated bitmap width (pixels): {pixelWidth}");
                Console.WriteLine($"Generated bitmap height (pixels): {pixelHeight}");

                // Verify that the set values match the generator's parameters
                bool widthMatch = Math.Abs(widthFromParams - desiredWidth) < 0.001f;
                bool heightMatch = Math.Abs(heightFromParams - desiredHeight) < 0.001f;
                Console.WriteLine($"Width matches specification: {widthMatch}");
                Console.WriteLine($"Height matches specification: {heightMatch}");

                // Save the image (optional)
                bitmap.Save("barcode.png", ImageFormat.Png);
            }
        }
    }
}