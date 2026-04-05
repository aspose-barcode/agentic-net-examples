using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Example dimensions; in a real scenario these could come from configuration or arguments.
        int width = 300;
        int height = 150;

        GenerateBarcodeWithAutoSize(width, height);
    }

    // Generates a barcode image and switches AutoSizeMode based on the provided dimensions.
    static void GenerateBarcodeWithAutoSize(int width, int height)
    {
        // Create a barcode generator for Code128.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the desired image dimensions.
            generator.Parameters.ImageWidth.Point = width;
            generator.Parameters.ImageHeight.Point = height;

            // Choose AutoSizeMode:
            // - If width is greater than height, use Interpolation (scale to exact size, may affect readability).
            // - Otherwise, use Nearest (scale to nearest lower size preserving aspect ratio).
            generator.Parameters.AutoSizeMode = width > height
                ? AutoSizeMode.Interpolation
                : AutoSizeMode.Nearest;

            // Optional: set code text.
            generator.CodeText = "1234567890";

            // Save the generated barcode image.
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated with AutoSizeMode set based on dimensions.");
    }
}