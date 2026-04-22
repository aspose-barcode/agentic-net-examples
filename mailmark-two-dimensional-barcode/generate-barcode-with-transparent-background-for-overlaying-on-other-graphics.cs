using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeTransparentBackground
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the background color to transparent
                generator.Parameters.BackColor = Color.Transparent;

                // Optionally set the bar color (e.g., black)
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the barcode image as PNG (supports transparency)
                generator.Save("barcode.png");
            }
        }
    }
}