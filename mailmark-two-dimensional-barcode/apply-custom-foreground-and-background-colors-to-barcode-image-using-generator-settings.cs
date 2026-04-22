using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeColorExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Set the foreground (bars) color.
                generator.Parameters.Barcode.BarColor = Color.Blue;

                // Set the background color.
                generator.Parameters.BackColor = Color.Yellow;

                // Save the barcode image to a PNG file.
                generator.Save("colored_barcode.png");
            }

            Console.WriteLine("Barcode generated with custom colors.");
        }
    }
}