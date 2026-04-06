using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = "ABC123";

                // Set a custom foreground (bar) color
                generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 0, 128, 0); // dark green

                // Save the barcode as a BMP file
                generator.Save("code128.bmp");
            }
        }
    }
}