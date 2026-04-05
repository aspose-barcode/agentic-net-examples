using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Keep automatic sizing mode as Interpolation
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Override default size by specifying explicit width and height (in points)
            generator.Parameters.ImageWidth.Point = 300f;   // Width = 300 points
            generator.Parameters.ImageHeight.Point = 150f; // Height = 150 points

            // Save the barcode image as PNG
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}