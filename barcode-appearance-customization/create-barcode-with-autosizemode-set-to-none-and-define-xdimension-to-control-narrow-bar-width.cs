using System;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to be encoded
                generator.CodeText = "1234567890";

                // Disable automatic resizing
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Define XDimension (narrow bar width) in points
                generator.Parameters.Barcode.XDimension.Point = 0.5f;

                // Save the generated barcode as a PNG file
                generator.Save("barcode.png");
            }

            Console.WriteLine("Barcode generated and saved as barcode.png");
        }
    }
}