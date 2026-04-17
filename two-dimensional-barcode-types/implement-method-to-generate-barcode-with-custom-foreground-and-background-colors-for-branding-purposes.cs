using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeColorExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Set custom foreground (bars) color
                generator.Parameters.Barcode.BarColor = Color.Blue;
                // Set custom background color
                generator.Parameters.BackColor = Color.Yellow;

                // Save the barcode image as PNG
                generator.Save("custom_barcode.png");
            }

            Console.WriteLine("Barcode generated successfully.");
        }
    }
}