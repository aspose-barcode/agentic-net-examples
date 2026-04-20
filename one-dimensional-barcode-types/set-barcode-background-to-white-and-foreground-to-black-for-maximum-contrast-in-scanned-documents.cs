using System;
using Aspose.BarCode;
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
                // Set the text to encode
                generator.CodeText = "1234567890";

                // Set foreground (bars) color to black
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Set background color to white
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image to a file
                generator.Save("barcode.png");
            }

            Console.WriteLine("Barcode generated successfully.");
        }
    }
}