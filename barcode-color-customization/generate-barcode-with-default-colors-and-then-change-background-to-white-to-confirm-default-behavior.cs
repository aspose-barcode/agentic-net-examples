using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode with default colors and save it
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123456";
                // Save with default background (which is white by default)
                generator.Save("default_background.png");
            }

            // Create another barcode, explicitly set background to white, and save it
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123456";
                // Explicitly set background color to white
                generator.Parameters.BackColor = Color.White;
                generator.Save("white_background.png");
            }

            Console.WriteLine("Barcodes generated successfully.");
        }
    }
}