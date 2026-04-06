using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Pixel dimensions and desired resolution (DPI)
        int widthPixels = 300;
        int heightPixels = 150;
        float dpi = 96f;

        // Convert pixels to points (1 point = 1/72 inch)
        float widthPoints = widthPixels * 72f / dpi;
        float heightPoints = heightPixels * 72f / dpi;

        // Create a Code128 barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set image resolution
            generator.Parameters.Resolution = dpi;

            // Apply converted dimensions using the .Point member
            generator.Parameters.ImageWidth.Point = widthPoints;
            generator.Parameters.ImageHeight.Point = heightPoints;

            // Optional: set XDimension and BarHeight in points
            generator.Parameters.Barcode.XDimension.Point = 2f;   // 2 points
            generator.Parameters.Barcode.BarHeight.Point = 40f; // 40 points

            // Set the text to encode
            generator.CodeText = "ABC123456";

            // Save the barcode image
            generator.Save("code128.png");
        }
    }
}