using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Use AutoSizeMode.Nearest to size the barcode based on image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Specify the desired image size (in points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image to a file
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode image saved as barcode.png");
    }
}