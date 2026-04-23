using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

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
                generator.CodeText = "PURPLE123";

                // Apply a custom purple foreground color (RGB 128,0,128)
                generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 128);

                // Ensure background is white (optional, default is white)
                generator.Parameters.BackColor = Color.White;

                // Save the barcode image to a PNG file
                generator.Save("purple_barcode.png");
            }
        }
    }
}