using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use interpolation auto-size mode to control image dimensions directly
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a non‑square aspect ratio: width larger than height
            generator.Parameters.ImageWidth.Point = 300f;   // 300 points wide
            generator.Parameters.ImageHeight.Point = 150f;  // 150 points high (lower than width)

            // Save the barcode image
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated: barcode.png");
    }
}