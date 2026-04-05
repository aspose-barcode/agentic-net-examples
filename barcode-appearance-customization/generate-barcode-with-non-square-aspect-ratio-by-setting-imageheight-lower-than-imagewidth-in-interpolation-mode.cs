using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Use Interpolation mode to allow custom image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a non‑square aspect ratio: width larger than height
            generator.Parameters.ImageWidth.Pixels = 300f;   // Width in pixels
            generator.Parameters.ImageHeight.Pixels = 100f;  // Height in pixels (lower than width)

            // Save the generated barcode image
            generator.Save("non_square_barcode.png");
        }

        Console.WriteLine("Barcode generated: non_square_barcode.png");
    }
}