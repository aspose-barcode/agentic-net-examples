using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the foreground (bars) color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Make the background transparent so the image can be overlaid on video
            generator.Parameters.BackColor = Color.Transparent;

            // Optional: define image size (in points) if needed
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 80f;

            // Save the barcode as PNG (supports alpha channel)
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode image with transparent background saved as 'barcode.png'.");
    }
}