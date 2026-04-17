using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    // Retrieves the actual pixel dimensions of the generated barcode image.
    static (int Width, int Height) GetBarcodePixelDimensions(BarcodeGenerator generator)
    {
        // Generate the barcode image as a Bitmap.
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Width and Height are already in pixels.
            return (bitmap.Width, bitmap.Height);
        }
    }

    static void Main()
    {
        // Create a barcode generator for Code128.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode.
            generator.CodeText = "1234567890";

            // Configure unit‑based size and resolution.
            generator.Parameters.ImageWidth.Point = 300f;   // 300 points width
            generator.Parameters.ImageHeight.Point = 150f; // 150 points height
            generator.Parameters.Resolution = 300f;        // 300 DPI
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the barcode to a file (optional, just to demonstrate saving).
            generator.Save("barcode.png");

            // Retrieve and display the actual pixel dimensions.
            var (width, height) = GetBarcodePixelDimensions(generator);
            Console.WriteLine($"Actual pixel dimensions: Width = {width}px, Height = {height}px");
        }
    }
}