using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Disable automatic resizing
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Increase X-dimension to make bars wider for low‑resolution screens
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set the text to encode
            generator.CodeText = "1234567890";

            // Save the generated barcode as a PNG file
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}