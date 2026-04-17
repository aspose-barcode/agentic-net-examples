using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Code39 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Increase XDimension to make bars wider (e.g., 2 points)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Optionally, set a higher resolution for better print quality
            generator.Parameters.Resolution = 300;

            // Save the barcode image to a PNG file
            generator.Save("code39_wide.png");
        }

        Console.WriteLine("Barcode generated: code39_wide.png");
    }
}