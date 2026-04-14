using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Keep AutoSizeMode as Interpolation
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Override default sizing by setting explicit image dimensions (points)
            generator.Parameters.ImageWidth.Point = 300f;   // Width = 300 points
            generator.Parameters.ImageHeight.Point = 150f; // Height = 150 points

            // Save the generated barcode image
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}