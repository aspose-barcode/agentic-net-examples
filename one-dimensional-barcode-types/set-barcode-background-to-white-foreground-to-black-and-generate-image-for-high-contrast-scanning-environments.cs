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
            // Create a barcode generator for Code128 with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set high‑contrast colors: white background, black bars
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the barcode image to a file
                generator.Save("high_contrast_barcode.png");
            }
        }
    }
}