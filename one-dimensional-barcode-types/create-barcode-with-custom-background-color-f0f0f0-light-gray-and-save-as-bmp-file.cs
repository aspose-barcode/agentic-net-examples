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
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set custom background color #F0F0F0 (light gray)
                generator.Parameters.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

                // Save the barcode as a BMP file
                generator.Save("barcode.bmp");
            }
        }
    }
}